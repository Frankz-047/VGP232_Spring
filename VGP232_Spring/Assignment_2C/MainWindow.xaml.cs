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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeaponLib;

namespace Assignment_2C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeaponCollection weaponCollection { get; set; }
        public List<Weapon> weaponTypeSelected { get; set; }
        public bool isTypeSorted { get; set; }
        public WeaponType selectedType { get; set; }
        public string currentFilter { get; set; }
        public bool isFiltered { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            weaponCollection = new WeaponCollection();
            weaponTypeSelected = new WeaponCollection();
            isTypeSorted = false;
            isFiltered = false;
            CbType.ItemsSource = Enum.GetNames(typeof(WeaponType));
        }

        private void LoadPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| Json files |*.json| CSV files |*.csv";

            if (openFile.ShowDialog() == true)
            {
                if (!weaponCollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load the file.");
                }
                else
                {
                    lbWeapons.ItemsSource = weaponCollection;
                    lbWeapons.Items.Refresh();
                }
            }
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            if (weaponCollection.Any())
            {
                EditWindow editWindow = new EditWindow();
                if (editWindow.ShowDialog() == true)
                {
                    if (editWindow.myWeapon != null)
                    {
                        weaponCollection.Add(editWindow.myWeapon);
                    }
                    if (lbWeapons.ItemsSource == null)
                    {
                        lbWeapons.ItemsSource = weaponCollection;
                    }
                    lbWeapons.Items.Refresh();
                }
            }
        }

        private void EditPressed(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
            {
                return;
            }
            if (weaponCollection.Any())
            {
                EditWindow editWindow = new EditWindow();
                editWindow.myWeapon = lbWeapons.SelectedItem as Weapon;

                if (editWindow.ShowDialog() == true)
                {
                    if (editWindow.myWeapon != null)
                    {
                        weaponCollection[lbWeapons.SelectedIndex] = editWindow.myWeapon;
                    }
                    lbWeapons.Items.Refresh();
                }
            }
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml| Json files |*.json| CSV files |*.csv";

            if (saveFile.ShowDialog() == true)
            {
                if (!weaponCollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            
            if (isTypeSorted)
            {
                weaponCollection.Remove(weaponTypeSelected[lbWeapons.SelectedIndex]);
                weaponTypeSelected.RemoveAt(lbWeapons.SelectedIndex);
            }
            else
            {
                weaponCollection.RemoveAt(lbWeapons.SelectedIndex);
            }
            lbWeapons.Items.Refresh();
        }

        private void SortByName_Checked(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("Name");
            if (isTypeSorted)
            {
                SortWeaponBy(selectedType);
            }
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
            lbWeapons.Items.Refresh();
        }

        private void SortByBaseAttack_Checked(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("BaseAttack");
            if (isTypeSorted)
            {
                SortWeaponBy(selectedType);
            }
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
            lbWeapons.Items.Refresh();
        }

        private void SortByRarity_Checked(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("rarity");
            if (isTypeSorted)
            {
                SortWeaponBy(selectedType);
            }
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
            lbWeapons.Items.Refresh();
        }

        private void SortByPassive_Checked(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("passive");
            if (isTypeSorted)
            {
                SortWeaponBy(selectedType);
            }
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
            lbWeapons.Items.Refresh();
        }

        private void SortBySecondaryStat_Checked(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("secondarystat");
            if (isTypeSorted)
            {
                SortWeaponBy(selectedType);
            }
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
            lbWeapons.Items.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedType = Enum.Parse<WeaponType>(CbType.SelectedValue.ToString());
            SortWeaponBy(selectedType);
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
        }

        private void lbWeapons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterTextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            currentFilter = textBox.Text.ToString();
            FilterByText(currentFilter);
        }

        private void SortWeaponBy(WeaponType selectedType)
        {
            if (selectedType == WeaponType.None)
            {
                isTypeSorted = false;
                lbWeapons.ItemsSource = weaponCollection;
                lbWeapons.Items.Refresh();
            }
            else
            {
                isTypeSorted = true;
                weaponTypeSelected = weaponCollection.GetAllWeaponsOfType(selectedType);
                lbWeapons.ItemsSource = weaponTypeSelected;
                lbWeapons.Items.Refresh();
            }
        }

        private void FilterByText(string currentFilter)
        {
            if (currentFilter != "")
            {
                isFiltered = true;
                if (isTypeSorted)
                {
                    foreach (var weapon in weaponTypeSelected.ToList())
                    {
                        if (!weapon.Name.StartsWith(currentFilter))
                        {
                            weaponTypeSelected.Remove(weapon);
                        }
                    }
                }
                else
                {
                    weaponTypeSelected = weaponCollection.FilterByName(currentFilter);
                }
                
                lbWeapons.ItemsSource = weaponTypeSelected;
                lbWeapons.Items.Refresh();
            }
            else
            {
                isFiltered = false;
                lbWeapons.ItemsSource = weaponCollection;
                lbWeapons.Items.Refresh();
            }
        }
    }
}
