using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Base64ToPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                Base64TextBox.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Base64TextBox.Text))
            {
                MessageBox.Show($"Nothing to Convert");
                return;
            }

            var pdfInBytes = Convert.FromBase64String(Base64TextBox.Text);

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                FileName = "Base64.pdf",
                Filter = "PDF (.pdf)|*.pdf|All Files|*.*",
                FilterIndex = 0,
                Title = "Select Name of Output PDF",
                DefaultExt = ".PDF"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFileDialog.FileName, pdfInBytes);

                if (File.Exists(saveFileDialog.FileName))
                {
                    ProcessStartInfo psi = new();
                    psi.FileName = saveFileDialog.FileName;
                    psi.UseShellExecute = true;
                    Process.Start(psi);
                }
            }
        }

        private void OnBase64TextBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ConvertBase64Btn.IsEnabled = !string.IsNullOrEmpty(Base64TextBox.Text);
        }
    }
}
