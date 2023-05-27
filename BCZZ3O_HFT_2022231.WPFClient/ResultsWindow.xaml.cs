using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VehicleFleetDb.Models;

namespace BCZZ3O_HFT_2022231.WPFClient
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public ResultsWindow(ObservableCollection<Shift> results)
        {
            InitializeComponent();
            listBox.ItemsSource = results;
        }

        public ResultsWindow(ObservableCollection<string> results)
        {
            InitializeComponent();
            listBox.ItemsSource = results;
        }

        public ResultsWindow(ObservableCollection<Driver> results)
        {
            InitializeComponent();
            listBox.ItemsSource = results;
        }

        public ResultsWindow(ObservableCollection<Vehicle> results)
        {
            InitializeComponent();
            listBox.ItemsSource = results;
        }
    }
}
