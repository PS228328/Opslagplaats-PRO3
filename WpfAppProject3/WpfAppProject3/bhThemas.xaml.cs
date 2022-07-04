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
    /// Interaction logic for bhThemas.xaml
    /// </summary>
    public partial class bhThemas : Window
    {
        Verkiezingen _data = new Verkiezingen();
        int _idtijdelijk = 0;

        public bhThemas()
        {
            InitializeComponent();
            HaalAllesOp();
        }

        private void HaalAllesOp()
        {
            //We maken een datatable aan die we vullen met alles wat in de partijen tabel staat
            DataTable allethemas = new DataTable();
            allethemas = _data.HaalAlleThemasOp();

            LoadDataTable(allethemas); //Geef de datatable mee aan een andere eventhandler

            //Deze eventhandler hierboven word ook gebruikt bij de update en delete functie.
            //Op deze manier word alles meteen bijgewerkt.
        }

        private void LoadDataTable(DataTable allethemas)
        {
            //Hiermee laten we alles op het scherm zien met de datatable die we hebben meegekregen
            databaseitems.Items.Clear();
            int counter = 0;

            //Voor iedere rij maken we een stackpanel met tekstblokken erin, deze voegen we toe aan de listbox (overzichtelijk!)
            while (counter < allethemas.Rows.Count)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.HorizontalAlignment = HorizontalAlignment.Center;
                panel.VerticalAlignment = VerticalAlignment.Top;
                databaseitems.Items.Add(panel);

                TextBlock id = new TextBlock();
                id.Name = "id";
                id.Width = 0;
                id.Opacity = 0;
                id.Text = allethemas.Rows[counter]["thema_id"].ToString();

                TextBlock titel = new TextBlock();
                titel.Width = 150;
                titel.Name = "thema";
                titel.TextAlignment = TextAlignment.Center;
                titel.Text = allethemas.Rows[counter]["thema"].ToString();

                Button wijzig = new Button();
                wijzig.Click += Wijzig_Click;
                wijzig.Width = 80;
                wijzig.Content = "Wijzig";

                Button verwijder = new Button();
                verwijder.Click += Verwijder_Click;
                verwijder.Width = 80;
                verwijder.Content = "Verwijder";

                panel.Children.Add(titel);
                panel.Children.Add(wijzig);
                panel.Children.Add(verwijder);
                panel.Children.Add(id);

                counter++;
            }
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult deleteOrNot = MessageBox.Show("Als je het thema verwijderd, verwijder je ook alle standpunten met dit thema. \n Weet je het zeker?", "Verwijder partij", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (deleteOrNot == MessageBoxResult.No)
            {
                return;
            }

            //Haal de button op waar op geklikt is, inclusief het stackpanel waar die button in zit
            Button verwijder = e.Source as Button;
            StackPanel parent = (StackPanel)verwijder.Parent;

            //We zoeken het tekstblok met het id, deze is niet op het scherm te zien, omdat die verborgen gemaakt is. 
            //deze krijgt de naam 'id', waardoor we deze makkelijk terug kunnen vinden

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                if (item.Name == "id")
                {
                    DataTable partij_standpunt = new DataTable();
                    partij_standpunt = _data.HaalAlleStandpuntenOp2();

                    DataTable stem = new DataTable();
                    stem = _data.HaalAlleStemmenOp();

                    int counter = 0;
                    while (counter < partij_standpunt.Rows.Count)
                    {
                        if (partij_standpunt.Rows[counter]["thema_id"].ToString() == item.Text)
                        {
                            string idstandpunt = partij_standpunt.Rows[counter]["standpunt_id"].ToString();
                            _data.VerwijderStandpuntVanEenThema(idstandpunt);
                            _data.VerwijderStandpuntVanStemmen(idstandpunt);
                        }
                        counter++;
                    }

                    int id = Int32.Parse(item.Text);
                    _data.VerwijderStandpuntMetThema(id);
                    _data.DeleteEenThema(id);
                }
            }

            //Omdat er iets is veranderd in de tabel, halen we opnieuw alles op
            HaalAllesOp();
        }

        private void Wijzig_Click(object sender, RoutedEventArgs e)
        {
            if (btZoekCreateUpdate.Content.ToString() == "Wijzig")
            {
                btZoekCreateUpdate.Content = "Zoek";
                btCreate.Content = "Nieuw";
                return;
            }

            //We zoeken de button uit het betreffende stackpanel
            btZoekCreateUpdate.Content = "Wijzig";
            btCreate.Content = "Terug";

            Button update = e.Source as Button;
            StackPanel parent = (StackPanel)update.Parent;

            //Haal alle gegevens op die belangrijk zijn

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                if (item.Name == "thema")
                {
                    tbZoekCreateUpdate.Text = item.Text;
                }
                else if (item.Name == "id")
                {
                    _idtijdelijk = Int32.Parse(item.Text);
                }
            }
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            //Letterlijk de ga terug knop, om terug te gaan naar het hoofdscherm
            MainWindow home = new MainWindow();
            this.Close();
            home.ShowDialog();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            /*Deze is een stuk complexer, maar in het kort:
             De tekst van de button is afhankelijk van wat er gaat gebeuren. "Zoek" is de default content
            De btCreate button is een button waar op kan staan "Nieuw" of "Terug". 
            Als er op geklikt word gaat de tekst altijd van "Nieuw" naar "Terug" of andersom.
             */

            if (btZoekCreateUpdate.Content.ToString() == "Voeg toe" || btZoekCreateUpdate.Content.ToString() == "Wijzig")
            {
                //Als de tekst van de andere button "Voeg toe" of "Wijzig" was, dan zetten we alles op default.
                btZoekCreateUpdate.Content = "Zoek";
                btCreate.Content = "Nieuw";
                tbZoekCreateUpdate.Text = "";
                return;
            }

            if (btCreate.Content.ToString() == "Nieuw")
            {
                //Was dit niet zo en was de tekst van de nieuwterug button "Nieuw"?, dan zetten we er "Terug" neer
                btCreate.Content = "Terug";
                btZoekCreateUpdate.Content = "Voeg toe";
            }
            else
            {
                //En andersom hetzelfde
                btCreate.Content = "Nieuw";
                btZoekCreateUpdate.Content = "Zoek";
                tbZoekCreateUpdate.Text = "";
            }
        }

        private void btZoekCreateUpdate_Click(object sender, RoutedEventArgs e)
        {
            string content = btZoekCreateUpdate.Content.ToString();
            string zoekwoord = tbZoekCreateUpdate.Text;

            //Afhankelijk van het woord op de button, word er iets gedaan

            if (content == "Wijzig")
            {
                //Wijzig? Dan moet er iets gewijzigd worden
                _data.WijzigEenThema(tbZoekCreateUpdate.Text, _idtijdelijk);
                HaalAllesOp();
            }
            else if (content == "Herstel")
            {
                //Herstel? Dan willen we alles weer zien, dus halen we alles op
                HaalAllesOp();
                btZoekCreateUpdate.Content = "Zoek";
            }
            else if (content == "Voeg toe")
            {
                //Voeg toe? Dan maken we een nieuw thema aan
                _data.CreateThema(zoekwoord);
                HaalAllesOp();
            }
            else
            {
                //Iets anders? Dan stond er zoek en moeten we iets zoeken met het ingevulde zoekwoord
                DataTable themas = new DataTable();
                themas = _data.HaalThemasOpMetZoekwoord(zoekwoord);
                LoadDataTable(themas);
                btZoekCreateUpdate.Content = "Herstel";
            }

            tbZoekCreateUpdate.Text = "";
        }
    }
}
