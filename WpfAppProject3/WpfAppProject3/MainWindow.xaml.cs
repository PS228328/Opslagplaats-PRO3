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

namespace WpfAppProject3
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

        //Voor iedere button is een eventhandler gemaakt, elke leid naar een nieuw window, afhankelijk van welke knop geklikt word

        private void btPartijen_Click(object sender, RoutedEventArgs e)
        {
            bhPartijen newWindow = new bhPartijen();
            this.Close();
            newWindow.ShowDialog();
        }

        private void btThemas_Click(object sender, RoutedEventArgs e)
        {
            bhThemas newWindow = new bhThemas();
            this.Close();
            newWindow.ShowDialog();
        }

        private void btStandpunten_Click(object sender, RoutedEventArgs e)
        {
            bhStandpunten newWindow = new bhStandpunten();
            this.Close();
            newWindow.ShowDialog();
        }

        private void btVerkiezingssoorten_Click(object sender, RoutedEventArgs e)
        {
            bhVerkiezingssoorten newWindow = new bhVerkiezingssoorten();
            this.Close();
            newWindow.ShowDialog();
        }

        private void btVerkiezingen_Click(object sender, RoutedEventArgs e)
        {
            bhVerkiezingen newWindow = new bhVerkiezingen();
            this.Close();
            newWindow.ShowDialog();
        }

        private void btVerkiezingspartijen_Click(object sender, RoutedEventArgs e)
        {
            bhVerkiezingspartijen newWindow = new bhVerkiezingspartijen();
            this.Close();
            newWindow.ShowDialog();
        }
    }
}
