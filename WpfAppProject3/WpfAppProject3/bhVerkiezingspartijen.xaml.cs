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
    /// Interaction logic for bhVerkiezingspartijen.xaml
    /// </summary>
    public partial class bhVerkiezingspartijen : Window
    {
        Class.Verkiezingen _data = new Class.Verkiezingen();
        string _selectedPartijId = null;
        string _selectedVerkiezingId = null;

        public bhVerkiezingspartijen()
        {
            InitializeComponent();

            HaalVerkiezingsPartijenOp();
        }

        private void HaalVerkiezingsPartijenOp()
        {
            DataTable volleTabel;
            StackPanel sp;
            int counter;

            volleTabel = _data.HaalVerkiezingsPartijenOp();

            int[] widths = { 0, 128, 118, 157 };
            string[] primaryKeys = { "partijId", null, "verkiezingId", null };

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
            DataTable partijen, verkiezingen;
            ComboBoxItem cbi;
            int counter;

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                // Als we het tekstblocks vinden met de juiste namen, dan gaan we dat getal in een variabele zetten. 
                if (item.Name == "partijId")
                {
                    _selectedPartijId = item.Text;
                }
                else if (item.Name == "verkiezingId")
                {
                    _selectedVerkiezingId = item.Text;
                }
            }

            btAdd.Content = "Annuleren";
            btAdd.Background = Brushes.LightCoral;
            cbPartij.Items.Clear();
            cbVerkiezing.Items.Clear();
            btVerzenden.Content = "Wijziggen";
            wpAdd.Visibility = Visibility.Visible;

            partijen = _data.HaalAllePartijenOp();
            verkiezingen = _data.haalAlleVerkiezingenOp();

            counter = 0;
            foreach (DataRow row in partijen.Rows)
            {
                cbi = new ComboBoxItem();
                cbi.Content = row[0].ToString() + " - " + row[1].ToString();
                cbPartij.Items.Add(cbi);
                if (row[0].ToString() == _selectedPartijId)
                {
                    cbPartij.SelectedIndex = counter;
                }
                counter++;
            }

            counter = 0;
            foreach (DataRow row in verkiezingen.Rows)
            {
                cbi = new ComboBoxItem();
                cbi.Content = row[0].ToString() + " - " + row[1].ToString();
                cbVerkiezing.Items.Add(cbi);
                if (row[0].ToString() == _selectedVerkiezingId)
                {
                    cbVerkiezing.SelectedIndex = counter;
                }
                counter++;
            }
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {
            string partijId, verkiezingId;

            partijId = null;
            verkiezingId = null;

            MessageBoxResult deleteOrNot = MessageBox.Show("Als je op ja klikt, wordt de verkiezingspartij verwijderd.\nWeet je het zeker?", "Verwijder verkiezingspartij", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                // Als we de tekstblocks vinden met de juiste namen, dan gaan we dat getal in een variabele zetten. 
                if (item.Name == "partijId")
                {
                    partijId = item.Text;
                }
                else if (item.Name == "verkiezingId")
                {
                    verkiezingId = item.Text;
                }
            }

            // De verkiezingspartij verwijderen.
            if (!_data.DeleteVerkiezingsPartij(partijId, verkiezingId))
            {
                // Foutmelding geven als er iets fout gaat.
                MessageBox.Show("Er is iets fout gegaan met het verwijderen van de verkiezingspartij.", "Verwijder error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Omdat er iets is veranderd in de tabel, halen we opnieuw alles op
            HaalVerkiezingsPartijenOp();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btAdd.Content.ToString() == "Toevoegen")
            {
                DataTable partijen, verkiezingen;
                ComboBoxItem cbi;

                btAdd.Content = "Annuleren";
                btAdd.Background = Brushes.LightCoral;
                cbPartij.Items.Clear();
                cbVerkiezing.Items.Clear();
                btVerzenden.Content = "Toevoegen";
                wpAdd.Visibility = Visibility.Visible;

                partijen = _data.HaalAllePartijenOp();
                verkiezingen = _data.haalAlleVerkiezingenOp();

                foreach (DataRow row in partijen.Rows)
                {
                    cbi = new ComboBoxItem();
                    cbi.Content = row[0].ToString() + " - " + row[1].ToString();
                    cbPartij.Items.Add(cbi);
                }

                foreach (DataRow row in verkiezingen.Rows)
                {
                    cbi = new ComboBoxItem();
                    cbi.Content = row[0].ToString() + " - " + row[1].ToString();
                    cbVerkiezing.Items.Add(cbi);
                }

                cbPartij.SelectedIndex = 0;
                cbVerkiezing.SelectedIndex = 0;
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
                if (cbPartij.SelectedIndex >= 0 && cbVerkiezing.SelectedIndex >= 0)
                {
                    string partijId, verkiezingId;

                    // De id's ophalen en in variabelen zetten.
                    partijId = cbPartij.Text.Split(' ')[0];
                    verkiezingId = cbVerkiezing.Text.Split(' ')[0];

                    // De verkiezingspartij toevoegen.
                    if (_data.VoegVerkiezingsPartijToe(partijId, verkiezingId))
                    {
                        btAdd.Content = "Toevoegen";
                        btAdd.Background = Brushes.LightGreen;
                        wpAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan met het toevoegen van de verkiezingspartij.\nMisschien bestaat deze verkiezingspartij al?", "Toevoeg error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Er is iets fout gegaan met het selecteren van de nieuwe verkiezingspartij.", "Toevoeg error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (cbPartij.SelectedIndex >= 0 && cbVerkiezing.SelectedIndex >= 0)
                {
                    string partijId, verkiezingId;

                    // De id's ophalen en in variabelen zetten.
                    partijId = cbPartij.Text.Split(' ')[0];
                    verkiezingId = cbVerkiezing.Text.Split(' ')[0];

                    // De verkiezingspartij toevoegen.
                    if (_data.UpdateVerkiezigParijen(_selectedPartijId, _selectedVerkiezingId, partijId, verkiezingId))
                    {
                        btAdd.Content = "Toevoegen";
                        btAdd.Background = Brushes.LightGreen;
                        wpAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan met het Wijziggen van de verkiezingspartij.\nMisschien bestaat deze verkiezingspartij al?", "Wijzig error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Er is iets fout gegaan met het selecteren van de nieuwe verkiezingspartij.", "Wijzig error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            HaalVerkiezingsPartijenOp();
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            this.Close();
            home.ShowDialog();
        }
    }
}