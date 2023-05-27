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

namespace BCZZ3O_HFT_2022231.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditVehicleWindow ev = new EditVehicleWindow();
            ev.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditDriverWindow ed = new EditDriverWindow();
            ed.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EditShiftWindow es = new EditShiftWindow();
            es.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NonCRUDsWindow ncw = new NonCRUDsWindow();
            ncw.Show();
        }
    }
}
