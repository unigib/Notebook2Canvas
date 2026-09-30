using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Notebook2Canvas
{
    public partial class Form1 : Form
    {
        String mdExportFile;
        String jsonImportFile;
        // user preference: explicit converter path
        private string preferredConverterPath;
        public Form1()
        {
            InitializeComponent();
            LoadPreferences();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Gracefully close the application
            Application.Exit();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a NotebookLM JSON file";
                openFileDialog.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.InitialDirectory = ".";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        jsonImportFile = openFileDialog.FileName;
                        mdExportFile = Path.ChangeExtension(jsonImportFile, ".md");
                        // Display file path
                        textBox1.Text = jsonImportFile;
                        textBox2.Text = mdExportFile;

                        // Read and display file content from the import file
                        String content = File.ReadAllText(openFileDialog.FileName);
                        richTextBox1.Text = content;


                        

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            JsonToTextConverter jsonToTextConverter = new JsonToTextConverter();

            StringBuilder sq = jsonToTextConverter.Convert(jsonImportFile);

            richTextBox2.Text = sq.ToString();

            File.WriteAllText(mdExportFile, sq.ToString());


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(mdExportFile, richTextBox2.Text);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void rjButtonExportQti_Click(object sender, EventArgs e)
        {
            // Ensure MD file exists
            if (string.IsNullOrWhiteSpace(mdExportFile) || !File.Exists(mdExportFile))
            {
                MessageBox.Show("Markdown file not found. Please generate and save the MD file first.", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prefer a bundled text2qti executable shipped with the app (./tools/text2qti/text2qti.exe or ./tools/text2qti.exe).
            // Fallback to `text2qti` on PATH if no bundled executable is present.
            string appBase = AppDomain.CurrentDomain.BaseDirectory;
            string bundledExe = Path.Combine(appBase, "tools", "text2qti", "text2qti.exe");
            string bundledExeAlt = Path.Combine(appBase, "tools", "text2qti.exe");

            string exeToRun;
            // If user configured a converter path, prefer it
            if (!string.IsNullOrWhiteSpace(preferredConverterPath) && File.Exists(preferredConverterPath))
            {
                exeToRun = preferredConverterPath;
            }
            else if (File.Exists(bundledExe))
            {
                exeToRun = bundledExe;
            }
            else if (File.Exists(bundledExeAlt))
            {
                exeToRun = bundledExeAlt;
            }
            else
            {
                exeToRun = "text2qti"; // expect on PATH
            }

            string args = $"\"{mdExportFile}\"";

            var outputDir = Path.GetDirectoryName(mdExportFile) ?? Environment.CurrentDirectory;

            var psi = new ProcessStartInfo
            {
                FileName = exeToRun,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = outputDir
            };

            try
            {
                // Run process off the UI thread
                var result = await Task.Run(() =>
                {
                    using (var p = Process.Start(psi))
                    {
                        string stdout = p.StandardOutput.ReadToEnd();
                        string stderr = p.StandardError.ReadToEnd();
                        p.WaitForExit();
                        return new { ExitCode = p.ExitCode, StdOut = stdout, StdErr = stderr };
                    }
                });

                if (result.ExitCode != 0)
                {
                    MessageBox.Show($"{text2qtiDisplayName(exeToRun)} failed (exit {result.ExitCode}).\n\n{result.StdErr}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(text2qtiDisplayName(exeToRun) + " completed successfully. Output directory: " + outputDir, "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Optionally open output folder
                    try { Process.Start("explorer.exe", outputDir); } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to run {exeToRun}: {ex.Message}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void rjButtonPrefs_Click(object sender, EventArgs e)
        {
            using (var pref = new PreferencesForm(preferredConverterPath))
            {
                if (pref.ShowDialog(this) == DialogResult.OK)
                {
                    preferredConverterPath = pref.SelectedPath;
                    SavePreferences(preferredConverterPath);
                }
            }
        }

        private void LoadPreferences()
        {
            try
            {
                var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook2Canvas");
                var file = Path.Combine(dir, "settings.txt");
                if (File.Exists(file))
                {
                    preferredConverterPath = File.ReadAllText(file).Trim();
                    if (string.IsNullOrWhiteSpace(preferredConverterPath))
                        preferredConverterPath = null;
                    // update UI if control exists
                    try { textBoxConverterPath.Text = preferredConverterPath ?? "(not set)"; } catch { }
                }
            }
            catch
            {
                // ignore
            }
        }

        private void SavePreferences(string converterPath)
        {
            try
            {
                var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook2Canvas");
                Directory.CreateDirectory(dir);
                var file = Path.Combine(dir, "settings.txt");
                File.WriteAllText(file, converterPath ?? string.Empty);
            }
            catch
            {
                // ignore
            }
        }
                });

                if (result.ExitCode != 0)
                {
                    MessageBox.Show($"{text2qtiDisplayName(exeToRun)} failed (exit {result.ExitCode}).\n\n{result.StdErr}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(text2qtiDisplayName(exeToRun) + " completed successfully. Output directory: " + outputDir, "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Optionally open output folder
                    try { Process.Start("explorer.exe", outputDir); } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to run {exeToRun}: {ex.Message}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string text2qtiDisplayName(string exePath)
        {
            if (string.IsNullOrWhiteSpace(exePath))
                return "text2qti";
            if (exePath.Equals("text2qti", StringComparison.OrdinalIgnoreCase))
                return "text2qti (PATH)";
            return Path.GetFileName(exePath);
        }
    }
}


        