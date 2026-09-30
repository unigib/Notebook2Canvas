using System;
using System.IO;
using System.Windows.Forms;

namespace Notebook2Canvas
{
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
