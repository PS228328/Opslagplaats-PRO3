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
    /// Interaction logic for bhWijzigStandpunt.xaml
    /// </summary>
    public partial class bhWijzigStandpunt : Window
    {
        public bhWijzigStandpunt(int id, string thema, string standpunt, bool createorupdate)
        {
            InitializeComponent();
            ComboBoxItem selectedThema = new ComboBoxItem();
            selectedThema.Content = thema;

            //Staat de boolean op true? Dan maken we een scherm aan wat aangeeft dat je iets gaat aanmaken
            //Anders doen we een scherm aanmaken waarbij we de gebruiker vertellen dat hij iets gaat updaten
            //Als de boolean op true staat hebben we de meegegeven dingen natuurlijk niet nodig, die zijn dan ingesteld op waarde 0 en ""

            if (createorupdate == true)
            {
                tbxHeader.Text = "Maak standpunt aan";
                btUpdate.Content = "Aanmaken";
            }
            else
            {
                tbxHeader.Text = "Wijzig standpunt";
                btUpdate.Content = "Wijzig";
            }

            //We gaan de combobox vullen met alle themas zoals we ook deden bij de standpunten zoekfunctie
            //We geven een comboboxitem mee met de content van het bijbehorende thema (indien het gaat om een update)

            FillComboBoxThemas(thema);
            tbxID.Text = id.ToString();
            tbxStandpunt.Text = standpunt;
        }

        Verkiezingen _data = new Verkiezingen();
        bhStandpunten standpunten = new bhStandpunten();

        private void FillComboBoxThemas(string selected)
        {
            cbxThema.Items.Clear();
            int counter = 0;
            int index = 0;

            DataTable themas = new DataTable();
            themas = _data.HaalAlleThemasOp();

            while (counter < themas.Rows.Count)
            {
                ComboBoxItem thema = new ComboBoxItem();
                thema.FontSize = 18;
                thema.Content = themas.Rows[counter]["thema"].ToString();

                //Doordat we een string hebben meegegeven, kunnen we nu bij elk comboboxitem kijken of de content overeenkomt
                //Zo ja? Dan word dat de selectedindex. Omdat dit maar 1x kan gebeuren, blijft dit de selectedindex. Handig toch? :)
                if (selected == thema.Content.ToString())
                {
                    index = counter;
                    cbxThema.SelectedIndex = index;
                }

                thema.Content += " - " + themas.Rows[counter]["thema_id"];
                cbxThema.Items.Add(thema);

                counter++;
            }
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Is alles goed en klikt de gebruiker op update? Lets gooooo!!!

            int id = Int32.Parse(tbxID.Text);
            ComboBoxItem selectedItem = (ComboBoxItem)cbxThema.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Selecteer aub een thema voor het standpunt!");
                return;
            }
            string thema = selectedItem.Content.ToString();
            string standpunt = tbxStandpunt.Text;
            int themaid = 0;

            string[] splitter = thema.Split('-');
            //Omdat we in het SQL-statement een id van het thema moeten meegeven, is deze erbij gezet in de content van elk comboboxitem
            //Op deze manier kunnen we door middel van een split het id van het thema ophalen

            try
            {
                themaid = Int32.Parse(splitter[1]);
            }
            catch (Exception)
            {
                MessageBox.Show("Er is een probleem...");
            }

            if (tbxHeader.Text == "Maak standpunt aan")
            {
                //Staat deze tekst in de header? Dan weten we dat de gebruiker een standpunt wilde aanmaken. En dan doen we dat :)
                _data.CreateStandpunt(themaid, standpunt);

                //We moeten alle standpunten ophalen om het juiste id mee te geven. Elke partij moet immers het standpunt van dat id beantwoorden
                DataTable alles = new DataTable();    
                alles =_data.HaalAlleStandpuntenOp();

                int counter = 0;

                while (counter < alles.Rows.Count)
                {
                    counter++;
                }

                counter = counter - 1;

                DataTable idStandpunt = new DataTable();
                idStandpunt = _data.HaalIDvanEenStandpuntOp(alles.Rows[counter]["standpunt_id"].ToString());
                string ingevuld = tbxStandpunt.Text;

                answerpartijen partijen = new answerpartijen(idStandpunt.Rows[0]["standpunt_id"].ToString(), ingevuld);
                this.Close();
                partijen.ShowDialog();
            }
            else
            {
                //Anders wijzigen we het standpunt
                _data.WijzigEenStandpunt(id, themaid, standpunt);
            }

            this.Close();
            standpunten.HaalAllesOp();
            standpunten.ShowDialog();
        }
    }
}
