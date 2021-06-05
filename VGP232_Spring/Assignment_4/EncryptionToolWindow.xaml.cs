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
using System.Windows.Shapes;

namespace Assignment_4
{
    /// <summary>
    /// Interaction logic for EncryptionToolWindow.xaml
    /// </summary>
    public partial class EncryptionToolWindow : Window
    {
        public Crypto myCrypto { get; set; }
        public CryptoAlgorithm mode { get; set; }


        public EncryptionToolWindow()
        {
            InitializeComponent();
            SelectionWindow sw = new SelectionWindow();
            if (sw.ShowDialog() == true)
            {
                myCrypto = sw.crypto;
                mode = sw.cAlgo;
            }
            else
            {
                Close();
            }
        }

        private void SaveToFile_Cipher(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, tbDecrypt_Result.Text);
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            try
            {
                string cypherText = tbCiphered.Text;
                byte[] cypherData = Convert.FromBase64String(cypherText);
                byte[] plainData = myCrypto.Decrypt(cypherData);
                string plainText = Convert.ToBase64String(plainData);
                plainText = plainText.Replace('+', ' ');
                tbDecrypt_Result.Text = plainText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Decryption failed: " + ex.Message);
            }
        }

        private void LoadCipher(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                byte[] cypherBytes = File.ReadAllBytes(openFile.FileName);
                tbCiphered.Text = Convert.ToBase64String(cypherBytes);
            }
        }

        private void SaveToFile_Text(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFile.FileName, Convert.FromBase64String(tbEncrypt_Result.Text));
            }
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            string plainText = tbMessage.Text;
            plainText = plainText.Replace(' ', '+');
            int padding = plainText.Length % 4;
            if (padding != 0)
            {
                plainText += new string('+', 4 - padding);
            }

            byte[] data = Convert.FromBase64String(plainText);
            byte[] cypherData = myCrypto.Encrypt(data);
            string cypherText = Convert.ToBase64String(cypherData);
            tbEncrypt_Result.Text = cypherText;
        }

        private void LoadText(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                tbMessage.Text = File.ReadAllText(openFile.FileName);
            }
        }
    }
}
