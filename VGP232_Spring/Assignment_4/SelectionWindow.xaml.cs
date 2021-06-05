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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        public Crypto crypto { get; set; }

        public CryptoAlgorithm cAlgo { get; set; }
        public SelectionWindow()
        {
            InitializeComponent();
        }

        private void Next_Pressed(object sender, RoutedEventArgs e)
        {
            if (cbAES.IsChecked == true)
            {
                crypto = new Crypto(CryptoAlgorithm.AES);
                cAlgo = CryptoAlgorithm.AES;
                KeysWindow kw = new KeysWindow(crypto , CryptoAlgorithm.AES);
                if (kw.ShowDialog() == true)
                {
                    crypto = kw.crypto1;
                    DialogResult = true;
                }
            }
            else if (cbRSA.IsChecked == true)
            {
                crypto = new Crypto(CryptoAlgorithm.RSA);
                cAlgo = CryptoAlgorithm.RSA;
                KeysWindow kw = new KeysWindow(crypto, CryptoAlgorithm.RSA);
                if (kw.ShowDialog() == true)
                {
                    crypto = kw.crypto1;
                    DialogResult = true;
                }
            }
            Close();
        }
    }
}
