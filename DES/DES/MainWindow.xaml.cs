using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DES
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Naciśnięcie przycisku 'Load'
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                inputTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }

            if (binRadioButton.IsChecked == false)
            {
                binRadioButton.IsChecked = true;
            }
        }

        //Naciśnięcie przycisku 'Encrypt'
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputTextBox.Text) && !string.IsNullOrWhiteSpace(keyTextBox.Text))
            {
                outputTextBox.Text = keyTextBox.Text.Length == 16
                    ? inputTextBox.Text.Length % 2 == 0
                        ? binRadioButton.IsChecked == true
                            ? DESunio.Encoding(inputTextBox.Text, keyTextBox.Text, true)
                            : DESunio.Encoding(inputTextBox.Text, keyTextBox.Text, false)
                        : "Input must to have even digits"
                    : "Key must be 16 characters long";
            }
        }


        //Naciśnięcie przycisku 'Descrypt'
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputTextBox.Text) && !string.IsNullOrWhiteSpace(keyTextBox.Text))
            {
                outputTextBox.Text = keyTextBox.Text.Length == 16
                    ? inputTextBox.Text.Length % 2 == 0
                        ? binRadioButton.IsChecked == true
                            ? DESunio.Decoding(inputTextBox.Text, keyTextBox.Text, true)
                            : DESunio.Decoding(inputTextBox.Text, keyTextBox.Text, false)
                        : "Input must to have even digits"
                    : "Key must be 16 characters long";
            }
        }

        //Naciśnięcie przycisku 'Generate'
        private void GenerateKey_Click(object sender, RoutedEventArgs e)
        {
            LFSR lfsr = new LFSR("16", "1+x^2+x^4+x^7");
            List<int> temp = lfsr.Generate();
            string temptext = "";
            foreach (int i in temp)
            {
                temptext += i.ToString();
            }
            keyTextBox.Text = temptext;
        }
    }
}
