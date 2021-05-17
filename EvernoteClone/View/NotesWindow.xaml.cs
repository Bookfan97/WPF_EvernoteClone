using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.IO;
using Azure.Storage.Blobs;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private NotesVM viewModel;

        public NotesWindow()
        {
            InitializeComponent();

            viewModel = Resources["Vm"] as NotesVM;
            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double> { 8, 9, 10, 11, 12, 14, 16, 28, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        private async Task ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentRichTextBox.Document.Blocks.Clear();
            if (viewModel.SelectedNote != null)
            {
                if (!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
                {
                    string downloadPath = $"{viewModel.SelectedNote.ID}.rtf";
                    await new BlobClient(new Uri(viewModel.SelectedNote.FileLocation)).DownloadToAsync(downloadPath);
                    using (FileStream fileStream = new FileStream(downloadPath, FileMode.Open))
                    {
                        var contents = new TextRange(contentRichTextBox.Document.ContentStart,
                            contentRichTextBox.Document.ContentEnd);
                        contents.Load(fileStream, DataFormats.Rtf);
                    }
                }
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (String.IsNullOrEmpty(App.UserID))
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                viewModel.GetNotebooks();
            }
        }

        //Speech Button
        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            var speechConfig =
                SpeechConfig.FromSubscription(SecretsHelper.GetAzureServiceApiKey(), SecretsHelper.GetAzureRegion());
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
            {
                var result = await recognizer.RecognizeOnceAsync();
                contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
            }
        }

        //Gets character count
        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int characterCount =
                (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text
                .Length;
            statusTextBlock.Text = $"Length: {characterCount} characters";
        }

        //Set Text to Bold
        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        //Set Text to Italic
        private void ItalicsButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        //Set Text to Underline
        private void UnderlineButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    TextDecorations.Underline);
            }
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as
                    TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        //Currently checks text in the textbox and changes font modifier buttons accordingly
        private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedBold = contentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            var selectedItalic = contentRichTextBox.Selection.GetPropertyValue(FontStyleProperty);
            var selectedUnderline = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            BoldButton.IsChecked = (selectedBold != DependencyProperty.UnsetValue) &&
                                   (selectedBold.Equals(FontWeights.Bold));
            ItalicsButton.IsChecked = (selectedItalic != DependencyProperty.UnsetValue) &&
                                      (selectedItalic.Equals(FontStyles.Italic));
            UnderlineButton.IsChecked = (selectedUnderline != DependencyProperty.UnsetValue) &&
                                        (selectedUnderline.Equals(TextDecorations.Underline));
            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            fontSizeComboBox.Text = contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }

        private void FontFamilyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty,
                    fontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, fontSizeComboBox.Text);
        }

        private void FontSizeComboBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"{viewModel.SelectedNote.ID}.rtf";
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            using (FileStream fileStream = new FileStream(rtfFile, FileMode.Create))
            {
                var contents = new TextRange(contentRichTextBox.Document.ContentStart,
                    contentRichTextBox.Document.ContentEnd);
                contents.Save(fileStream, DataFormats.Rtf);
            }
            viewModel.SelectedNote.FileLocation = await UpdateFile(rtfFile, fileName);
            await DatabaseHelper.Update(viewModel.SelectedNote);
        }

        private async Task<string> UpdateFile(string file, string rtfFile)
        {
            string connectionString = SecretsHelper.GetAzureConnectionString();
            string containerName = SecretsHelper.GetAzureContainerName();
            string containerURL = SecretsHelper.GetAzureContainerURL();
            var container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExistsAsync();
            var blob = container.GetBlobClient(rtfFile);
            await blob.UploadAsync(rtfFile);
            return $"{containerURL}/{rtfFile}";
        }
    }
}