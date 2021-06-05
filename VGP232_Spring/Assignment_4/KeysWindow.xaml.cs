using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for KeysWindow.xaml
    /// </summary>
    public partial class KeysWindow : Window
    {
        string[] RSAButtons = { "Import Private key", "Import Public key", "Export Private key", "Export Public key" };
        string[] AESButtons = { "Import Shared key", "Import IV", "Export Shared key", "Export IV" };

        public Crypto crypto1;
        CryptoAlgorithm mode;
        public KeysWindow(Crypto crypto,CryptoAlgorithm ca)
        {
            InitializeComponent();
            mode = ca;
            crypto1 = crypto;
            if (mode == CryptoAlgorithm.AES)
            {
                ButtonSetup(AESButtons);
            }
            else if (mode == CryptoAlgorithm.RSA)
            {
                ButtonSetup(RSAButtons);
            }
        }


        private void Loadk1_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            switch (mode)
            {
                case CryptoAlgorithm.RSA:
                    openFile.Filter = "XML files | *.xml";
                    break;
                case CryptoAlgorithm.AES:
                    openFile.Filter = "Binary files |*.bin";
                    break;
                default:
                    break;
            }

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    crypto1.LoadK1(openFile.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Loading Failed");
                }
            }
        }

        private void Loadk2_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            switch (mode)
            {
                case CryptoAlgorithm.RSA:
                    openFile.Filter = "XML files | *.xml";
                    break;
                case CryptoAlgorithm.AES:
                    openFile.Filter = "Binary files |*.bin";
                    break;
                default:
                    break;
            }

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    crypto1.LoadK2(openFile.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Loading Failed");
                }
            }
        }

        private void Savek1_Clicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            switch (mode)
            {
                case CryptoAlgorithm.RSA:
                    saveFile.Filter = "XML files | *.xml";
                    break;
                case CryptoAlgorithm.AES:
                    saveFile.Filter = "Binary files |*.bin";
                    break;
                default:
                    break;
            }

            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    crypto1.SaveK1(saveFile.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Save Failed");
                }
            }
        }

        private void Savek2_Clicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            switch (mode)
            {
                case CryptoAlgorithm.RSA:
                    saveFile.Filter = "XML files | *.xml";
                    break;
                case CryptoAlgorithm.AES:
                    saveFile.Filter = "Binary files |*.bin";
                    break;
                default:
                    break;
            }

            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    crypto1.SaveK2(saveFile.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Save Failed");
                }
            }
        }

        private void Next_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButtonSetup(string[] contents)
        {
            Loadk1.Content = contents[0];
            Loadk2.Content = contents[1];
            Savek1.Content = contents[2];
            Savek2.Content = contents[3];
        }
    }
}
