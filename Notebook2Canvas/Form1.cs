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
        public Form1()
        {
            InitializeComponent();
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

            // Default command: try to run `text2qti` on PATH. If users bundle python, they can change this later.
            string exe = "text2qti";
            string args = $"\"{mdExportFile}\"";

            var outputDir = Path.GetDirectoryName(mdExportFile) ?? Environment.CurrentDirectory;

            var psi = new ProcessStartInfo
            {
                FileName = exe,
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
                var result = await Task.Run(() => {
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
                    MessageBox.Show($"text2qti failed (exit {result.ExitCode}).\n\n{result.StdErr}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("text2qti completed successfully. Output directory: " + outputDir, "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Optionally open output folder
                    try { Process.Start("explorer.exe", outputDir); } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to run text2qti: {ex.Message}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


        