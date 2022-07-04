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

namespace WpfAppProject3
{
    /// <summary>
    /// Interaction logic for bhVerkiezingen.xaml
    /// </summary>
    public partial class bhVerkiezingen : Window
    {
        Class.Verkiezingen _data = new Class.Verkiezingen();
        string _selectedVerkiezingId = null;

        public bhVerkiezingen()
        {
            InitializeComponent();

            HaalVerkiezingenOp();
        }

        private void HaalVerkiezingenOp()
        {
            DataTable volleTabel;
            StackPanel sp;
            int counter;

            volleTabel = _data.haalAlleVerkiezingenOp();

            int[] widths = { 118, 156, 137 };
            string[] primaryKeys = { "verkiezingId", null, null };

            databaseitems.Items.Clear();

            foreach (DataRow row in volleTabel.Rows)
            {
                counter = 0;

                sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Background = Brushes.White;

                foreach (object data in row.ItemArray)
                {
                    sp.Children.Add(MaakTextBlock(widths[counter], primaryKeys[counter], data.ToString()));

                    counter++;
                }

                sp.Children.Add(MaakButton("Wijzig"));
                sp.Children.Add(MaakButton("Verwijderen"));

                databaseitems.Items.Add(sp);
            }
        }

        private TextBlock MaakTextBlock(int width, string primaryKey, string data)
        {
            TextBlock result = new TextBlock();
            result.Width = width;
            result.Text = data;
            result.Name = primaryKey;

            return result;
        }

        private Button MaakButton(string optie)
        {
            Button result = new Button();
            result.Width = 77;
            result.Content = optie;

            if (optie == "Wijzig")
            {
                result.Click += Wijzig_Click;
            }
            else
            {
                result.Click += Verwijder_Click;
            }

            return result;
        }

        private void Wijzig_Click(object sender, RoutedEventArgs e)
        {
            Button wijzig = e.Source as Button;
            StackPanel parent = (StackPanel)wijzig.Parent;
            DataTable soorten, verkiezingSoort, verkiezing;
            ComboBoxItem cbi;
            string datum, selectedVerkiezingSoortId;
            string[] dmj;
            int counter;

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                // Als we het tekstblocks vinden met de juiste namen, dan gaan we dat getal in een variabele zetten. 
                if (item.Name == "verkiezingId")
                {
                    _selectedVerkiezingId = item.Text;
                }
            }

            btAdd.Content = "Annuleren";
            btAdd.Background = Brushes.LightCoral;
            cbSoort.Items.Clear();
            btVerzenden.Content = "Wijziggen";
            wpAdd.Visibility = Visibility.Visible;

            verkiezingSoort = _data.haalVerkiezingSoortIdOp(_selectedVerkiezingId);
            selectedVerkiezingSoortId = null;
            foreach (DataRow row in verkiezingSoort.Rows)
            {
                selectedVerkiezingSoortId = row[0].ToString();
            }

            soorten = _data.HaalAlleVerkiezingSoortenOp();
            counter = 0;
            foreach (DataRow row in soorten.Rows)
            {
                cbi = new ComboBoxItem();
                cbi.Content = row[1].ToString();
                cbSoort.Items.Add(cbi);
                if (row[0].ToString() == selectedVerkiezingSoortId)
                {
                    cbSoort.SelectedIndex = counter;
                }
                counter++;
            }

            verkiezing = _data.haalVerkiezingOpBijId(_selectedVerkiezingId);
            foreach (DataRow row in verkiezing.Rows)
            {
                dmj = row[2].ToString().Split(' ');
                dmj = dmj[0].Split('-');
                datum = dmj[2] + '/' + dmj[1] + '/' + dmj[0];

                dpDatum.SelectedDate = DateTime.Parse(datum);
            }
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {
            string verkiezingId;

            verkiezingId = null;

            MessageBoxResult deleteOrNot = MessageBox.Show("Als je op ja klikt, wordt de verkiezing verwijderd.\nWeet je het zeker?", "Verwijder verkiezing", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (deleteOrNot == MessageBoxResult.No)
            {
                return;
                //Zegt de gebruiker toch nee? Dan stopt de code hier en word er niets verwijderd
            }

            //Haal de button op waar op geklikt is, inclusief het stackpanel waar die button in zit
            Button verwijder = e.Source as Button;
            StackPanel parent = (StackPanel)verwijder.Parent;

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                // Als we het tekstblock vinden met de juiste naam, dan gaan we dat getal in een variabele zetten. 
                if (item.Name == "verkiezingId")
                {
                    verkiezingId = item.Text;
                }
            }

            // De verkiezingspartij verwijderen.
            if (!_data.DeleteVerkiezing(verkiezingId))
            {
                // Foutmelding geven als er iets fout gaat.
                MessageBox.Show("Er is iets fout gegaan met het verwijderen van de verkiezing.", "Verwijder error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Omdat er iets is veranderd in de tabel, halen we opnieuw alles op
            HaalVerkiezingenOp();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btAdd.Content.ToString() == "Toevoegen")
            {
                DataTable soorten;
                ComboBoxItem cbi;

                btAdd.Content = "Annuleren";
                btAdd.Background = Brushes.LightCoral;
                cbSoort.Items.Clear();
                btVerzenden.Content = "Toevoegen";
                wpAdd.Visibility = Visibility.Visible;

                soorten = _data.HaalAlleVerkiezingSoortenOp();

                foreach (DataRow row in soorten.Rows)
                {
                    cbi = new ComboBoxItem();
                    cbi.Content = row[1].ToString();
                    cbSoort.Items.Add(cbi);
                }

                cbSoort.SelectedIndex = 0;
            }
            else
            {
                btAdd.Content = "Toevoegen";
                btAdd.Background = Brushes.LightGreen;
                wpAdd.Visibility = Visibility.Hidden;
            }
        }

        private void btVerzenden_Click(object sender, RoutedEventArgs e)
        {
            if (btVerzenden.Content.ToString() == "Toevoegen")
            {
                if (cbSoort.SelectedIndex >= 0 && dpDatum.Text != "")
                {
                    string soort, datum;
                    string[] dmj;
                    DataTable Soorten;

                    // De id's ophalen en in variabelen zetten.
                    dmj = dpDatum.SelectedDate.ToString().Split(' ');
                    dmj = dmj[0].Split('-');
                    datum = dmj[2] + '-' + dmj[1] + '-' + dmj[0];
                    soort = null;
                    Soorten = _data.HaalVerkiezingSoortOpBijNaam(cbSoort.Text);
                    foreach (DataRow row in Soorten.Rows)
                    {
                        soort = row[0].ToString();
                    }

                    // De verkiezingspartij toevoegen.
                    if (_data.VoegVerkiezingToe(soort, datum))
                    {
                        btAdd.Content = "Toevoegen";
                        btAdd.Background = Brushes.LightGreen;
                        wpAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan met het toevoegen van de verkiezing.", "Toevoeg error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Voer alstublieft alle gegevens in.", "Toevoeg error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                if (cbSoort.SelectedIndex >= 0 && dpDatum.Text != "")
                {
                    string soort, datum;
                    string[] dmj;
                    DataTable Soorten;

                    // De id's ophalen en in variabelen zetten.
                    dmj = dpDatum.SelectedDate.ToString().Split(' ');
                    dmj = dmj[0].Split('-');
                    datum = dmj[2] + '-' + dmj[1] + '-' + dmj[0];
                    soort = null;
                    Soorten = _data.HaalVerkiezingSoortOpBijNaam(cbSoort.Text);
                    foreach (DataRow row in Soorten.Rows)
                    {
                        soort = row[0].ToString();
                    }

                    // De verkiezingspartij toevoegen.
                    if (_data.UpdateVerkiezig(_selectedVerkiezingId, soort, datum))
                    {
                        btAdd.Content = "Toevoegen";
                        btAdd.Background = Brushes.LightGreen;
                        wpAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan met het Wijziggen van de verkiezingspartij.", "Wijzig error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vul alstublieft alle gegevens in.", "Wijzig error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            HaalVerkiezingenOp();
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            this.Close();
            home.ShowDialog();
        }
    }
}