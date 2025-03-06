using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace BrailleTranslatorApp
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> availableTables;
        private const string tablesDirectory = "C:/msys64/usr/share/liblouis/tables";

        public MainWindow()
        {
            InitializeComponent();
            InitializeUI();
            LoadTables();
        }

        private void InitializeUI()
        {
            this.Title = "Braille Translator";
            this.WindowState = WindowState.Maximized;
        }

        private void LoadTables()
        {
            availableTables = GetAllTables(tablesDirectory);
            tableComboBox.ItemsSource = availableTables.Keys.ToArray();
            if (tableComboBox.Items.Count > 0)
                tableComboBox.SelectedIndex = 0;
        }

        private Dictionary<string, string> GetAllTables(string directory)
        {
            var tables = new Dictionary<string, string>();
            if (Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory, "*.ctb"))
                {
                    var name = Path.GetFileName(file);
                    tables[name] = file;
                }
            }
            return tables;
        }

        private void TextInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string inputText = textInput.Text;
            if (string.IsNullOrWhiteSpace(inputText))
            {
                brailleOutput.Clear();
                return;
            }

            string selectedTable = tableComboBox.SelectedItem?.ToString();
            if (selectedTable == null || !availableTables.ContainsKey(selectedTable))
            {
                brailleOutput.Text = "Please select a valid Braille table.";
                return;
            }

            string tablePath = availableTables[selectedTable];
            string result = RunLibLouisTranslation(inputText, tablePath);

            brailleOutput.Text = result;
        }

        private string RunLibLouisTranslation(string input, string tablePath)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "C:/msys64/usr/bin/lou_translate",
                    Arguments = $"--forward \"{tablePath}\" --display-table C:/msys64/usr/share/liblouis/tables/unicode.dis",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    using (StreamWriter writer = process.StandardInput)
                    {
                        writer.WriteLine(input);
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return output.Trim();  // Clean any trailing whitespaces or newlines
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running LibLouis: " + ex.Message);
                return "Error!";
            }
        }
    }
}
