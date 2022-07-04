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
    /// Interaction logic for bhPartijen.xaml
    /// </summary>
    public partial class bhPartijen : Window
    {
        Verkiezingen _data = new Verkiezingen();
        bool _naarstandpunten = false;

        public bhPartijen()
        {
            //Zodra de window opstart, moeten we de functie haal alles op aanroepen omdat we alles uit de tabel partijen willen zien
            InitializeComponent();
            HaalAllesOp();
        }

        private void HaalAllesOp()
        {
            //We maken een datatable aan die we vullen met alles wat in de partijen tabel staat in de database
            DataTable allepartijen = new DataTable();
            allepartijen = _data.HaalAllePartijenOp();

            LoadDataTable(allepartijen); //Geef de datatable mee aan een andere eventhandler
        }

        private void btZoek_Click(object sender, RoutedEventArgs e)
        {
            //De zoekfunctie, we doen hetzelfde als met het opstarten, maar dan alleen degene met het betreffende zoekwoord
            DataTable allepartijen = new DataTable();
            string zoekop = "";
            string zoekwoord = tbZoek.Text;

            //Haal het geselecteerde item op uit de combobox
            ComboBoxItem selectedItem = (ComboBoxItem)cbZoek.SelectedItem;
            if (selectedItem == null)
            {
                zoekop = "naam";
            }
            else
            {
                zoekop = selectedItem.Content.ToString();
            }

            //We halen alles op met het ingevulde zoekwoord en hetgeen waarop we moeten zoeken (naam, adres etc.)
            allepartijen = _data.HaalPartijenOpMetZoekwoord(zoekop, zoekwoord);
            LoadDataTable(allepartijen);
        }

        private void LoadDataTable(DataTable allepartijen)
        {
            //Hiermee laten we alles op het scherm zien met de datatable die we hebben meegekregen
            databaseitems.Items.Clear();
            int counter = 0;

            //Voor iedere rij maken we een stackpanel met tekstblokken erin, deze voegen we toe aan de listbox (overzichtelijk!)
            while (counter < allepartijen.Rows.Count)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.HorizontalAlignment = HorizontalAlignment.Center;
                panel.VerticalAlignment = VerticalAlignment.Top;
                databaseitems.Items.Add(panel);

                TextBlock id = new TextBlock();
                id.Name = "id";
                id.Opacity = 0;
                id.Width = 0;
                id.Text = allepartijen.Rows[counter]["partij_id"].ToString();

                TextBlock name = new TextBlock();
                name.Width = 120;
                name.Name = "Naam";
                name.TextAlignment = TextAlignment.Center;
                name.Text = allepartijen.Rows[counter]["naam"].ToString();

                TextBlock adres = new TextBlock();
                adres.Width = 120;
                adres.Name = "Adres";
                adres.TextAlignment = TextAlignment.Center;
                adres.Text = allepartijen.Rows[counter]["adres"].ToString();

                TextBlock postcode = new TextBlock();
                postcode.Width = 80;
                postcode.Name = "Postcode";
                postcode.TextAlignment = TextAlignment.Center;
                postcode.Text = allepartijen.Rows[counter]["postcode"].ToString();

                TextBlock gemeente = new TextBlock();
                gemeente.Width = 120;
                gemeente.Name = "Gemeente";
                gemeente.TextAlignment = TextAlignment.Center;
                gemeente.Text = allepartijen.Rows[counter]["gemeente"].ToString();

                TextBlock emailadres = new TextBlock();
                emailadres.Width = 120;
                emailadres.Name = "Email";
                emailadres.TextAlignment = TextAlignment.Center;
                emailadres.Text = allepartijen.Rows[counter]["emailadres"].ToString();

                TextBlock telefoonnr = new TextBlock();
                telefoonnr.Width = 100;
                telefoonnr.Name = "Telefoon";
                telefoonnr.TextAlignment = TextAlignment.Center;
                telefoonnr.Text = allepartijen.Rows[counter]["telefoonnummer"].ToString();

                Button wijzig = new Button();
                wijzig.Click += Wijzig_Click;
                wijzig.Width = 80;
                wijzig.Content = "Wijzig";

                Button verwijder = new Button();
                verwijder.Click += Verwijder_Click;
                verwijder.Width = 80;
                verwijder.Content = "Verwijder";

                panel.Children.Add(name);
                panel.Children.Add(adres);
                panel.Children.Add(postcode);
                panel.Children.Add(gemeente);
                panel.Children.Add(emailadres);
                panel.Children.Add(telefoonnr);
                panel.Children.Add(wijzig);
                panel.Children.Add(verwijder);
                panel.Children.Add(id);

                counter++;
            }

            if (_naarstandpunten == true)
            {
                counter--;
                DataTable idPartij = new DataTable();
                idPartij = _data.HaalIDvanEenPartijOp(allepartijen.Rows[counter]["partij_id"].ToString());

                answerstandpunt answer = new answerstandpunt(idPartij.Rows[0]["partij_id"].ToString());
                this.Close();
                answer.ShowDialog();
            }
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult deleteOrNot = MessageBox.Show("Als je de partij verwijderd, verwijder je ook alle meningen van deze partij en haal je ze ook uit alle verkiezingen! \n Weet je het zeker?", "Verwijder partij", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (deleteOrNot == MessageBoxResult.No)
            {
                return;
                //Zegt de gebruiker toch nee? Dan stopt de code hier en word er niets verwijderd
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
                    //Als we het tekstblock vinden met de naam id, dan gaan we dat getal parsen. 
                    //Vervolgens geven we dat getal mee en word de partij met dat id verwijderd.
                    int id = Int32.Parse(item.Text);
                    _data.VerwijderPartijUitStandpunt(id);
                    //Eerst moet de partij verwijderd worden uit de partij_standpunten-tabel
                    _data.VerwijderPartijUitVerkiezing(id);
                    //Daarna word de partij ook weggehaald uit de partij_verkiezing tabel
                    _data.DeleteEenPartij(id);
                    //Daarna verwijderen we de partij zelf
                }
            }

            //Omdat er iets is veranderd in de tabel, halen we opnieuw alles op
            HaalAllesOp();
        }

        private void Wijzig_Click(object sender, RoutedEventArgs e)
        {
            //We zoeken de button uit het betreffende stackpanel
            Button update = e.Source as Button;
            StackPanel parent = (StackPanel)update.Parent;

            //Alles wat de gebruiker moet zien om iets te bewerken, word op visible gezet (of juist op hidden omdat iets niet gezien mag worden)

            wpUpdate.Visibility = Visibility.Visible;
            spZoek.Visibility = Visibility.Hidden;
            tbStatus.Visibility = Visibility.Visible;
            btWijzig.Visibility = Visibility.Visible;
            btWijzig.Content = "Wijzig";
            tbStatus.Text = "Wijzig partij";

            //We halen alles op, omdat in principe alles gewijzigd kan zijn en dus alles ook geupdate moet worden

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                if (item.Name == "Naam")
                {
                    tbNaam.Text = item.Text;
                }
                else if (item.Name == "Adres")
                {
                    tbAdres.Text = item.Text;
                }
                else if (item.Name == "Postcode")
                {
                    tbPostcode.Text = item.Text;
                }
                else if (item.Name == "Gemeente")
                {
                    tbGemeente.Text = item.Text;
                }
                else if (item.Name == "Email")
                {
                    tbEmail.Text = item.Text;
                }
                else if (item.Name == "Telefoon")
                {
                    tbTelefoon.Text = item.Text;
                }
                else if (item.Name == "id")
                {
                    tbHiddenID.Text = item.Text;
                }
            }
            
        }

        private void btWijzig_Click(object sender, RoutedEventArgs e)
        {
            //Zodra de gebruiker wijzigingen heeft aangebracht, klikt hij op de button en komen we in deze eventhandler
            int id = 0;
            string naam = "";
            string adres = "";
            string postcode = "";
            string gemeente = "";
            string email = "";
            string telefoonnr = "";

            try
            {
                id = Int32.Parse(tbHiddenID.Text);
                naam = tbNaam.Text;
                adres = tbAdres.Text;
                postcode = tbPostcode.Text;
                gemeente = tbGemeente.Text;
                email = tbEmail.Text;
                telefoonnr = tbTelefoon.Text;
                //We controleren of alle waardes geldig zijn door ze in een int of string te zetten
            }
            catch (Exception)
            {
                MessageBox.Show("Oeps, er ging iets mis");
                return;
            }


            /*In deze method kunnen zowel partijen gewijzigd worden als nieuwe partijen worden aangemaakt.
            De content van het tekstblok word daarom gewijzigd wanneer de gebruiker klikt op update
            
            Wanneer de gebruiker iets wil wijzigen, zal de tekst "Wijzig partij" boven komen te staan (tbStatus)
            Wanneer de gebruiker iets wil aanmaken, zal de tekst veranderen waardoor we niet in de if hieronder komen

            Op deze manier word ofwel iets gewijzigd of iets aangemaakt
             */

            if (tbStatus.Text == "Wijzig partij")
            {
                try
                {
                    _data.WijzigEenPartij(id, naam, adres, postcode, gemeente, email, telefoonnr);

                    wpUpdate.Visibility = Visibility.Hidden;
                    tbStatus.Visibility = Visibility.Hidden;
                    btWijzig.Visibility = Visibility.Hidden;
                    spZoek.Visibility = Visibility.Visible;
                    btWijzig.Content = "Wijzig";
                    HaalAllesOp();
                }
                catch (Exception)
                {
                    MessageBox.Show("Oops, er ging iets mis");
                    return;
                }
            }
            else
            {
                try
                {
                    _data.CreatePartij(naam, adres, postcode, gemeente, email, telefoonnr);

                    wpUpdate.Visibility = Visibility.Hidden;
                    tbStatus.Visibility = Visibility.Hidden;
                    btWijzig.Visibility = Visibility.Hidden;
                    btWijzig.Content = "Wijzig";

                    _naarstandpunten = true;
                    HaalAllesOp();
                }
                catch (Exception)
                {
                    MessageBox.Show("Oops, er ging iets mis");
                    return;
                }
            }
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            //Ga terug naar het mainwindow
            MainWindow home = new MainWindow();
            this.Close();
            home.ShowDialog();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            //We zorgen dat alles wat nodig is om een partij te creeren zichtbaar word gemaakt
            spZoek.Visibility = Visibility.Hidden;
            wpUpdate.Visibility = Visibility.Visible;
            tbStatus.Visibility = Visibility.Visible;
            btWijzig.Visibility = Visibility.Visible;
            btWijzig.Content = "Creëer";
            tbStatus.Text = "Nieuwe partij";
            tbNaam.Text = "";
            tbAdres.Text = "";
            tbPostcode.Text = "";
            tbGemeente.Text = "";
            tbEmail.Text = "";
            tbTelefoon.Text = "";
        }
    }
}
