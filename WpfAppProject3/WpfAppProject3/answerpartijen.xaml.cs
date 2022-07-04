using System;
using System.Collections.Generic;
using System.Data;
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
using WpfAppProject3.Class;

namespace WpfAppProject3
{
    /// <summary>
    /// Interaction logic for answerpartijen.xaml
    /// </summary>
    public partial class answerpartijen : Window
    {
        DataTable _allepartijen = new DataTable();
        string _standpuntid = "";

        public answerpartijen(string id, string ingevuld)
        {
            InitializeComponent();
            _allepartijen = _data.HaalAllePartijenOp();
            _standpuntid = id;
            tbStelling.Text = "Stelling: " + ingevuld; 
            LoadNextPartij();
        }

        Verkiezingen _data = new Verkiezingen();
        int _counter = 0;

        private void LoadNextPartij()
        {
            if (_counter < _allepartijen.Rows.Count)
            {
                tbStatusPartij.Text = "Partij " + (_counter + 1) + " van " + _allepartijen.Rows.Count;
                tbPartij.Text = _allepartijen.Rows[_counter]["naam"].ToString();
            }
            else
            {
                MessageBox.Show("Dank voor het beantwoorden van alle partijen! Je gaat nu terug naar het overzicht van alle standpunten");
                bhStandpunten standpunten = new bhStandpunten();
                this.Close();
                standpunten.ShowDialog();
            }
        }

        private void btEens_Click(object sender, RoutedEventArgs e)
        {
            string eensofniet = "1";
            string partijid = _allepartijen.Rows[_counter]["partij_id"].ToString();
            _data.AddMeningVanPartij(_standpuntid, partijid ,eensofniet);
            _counter++;
            LoadNextPartij();
        }

        private void btOneens_Click(object sender, RoutedEventArgs e)
        {
            string eensofniet = "0";
            string partijid = _allepartijen.Rows[_counter]["partij_id"].ToString();
            _data.AddMeningVanPartij(_standpuntid, partijid, eensofniet);
            _counter++;
            LoadNextPartij();
        }
    }
}
