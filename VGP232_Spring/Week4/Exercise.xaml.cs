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

namespace Week4
{
    /// <summary>
    /// Interaction logic for Exercise.xaml
    /// </summary>
    public partial class Exercise : Window
    {
        private int counter = 0;
        public Exercise()
        {
            InitializeComponent();
        }

        private void IncrementCount(object sender, RoutedEventArgs e)
        {
            ++counter;
            tbCounter.Text = string.Format($"Count = {counter}");
        }

        private void HelloMessage(object sender, RoutedEventArgs e)
        {
            tbTitle.Text = string.Format($"Hello {tboxUserName.Text} !");
        }
    }
}
