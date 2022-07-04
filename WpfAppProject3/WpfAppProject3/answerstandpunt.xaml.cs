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
    /// Interaction logic for answerstandpunt.xaml
    /// </summary>
    public partial class answerstandpunt : Window
    {
        DataTable _allestandpunten = new DataTable();
        string _partijid = "";

        public answerstandpunt(string id)
        {
            InitializeComponent();
            _allestandpunten = _data.HaalAlleStandpuntenOp();
            _partijid = id;
            LoadNextStandpunt();
        }

        Verkiezingen _data = new Verkiezingen();
        int _counter = 0;

        private void LoadNextStandpunt()
        {
            if (_counter < _allestandpunten.Rows.Count)
            {
                tbStatusStelling.Text = "Stelling " + (_counter + 1) + " van " + _allestandpunten.Rows.Count;
                tbStelling.Text = _allestandpunten.Rows[_counter]["standpunt"].ToString();
            }
            else
            {
                MessageBox.Show("Dank voor het beantwoorden van alle stellingen! Je gaat nu terug naar het overzicht van alle partijen");
                bhPartijen partijen = new bhPartijen();
                this.Close();
                partijen.ShowDialog();
            }
        }

        private void btEens_Click(object sender, RoutedEventArgs e)
        {
            string eensofoneens = "1";
            string standpuntid = _allestandpunten.Rows[_counter]["standpunt_id"].ToString();
            _data.AddMeningVanPartij(standpuntid, _partijid, eensofoneens);
            _counter++;
            LoadNextStandpunt();
        }

        private void btOneens_Click(object sender, RoutedEventArgs e)
        {
            string eensofoneens = "0";
            string standpuntid = _allestandpunten.Rows[_counter]["standpunt_id"].ToString();
            _data.AddMeningVanPartij(standpuntid, _partijid, eensofoneens);
            _counter++;
            LoadNextStandpunt();
        }
    }
}
