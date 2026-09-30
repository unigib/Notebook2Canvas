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
    }
}


        