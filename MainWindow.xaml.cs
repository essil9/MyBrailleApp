using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BrailleTranslatorApp
{
    public partial class MainWindow : Window
    {
        private const int MaxLinesPerPage = 20; // Nombre de lignes avant une nouvelle page
        private Dictionary<string, string> availableTables = new(); // ✅ Initialisation pour éviter le warning
        private const string tablesDirectory = "C:/msys64/usr/share/liblouis/tables";

        private List<TextBox> textPages = new();
        private List<TextBox> braillePages = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializeUI();
            LoadTables();
            AddNewPage(); // ✅ Créer la première page
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

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textPages.Count == 0) return;

            TextBox currentTextBox = textPages.Last();
            string[] lines = currentTextBox.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // ✅ Si on dépasse le nombre de lignes, on crée une nouvelle page
            if (lines.Length > MaxLinesPerPage)
            {
                string overflowText = string.Join(Environment.NewLine, lines.Skip(MaxLinesPerPage));
                currentTextBox.Text = string.Join(Environment.NewLine, lines.Take(MaxLinesPerPage));

                AddNewPage();
                textPages.Last().Text = overflowText; // ✅ Ajouter l'excédent sur la nouvelle page
            }

            // ✅ Traduire en Braille et mettre à jour la sortie
            UpdateBrailleOutput();
        }

        private void UpdateBrailleOutput()
        {
            braillePagesContainer.Children.Clear();
            braillePages.Clear();

            foreach (var textPage in textPages)
            {
                string translatedText = RunLibLouisTranslation(textPage.Text, tableComboBox.SelectedItem?.ToString() ?? "");
                TextBox braillePage = CreateBraillePage(translatedText);
                braillePages.Add(braillePage);
                braillePagesContainer.Children.Add(braillePage);
            }
        }

        private string RunLibLouisTranslation(string input, string tableName)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(tableName) || !availableTables.ContainsKey(tableName))
                return "Please select a valid Braille table.";

            string tablePath = availableTables[tableName];

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

                using (Process process = new() { StartInfo = psi })
                {
                    process.Start();

                    using (StreamWriter writer = process.StandardInput)
                    {
                        writer.WriteLine(input);
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return output.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running LibLouis: " + ex.Message);
                return "Error!";
            }
        }

        private void AddNewPage()
        {
            // ✅ Ajouter une nouvelle page de saisie
            TextBox newTextPage = CreateTextPage();
            textPages.Add(newTextPage);
            textPagesContainer.Children.Add(newTextPage);

            // ✅ Ajouter une nouvelle page de sortie Braille
            TextBox newBraillePage = CreateBraillePage();
            braillePages.Add(newBraillePage);
            braillePagesContainer.Children.Add(newBraillePage);
        }

        private TextBox CreateTextPage()
        {
            TextBox textPage = new TextBox
            {
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                TextWrapping = TextWrapping.Wrap, // ✅ Retour à la ligne automatique
                Height = 400, // ✅ Hauteur d'une page
                Width = 450,
                MaxLines = MaxLinesPerPage
            };

            // ✅ Corriger l'attachement de l'événement
            textPage.TextChanged += TextInput_TextChanged;

            return textPage;
        }
        private void NewDocument_Click(object sender, RoutedEventArgs e)
{
    // Effacer les pages existantes
    textPagesContainer.Children.Clear();
    braillePagesContainer.Children.Clear();
    
    textPages.Clear();
    braillePages.Clear();

    // Ajouter une nouvelle page vide
    AddNewPage();
}
 
        private TextBox CreateBraillePage(string text = "")
        {
            TextBox braillePage = new TextBox
            {
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                TextWrapping = TextWrapping.Wrap, // ✅ Retour à la ligne automatique
                Height = 400, // ✅ Hauteur d'une page
                Width = 450,
                IsReadOnly = true,
                Text = text
            };

            return braillePage;
        }
    }
}

