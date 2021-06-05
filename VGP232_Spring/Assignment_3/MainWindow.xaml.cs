using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Serialization;
using TextureAtlasLib;

namespace Assignment_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Spritesheet mySpriteSheet { get; set; }

        public string myXMLPath { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mySpriteSheet = new Spritesheet();
            mySpriteSheet.InputPaths = new List<string>();
            DataContext = mySpriteSheet;
            lbImages.ItemsSource = mySpriteSheet.InputPaths;
            myXMLPath = "";
        }

        private void OpenPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml";
            if (openFile.ShowDialog() == true)
            {
                if (!Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load the file.");
                }
                else
                {
                    myXMLPath = openFile.FileName;
                    tbOutputDir.Text = mySpriteSheet.OutputDirectory;
                    tbOutputFile.Text = mySpriteSheet.OutputFile;
                    tbColumns.Text = mySpriteSheet.Columns.ToString();
                    RbMetaData.IsChecked = mySpriteSheet.IncludeMetaData;
                    lbImages.ItemsSource = mySpriteSheet.InputPaths;
                    lbImages.Items.Refresh();
                }
            }
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PNG Images| *.png";
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                foreach (string imagePath in openFile.FileNames)
                {
                    mySpriteSheet.InputPaths.Add(imagePath);
                }
                lbImages.Items.Refresh();
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if (lbImages.SelectedIndex == -1)
            {
                return;
            }
            mySpriteSheet.InputPaths.RemoveAt(lbImages.SelectedIndex);
            lbImages.Items.Refresh();
        }

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {

            mySpriteSheet.OutputDirectory = tbOutputDir.Text;
            mySpriteSheet.OutputFile = tbOutputFile.Text;

            try
            {
                mySpriteSheet.Generate(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var generateBox = MessageBox.Show("Do you want to view it?", "View Confirmation", MessageBoxButton.YesNo);

            if (generateBox == MessageBoxResult.Yes)
            {
                Process.Start("explorer.exe",@mySpriteSheet.OutputDirectory);
            }
            else
            {
                return;
            }
        }

        private void BrowsePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog browsePath = new SaveFileDialog();
            browsePath.Filter = "PNG Images| *.png";
            if (browsePath.ShowDialog() == true)
            {
                tbOutputFile.Text = System.IO.Path.GetFileName(browsePath.FileName);
                tbOutputDir.Text = System.IO.Path.GetDirectoryName(browsePath.FileName);
            }
        }

        private void NewPressed(object sender, RoutedEventArgs e)
        {
            mySpriteSheet = new Spritesheet();
            mySpriteSheet.InputPaths = new List<string>();
            tbColumns.Clear();
            tbOutputDir.Clear();
            tbOutputFile.Clear();
            RbMetaData.IsChecked = false;
            DataContext = mySpriteSheet;
            lbImages.ItemsSource = mySpriteSheet.InputPaths;
            lbImages.Items.Refresh();
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            if (mySpriteSheet != new Spritesheet() && mySpriteSheet.InputPaths.Any() && myXMLPath != "")
            {
                if (!Save(myXMLPath))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void SaveAsPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml";

            if (saveFile.ShowDialog() == true)
            {
                if (!Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
                else if(myXMLPath == "")
                {
                    myXMLPath = saveFile.FileName;
                }
            }
        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
            var saveBox = MessageBox.Show("Do you want to save the file before Exit? ", "Save Confirmation", MessageBoxButton.YesNoCancel);

            if (saveBox == MessageBoxResult.Cancel)
            {
                return;
            }
            else if (saveBox == MessageBoxResult.Yes)
            {
                if (myXMLPath != "")
                {
                    if (!Save(myXMLPath))
                    {
                        MessageBox.Show("Unable to save file.");
                    }
                }
                else
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "XML files |*.xml";

                    if (saveFile.ShowDialog() == true)
                    {
                        if (!Save(saveFile.FileName))
                        {
                            MessageBox.Show("Unable to save file.");
                        }
                    }
                }
                Application.Current.Shutdown();
            }
            else if (saveBox == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
        }

        private bool Save(string filename)
        {
            if (System.IO.Path.GetExtension(filename) != ".xml")
            {
                return false;
            }
            XmlSerializer xmlser = new XmlSerializer(typeof(Spritesheet));
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    xmlser.Serialize(sw, mySpriteSheet);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private bool Load(string filename)
        {
            if (!File.Exists(filename) || System.IO.Path.GetExtension(filename) != ".xml")
            {
                return false;
            }
            mySpriteSheet = new Spritesheet();
            mySpriteSheet.InputPaths = new List<string>();
            XmlSerializer xmlser = new XmlSerializer(typeof(Spritesheet));
            try
            {

                using (StreamReader sr = new StreamReader(filename))
                {
                    mySpriteSheet = (Spritesheet)xmlser.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void IncludeMeta_Checked(object sender, RoutedEventArgs e)
        {
            mySpriteSheet.IncludeMetaData = true;
        }

        private void IncludeMeta_Unchecked(object sender, RoutedEventArgs e)
        {
            mySpriteSheet.IncludeMetaData = false;
        }
    }
}
