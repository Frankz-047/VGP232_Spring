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
using PerksLib;

namespace PerksEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PerksCollection PerksCollection { get; set; }
        public List<Perk> PerkFieldSelected { get; set; }
        public Field selectedField { get; set; }
        public string currentFilter { get; set; }
        public bool isFieldSorted { get; set; }
        public bool isFiltered { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PerksCollection = new PerksCollection();
            PerkFieldSelected = new PerksCollection();
            isFiltered = false;
            CbField.ItemsSource = Enum.GetNames(typeof(Field));
        }

        private void LoadPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| Json files |*.json| CSV files |*.csv| Text files |*.txt";

            if (openFile.ShowDialog() == true)
            {
                if (!PerksCollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load the file.");
                    lbPerks.Items.Refresh();
                }
                else
                {
                    lbPerks.ItemsSource = PerksCollection;
                    lbPerks.Items.Refresh();
                }
            }
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            if (editWindow.ShowDialog() == true)
            {
                if (editWindow.myPerk != null)
                {
                    PerksCollection.Add(editWindow.myPerk);
                }
                if (lbPerks.ItemsSource == null)
                {
                    lbPerks.ItemsSource = PerksCollection;
                }
                lbPerks.Items.Refresh();
            }
        }

        private void EditPressed(object sender, RoutedEventArgs e)
        {
            if (lbPerks.SelectedIndex == -1)
            {
                return;
            }
            if (PerksCollection.Any())
            {
                EditWindow editWindow = new EditWindow();
                editWindow.myPerk = lbPerks.SelectedItem as Perk;

                if (editWindow.ShowDialog() == true)
                {
                    if (editWindow.myPerk != null)
                    {
                        PerksCollection[lbPerks.SelectedIndex] = editWindow.myPerk;
                    }
                    lbPerks.Items.Refresh();
                }
            }
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml| Json files |*.json| CSV files |*.csv| Text files |*.txt";

            if (saveFile.ShowDialog() == true)
            {
                if (!PerksCollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if (lbPerks.SelectedIndex == -1)
            {
                return;
            }


            if (isFieldSorted)
            {
                PerksCollection.Remove(PerkFieldSelected[lbPerks.SelectedIndex]);
                PerkFieldSelected.RemoveAt(lbPerks.SelectedIndex);
            }
            else
            {
                PerksCollection.RemoveAt(lbPerks.SelectedIndex);
            }
            lbPerks.Items.Refresh();
        }

       

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedField = Enum.Parse<Field>(CbField.SelectedValue.ToString());
            SortPerkBy(selectedField);
            if (isFiltered)
            {
                FilterByText(currentFilter);
            }
        }

        private void lbPerks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterTextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            currentFilter = textBox.Text.ToString();
            FilterByText(currentFilter);
        }

        private void SortPerkBy(Field selectedField)
        {
            if (selectedField == Field.None)
            {
                isFieldSorted = false;
                lbPerks.ItemsSource = PerksCollection;
                lbPerks.Items.Refresh();
            }
            else
            {
                isFieldSorted = true;
                PerkFieldSelected = PerksCollection.GetAllPerksWithField(selectedField);
                lbPerks.ItemsSource = PerkFieldSelected;
                lbPerks.Items.Refresh();
            }
            
        }

        private void FilterByText(string currentFilter)
        {
            if (currentFilter != "")
            {
                isFiltered = true;
                if (isFieldSorted)
                {
                    foreach (var Perk in PerkFieldSelected.ToList())
                    {
                        if (!Perk.Name.StartsWith(currentFilter))
                        {
                            PerkFieldSelected.Remove(Perk);
                        }
                    }
                }
                else
                {
                    PerkFieldSelected = PerksCollection.FilterByName(currentFilter);
                }

                lbPerks.ItemsSource = PerkFieldSelected;
                lbPerks.Items.Refresh();
            }
            else
            {
                isFiltered = false;
                lbPerks.ItemsSource = PerksCollection;
                lbPerks.Items.Refresh();
            }
        }

        private void Simulation_Click(object sender, RoutedEventArgs e)
        {
            SimulationWindow simulationWindow = new SimulationWindow(PerksCollection);
            simulationWindow.ShowDialog();
        }
    }
}
