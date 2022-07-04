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
    /// Interaction logic for bhVerkiezingssoorten.xaml
    /// </summary>
    public partial class bhVerkiezingssoorten : Window
    {
        Class.Verkiezingen _data = new Class.Verkiezingen();
        string _selectedSoort = null;

        public bhVerkiezingssoorten()
        {
            InitializeComponent();

            HaalVerkiezingSoortenOp();
        }

        private void HaalVerkiezingSoortenOp()
        {
            DataTable volleTabel;
            StackPanel sp;
            int counter;

            volleTabel = _data.HaalAlleVerkiezingSoortenOp();

            int[] widths = { 0, 175 };
            string[] primaryKeys = { "verkiezingsoortId", null };

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
            DataTable soort;

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                // Als we het tekstblocks vinden met de juiste namen, dan gaan we dat getal in een variabele zetten. 
                if (item.Name == "verkiezingsoortId")
                {
                    _selectedSoort = item.Text;
                }
            }

            btAdd.Content = "Annuleren";
            btAdd.Background = Brushes.LightCoral;
            btVerzenden.Content = "Wijziggen";
            wpAdd.Visibility = Visibility.Visible;

            soort = _data.HaalVerkiezingSoortOpBijId(_selectedSoort);

            foreach (DataRow row in soort.Rows)
            {
                tbSoort.Text = row[1].ToString();
            }
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {
            string verkiezingSoortId;

            verkiezingSoortId = null;

            MessageBoxResult deleteOrNot = MessageBox.Show("Als je op ja klikt, wordt de verkiezingsoort verwijderd.\nWeet je het zeker?", "Verwijder verkiezingspartij", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                if (item.Name == "verkiezingsoortId")
                {
                    verkiezingSoortId = item.Text;
                }
            }

            // De verkiezingspartij verwijderen.
            if (!_data.DeleteVerkiezingSoort(verkiezingSoortId))
            {
                // Foutmelding geven als er iets fout gaat.
                MessageBox.Show("Er is iets fout gegaan met het verwijderen van de verkiezingsoort.", "Verwijder error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Omdat er iets is veranderd in de tabel, halen we opnieuw alles op
            HaalVerkiezingSoortenOp();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btAdd.Content.ToString() == "Toevoegen")
            {
                DataTable soorten;

                btAdd.Content = "Annuleren";
                btAdd.Background = Brushes.LightCoral;
                btVerzenden.Content = "Toevoegen";
                wpAdd.Visibility = Visibility.Visible;

                soorten = _data.HaalAlleVerkiezingSoortenOp();
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
                if (tbSoort.Text != "")
                {
                    // De verkiezingspartij toevoegen.
                    if (_data.VoegVerkiezingSoortToe(tbSoort.Text))
                    {
                        btAdd.Content = "Toevoegen";
                        btAdd.Background = Brushes.LightGreen;
                        wpAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan met het toevoegen van de verkiezingsoort.", "Toevoeg error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Voer alstublieft alle gegevens in.", "Toevoeg error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                if (tbSoort.Text != "")
                {
                    string soortId;

                    // De id's ophalen en in variabelen zetten.
                    soortId = tbSoort.Text;

                    // De verkiezingspartij toevoegen.
                    if (_data.UpdateVerkiezigSoort(_selectedSoort, tbSoort.Text))
                    {
                        btAdd.Content = "Toevoegen";
                        btAdd.Background = Brushes.LightGreen;
                        wpAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan met het Wijziggen van de verkiezingsoort.", "Wijzig error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vul alstublieft alle gegevens in.", "Wijzig error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            HaalVerkiezingSoortenOp();
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            this.Close();
            home.ShowDialog();
        }
    }
}