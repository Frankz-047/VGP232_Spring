using PerksLib;
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

namespace PerksEditor
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        public Character myCharacter { get; set; }
        public Character defaulCharacter { get; set; }
        public PerksCollection perksCollection { get; set; }
        public List<Perk> SelectedPerks { get; set; }
        public bool isSelectionChanged { get; set; }
        public int totalCost { get; set; }
        public SimulationWindow(PerksCollection perks)
        {
            InitializeComponent();
            myCharacter = new Character();
            defaulCharacter = new Character();
            DataContext = myCharacter;
            perksCollection = perks;
            SelectedPerks = new List<Perk>();
            isSelectionChanged = false;
            lbAll.ItemsSource = perksCollection;
            lbSelected.ItemsSource = SelectedPerks;
            lbAll.Items.Refresh();
            lbSelected.Items.Refresh();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            if (lbAll.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                SelectedPerks.Add(perksCollection[lbAll.SelectedIndex]);
                lbSelected.Items.Refresh();
                isSelectionChanged = true;
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (lbSelected.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                SelectedPerks.RemoveAt(lbSelected.SelectedIndex);
                lbSelected.Items.Refresh();
                isSelectionChanged = true;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerks.Any())
            {
                SelectedPerks.Clear();
                lbSelected.Items.Refresh();
                isSelectionChanged = true;
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (!SelectedPerks.Any() || !isSelectionChanged)
            {
                return;
            }
            else
            {
                foreach (var perk in SelectedPerks)
                {
                    myCharacter.ProcessPerk(perk);
                }
                isSelectionChanged = false;
                tbHealth.Text = myCharacter.Health.ToString();
                tbAmmo.Text = myCharacter.Ammo.ToString();
                tbReload.Text = myCharacter.ReloadTime.ToString();
                tbSpeed.Text = myCharacter.MoveSpeed.ToString();
                tbMagSize.Text = myCharacter.MagSize.ToString();
                tbFireRate.Text = myCharacter.FireRate.ToString();
                MessageBox.Show("All Selected perks applied!");
            }
        }

        private void Reset()
        {
            myCharacter.Health = defaulCharacter.Health;
            myCharacter.Ammo = defaulCharacter.Ammo;
            myCharacter.ReloadTime = defaulCharacter.ReloadTime;
            myCharacter.MoveSpeed = defaulCharacter.MoveSpeed;
            myCharacter.MagSize = defaulCharacter.MagSize;
            myCharacter.FireRate = defaulCharacter.FireRate;

            DataContext = myCharacter;

            tbHealth.Text = myCharacter.Health.ToString();
            tbAmmo.Text = myCharacter.Ammo.ToString();
            tbReload.Text = myCharacter.ReloadTime.ToString();
            tbSpeed.Text = myCharacter.MoveSpeed.ToString();
            tbMagSize.Text = myCharacter.MagSize.ToString();
            tbFireRate.Text = myCharacter.FireRate.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+[.]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
