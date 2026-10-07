using System;
using System.IO;
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

        // Designer control fields (minimal set so form compiles if designer file isn't included)
        private RJButton rjButton1;
        private RJButton rjButton2;
        private RJButton rjButton3;
        private RJButton rjButtonExport;
        private RJButton rjButtonPrefs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBoxConverterPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        // Minimal InitializeComponent fallback (keeps layout roughly consistent)
        private void InitializeComponent()
        {
            this.rjButton1 = new Notebook2Canvas.RJButton();
            this.rjButton2 = new Notebook2Canvas.RJButton();
            this.rjButton3 = new Notebook2Canvas.RJButton();
            this.rjButtonExport = new Notebook2Canvas.RJButton();
            this.rjButtonPrefs = new Notebook2Canvas.RJButton();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBoxConverterPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButton1.BorderColor = System.Drawing.SystemColors.Desktop;
            this.rjButton1.BorderRadius = 0;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(21, 24);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(150, 40);
            this.rjButton1.TabIndex = 0;
            this.rjButton1.Text = "Load NotebookLM JSON";
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // rjButton2
            // 
            this.rjButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButton2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton2.BorderRadius = 0;
            this.rjButton2.BorderSize = 0;
            this.rjButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton2.ForeColor = System.Drawing.Color.White;
            this.rjButton2.Location = new System.Drawing.Point(727, 321);
            this.rjButton2.Name = "rjButton2";
            this.rjButton2.Size = new System.Drawing.Size(150, 33);
            this.rjButton2.TabIndex = 10;
            this.rjButton2.Text = "Save MD";
            this.rjButton2.TextColor = System.Drawing.Color.White;
            this.rjButton2.UseVisualStyleBackColor = false;
            this.rjButton2.Click += new System.EventHandler(this.rjButton2_Click);
            // 
            // rjButton3
            // 
            this.rjButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButton3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButton3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton3.BorderRadius = 0;
            this.rjButton3.BorderSize = 0;
            this.rjButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton3.ForeColor = System.Drawing.Color.White;
            this.rjButton3.Location = new System.Drawing.Point(159, 323);
            this.rjButton3.Name = "rjButton3";
            this.rjButton3.Size = new System.Drawing.Size(150, 33);
            this.rjButton3.TabIndex = 9;
            this.rjButton3.Text = "JSON to MD";
            this.rjButton3.TextColor = System.Drawing.Color.White;
            this.rjButton3.UseVisualStyleBackColor = false;
            this.rjButton3.Click += new System.EventHandler(this.rjButton3_Click);
            // 
            // rjButtonExport
            // 
            this.rjButtonExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButtonExport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButtonExport.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButtonExport.BorderRadius = 0;
            this.rjButtonExport.BorderSize = 0;
            this.rjButtonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButtonExport.ForeColor = System.Drawing.Color.White;
            this.rjButtonExport.Location = new System.Drawing.Point(560, 321);
            this.rjButtonExport.Name = "rjButtonExport";
            this.rjButtonExport.Size = new System.Drawing.Size(131, 33);
            this.rjButtonExport.TabIndex = 12;
            this.rjButtonExport.Text = "Export → QTI";
            this.rjButtonExport.TextColor = System.Drawing.Color.White;
            this.rjButtonExport.UseVisualStyleBackColor = false;
            this.rjButtonExport.Click += new System.EventHandler(this.rjButtonExportQti_Click);
            // 
            // rjButtonPrefs
            // 
            this.rjButtonPrefs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButtonPrefs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(0)))), ((int)(((byte)(12)))));
            this.rjButtonPrefs.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButtonPrefs.BorderRadius = 0;
            this.rjButtonPrefs.BorderSize = 0;
            this.rjButtonPrefs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButtonPrefs.ForeColor = System.Drawing.Color.White;
            this.rjButtonPrefs.Location = new System.Drawing.Point(736, 386);
            this.rjButtonPrefs.Name = "rjButtonPrefs";
            this.rjButtonPrefs.Size = new System.Drawing.Size(131, 33);
            this.rjButtonPrefs.TabIndex = 11;
            this.rjButtonPrefs.Text = "Preferences";
            this.rjButtonPrefs.TextColor = System.Drawing.Color.White;
            this.rjButtonPrefs.UseVisualStyleBackColor = false;
            this.rjButtonPrefs.Click += new System.EventHandler(this.rjButtonPrefs_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(893, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 33);
            this.button1.TabIndex = 14;
            this.button1.Text = "Quit";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 115);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(441, 202);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(482, 113);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(417, 202);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(287, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(574, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(287, 47);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(574, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBoxConverterPath
            // 
            this.textBoxConverterPath.Location = new System.Drawing.Point(287, 75);
            this.textBoxConverterPath.Name = "textBoxConverterPath";
            this.textBoxConverterPath.ReadOnly = true;
            this.textBoxConverterPath.Size = new System.Drawing.Size(574, 20);
            this.textBoxConverterPath.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(194, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "JSON Export File";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(194, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "MD File Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Notebook LM  Export JSON";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(488, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "QTI Markdown";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(988, 431);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rjButton3);
            this.Controls.Add(this.rjButton2);
            this.Controls.Add(this.rjButtonPrefs);
            this.Controls.Add(this.rjButtonExport);
            this.Controls.Add(this.textBoxConverterPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Notebook2Canvas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Form1()
        {
            InitializeComponent();
            LoadPreferences();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a NotebookLM JSON file";
                openFileDialog.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.InitialDirectory = ".";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        jsonImportFile = openFileDialog.FileName;
                        mdExportFile = Path.ChangeExtension(jsonImportFile, ".md");
                        textBox1.Text = jsonImportFile;
                        textBox2.Text = mdExportFile;
                        string content = File.ReadAllText(openFileDialog.FileName);
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
            // Ensure a JSON file has been selected and exists before converting
            if (string.IsNullOrWhiteSpace(jsonImportFile) || !File.Exists(jsonImportFile))
            {
                MessageBox.Show(this, "No JSON file selected or file does not exist. Please load a NotebookLM JSON file first.", "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var jsonToTextConverter = new JsonToTextConverter();
            StringBuilder sq;
            try
            {
                sq = jsonToTextConverter.Convert(jsonImportFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to convert JSON: " + ex.Message, "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            richTextBox2.Text = sq.ToString();
            try
            {
                File.WriteAllText(mdExportFile, sq.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to write MD file: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(mdExportFile, richTextBox2.Text);
        }

        // comboBox1 removed - no longer used

        private async void rjButtonExportQti_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mdExportFile) || !File.Exists(mdExportFile))
            {
                MessageBox.Show("Markdown file not found. Please generate and save the MD file first.", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string appBase = AppDomain.CurrentDomain.BaseDirectory;
            string bundledExe = Path.Combine(appBase, "tools", "text2qti", "text2qti.exe");
            string bundledExeAlt = Path.Combine(appBase, "tools", "text2qti.exe");

            string exeToRun;
            if (!string.IsNullOrWhiteSpace(preferredConverterPath) && File.Exists(preferredConverterPath))
                exeToRun = preferredConverterPath;
            else if (File.Exists(bundledExe))
                exeToRun = bundledExe;
            else if (File.Exists(bundledExeAlt))
                exeToRun = bundledExeAlt;
            else
                exeToRun = "text2qti";

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
                    try { Process.Start("explorer.exe", outputDir); } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to run {exeToRun}: {ex.Message}", "Export QTI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void rjButtonPrefs_Click(object sender, EventArgs e)
        {
            using (var pref = new PreferencesForm(preferredConverterPath))
            {
                if (pref.ShowDialog(this) == DialogResult.OK)
                {
                    preferredConverterPath = pref.SelectedPath;
                    SavePreferences(preferredConverterPath);

                    try
                    {
                        var validation = await ValidateConverterAsync(preferredConverterPath);
                        if (validation.Item1)
                        {
                            MessageBox.Show(this, $"Converter validated successfully:\n{(string.IsNullOrWhiteSpace(validation.Item2) ? "(no output)" : validation.Item2)}", "Preferences", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            var keep = MessageBox.Show(this, "The selected converter did not respond successfully to common test arguments (--version/--help).\nKeep this path anyway?", "Preferences", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (keep != DialogResult.Yes)
                            {
                                preferredConverterPath = null;
                                SavePreferences(null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Failed to validate converter: " + ex.Message, "Preferences", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    try { textBoxConverterPath.Text = preferredConverterPath ?? "(not set)"; } catch { }
                }
            }
        }

        private Task<Tuple<bool, string>> ValidateConverterAsync(string exePath)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(exePath) || !File.Exists(exePath))
                    return Tuple.Create(false, "Executable not found.");

                string[] testArgs = new[] { "--version", "--help", "-h" };
                foreach (var a in testArgs)
                {
                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = exePath,
                            Arguments = a,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        using (var p = Process.Start(psi))
                        {
                            if (!p.WaitForExit(3000))
                            {
                                try { p.Kill(); } catch { }
                                continue;
                            }

                            string outp = p.StandardOutput.ReadToEnd();
                            string err = p.StandardError.ReadToEnd();
                            if (p.ExitCode == 0)
                                return Tuple.Create(true, outp + err);
                        }
                    }
                    catch
                    {
                        // ignore and try next arg
                    }
                }

                return Tuple.Create(false, "No successful response from converter.");
            });
        }

        private void textBoxConverterPath_TextChanged(object sender, EventArgs e) { }

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
                    try { textBoxConverterPath.Text = preferredConverterPath ?? "(not set)"; } catch { }
                }
                else
                {
                    try { textBoxConverterPath.Text = preferredConverterPath ?? "(not set)"; } catch { }
                }
            }
            catch
            {
                // ignore
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

    // Simple PreferencesForm embedded here so we don't need to edit the csproj.
    public class PreferencesForm : Form
    {
        private TextBox textBoxPath;
        private Button buttonBrowse;
        private Button buttonSave;
        private Button buttonCancel;

        public string SelectedPath { get; private set; }

        public PreferencesForm(string currentPath)
        {
            InitializeComponent();
            textBoxPath.Text = currentPath ?? string.Empty;
        }

        private void InitializeComponent()
        {
            this.textBoxPath = new TextBox() { Width = 380, Left = 12, Top = 12 };
            this.buttonBrowse = new Button() { Text = "Browse...", Left = 400, Top = 10, Width = 75 };
            this.buttonSave = new Button() { Text = "Save", Left = 220, Top = 48, Width = 80, DialogResult = DialogResult.OK };
            this.buttonCancel = new Button() { Text = "Cancel", Left = 310, Top = 48, Width = 80, DialogResult = DialogResult.Cancel };

            this.buttonBrowse.Click += ButtonBrowse_Click;
            this.buttonSave.Click += ButtonSave_Click;

            this.Text = "Preferences";
            this.ClientSize = new System.Drawing.Size(490, 90);
            this.Controls.Add(textBoxPath);
            this.Controls.Add(buttonBrowse);
            this.Controls.Add(buttonSave);
            this.Controls.Add(buttonCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                ofd.Title = "Select text2qti executable";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxPath.Text = ofd.FileName;
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            var path = textBoxPath.Text?.Trim();
            if (!string.IsNullOrEmpty(path) && !File.Exists(path))
            {
                var res = MessageBox.Show(this, "The selected file does not exist. Save anyway?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }

            SelectedPath = path;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
