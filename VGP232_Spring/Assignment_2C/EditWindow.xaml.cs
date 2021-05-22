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
using WeaponLib;

namespace Assignment_2C
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Weapon weapon;

        public Weapon myWeapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                DataContext = myWeapon;
            }
        }
        public EditWindow()
        {
            InitializeComponent();
            CbType.ItemsSource = Enum.GetNames(typeof(WeaponType));

            myWeapon = new Weapon();
        }

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {
            var ran = new Random();
            myWeapon.BaseAttack = ran.Next(20,50);
            myWeapon.Rarity = ran.Next(1, 6);
            myWeapon.Type = (WeaponType)ran.Next(1, 5);
            DataContext = myWeapon;
            tbBaseAttack.Text = myWeapon.BaseAttack.ToString();
            CbType.SelectedIndex = (int)myWeapon.Type;
            cbRarity.SelectedIndex = myWeapon.Rarity - 1;
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
            if (myWeapon.Name.Any() 
                && myWeapon.Type != WeaponType.None 
                && myWeapon.Rarity >= 1 
                && myWeapon.Passive.Any() 
                && myWeapon.SecondaryStat.Any()
                && myWeapon.Image.Any()
                && myWeapon.BaseAttack >= 0)
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
            Close();
        }
    }
}
