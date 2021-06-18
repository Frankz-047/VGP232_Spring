using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using PerksLib;

namespace PerksEditor
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Perk perk;

        public Perk myPerk
        {
            get { return perk; }
            set
            {
                perk = value;
                DataContext = myPerk;
            }
        }
        public EditWindow()
        {
            InitializeComponent();
            CbField.ItemsSource = Enum.GetNames(typeof(Field));
            CbMethod.ItemsSource = Enum.GetNames(typeof(Method));
            myPerk = new Perk();
        }


        private void CancelPressed(object sender, RoutedEventArgs e)
        {
            //Set the state to false when I close the window
            DialogResult = false;

            //Closes the window
            Close();
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            if (myPerk.Name.Any()
                && myPerk.ModifyField != Field.None
                && myPerk.Name.Any()
                && myPerk.ModifyMethod != Method.None
                && myPerk.Icon.Any())
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
            Close();
        }

        private void GetIconFromFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PNG files |*.png| JPEG files |*.jpg";

            if (openFile.ShowDialog() == true)
            {
                myPerk.Icon = openFile.FileName;
                tbIcon.Text = myPerk.Icon;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^.-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
