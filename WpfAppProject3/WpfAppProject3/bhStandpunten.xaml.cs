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
    /// Interaction logic for bhStandpunten.xaml
    /// </summary>
    public partial class bhStandpunten : Window
    {
        Verkiezingen _data = new Verkiezingen();
        bool _createorupdate = false;

        public bhStandpunten()
        {
            InitializeComponent();
            HaalAllesOp();
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            this.Close();
            home.ShowDialog();
        }

        public void HaalAllesOp()
        {
            //We maken een datatable aan die we vullen met alles wat in de partijen tabel staat
            DataTable allestandpunten = new DataTable();
            allestandpunten = _data.HaalAlleStandpuntenOp();

            DataTable themas = new DataTable();
            themas = _data.HaalAlleThemasOp();

            LoadDataTable(allestandpunten); //Geef de datatable mee aan een andere eventhandler
            FillComboBoxThemas(themas);
        }

        private void FillComboBoxThemas(DataTable themas)
        {
            cbThemas.Items.Clear();
            int counter = 0;
            while (counter < themas.Rows.Count)
            {
                ComboBoxItem thema = new ComboBoxItem();
                thema.FontSize = 20;
                thema.Content = themas.Rows[counter]["thema"].ToString();
                cbThemas.Items.Add(thema);

                counter++;
            }
        }

        private void LoadDataTable(DataTable allestandpunten)
        {
            //Hiermee laten we alles op het scherm zien met de datatable die we hebben meegekregen
            databaseitems.Items.Clear();
            int counter = 0;

            //Voor iedere rij maken we een stackpanel met tekstblokken erin, deze voegen we toe aan de listbox (overzichtelijk!)
            while (counter < allestandpunten.Rows.Count)
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
                id.Text = allestandpunten.Rows[counter]["standpunt_id"].ToString();

                TextBlock thema = new TextBlock();
                thema.Width = 200;
                thema.FontSize = 14;
                thema.Name = "thema";
                thema.TextAlignment = TextAlignment.Center;
                thema.Text = allestandpunten.Rows[counter]["thema"].ToString();

                TextBlock standpunt = new TextBlock();
                standpunt.Width = 500;
                standpunt.TextWrapping = TextWrapping.Wrap;
                standpunt.FontSize = 12;
                standpunt.Name = "standpunt";
                standpunt.TextAlignment = TextAlignment.Center;
                standpunt.Text = allestandpunten.Rows[counter]["standpunt"].ToString();

                Button wijzig = new Button();
                wijzig.Click += Wijzig_Click;
                wijzig.Width = 80;
                wijzig.Content = "Wijzig";

                Button verwijder = new Button();
                verwijder.Click += Verwijder_Click;
                verwijder.Width = 80;
                verwijder.Content = "Verwijder";

                panel.Children.Add(standpunt);
                panel.Children.Add(thema);
                panel.Children.Add(wijzig);
                panel.Children.Add(verwijder);
                panel.Children.Add(id);

                counter++;
            }
        }

        private void btZoek_Click(object sender, RoutedEventArgs e)
        {
            if (btZoek.Content.ToString() != "Zoek")
            {
                //Wanneer er geen "Zoek" op de button staat, dan moeten we alles weer ophalen (Omdat er "Herstel" stond)
                HaalAllesOp();
                btZoek.Content = "Zoek"; //We zetten de content op zoek zodat we bij de volgende klik niet hier terecht komen
                return;
            }

            ComboBoxItem selectedItem = (ComboBoxItem)cbThemas.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Selecteer aub waar je naar wilt zoeken!");
                return;
            }
            string selected = selectedItem.Content.ToString();

            DataTable standpunten = new DataTable();
            standpunten = _data.HaalStandpuntOpMetZoekwoord(selected);

            LoadDataTable(standpunten);
            btZoek.Content = "Herstel";
            cbThemas.SelectedIndex = -1;
            //De selectedindex komt op -1, op deze manier is er niets meer geselecteerd in de combobox
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult deleteOrNot = MessageBox.Show("Als je het standpunt verwijderd, verwijder je ook alle meningen van de partijen van dit standpunt. \n Weet je het zeker?", "Verwijder standpunt", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
                    int id = Int32.Parse(item.Text);
                    _data.DeleteEenStandpunt(id);
                }
            }
            //Omdat er iets is veranderd in de tabel, halen we opnieuw alles op
            HaalAllesOp();
        }

        /* Onderstaande twee handlers gaan over het wijzigen en aanmaken. 
           We gaan bij beide eventhandlers naar hetzelfde window, en we geven een boolean mee naar de nieuwe window.
           Staat deze op true? Dan weten we in het nieuwe window dat we iets willen creeren
           Maar staat deze op false? Dan weten we dat we iets willen wijzigen.
         */

        private void Wijzig_Click(object sender, RoutedEventArgs e)
        {
            //We zoeken de button uit het betreffende stackpanel

            Button update = e.Source as Button;
            StackPanel parent = (StackPanel)update.Parent;

            //Haal alle gegevens op die belangrijk zijn

            int id = 0;
            string thema = "";
            string standpunt = "";

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                if (item.Name == "id")
                {
                    id = Int32.Parse(item.Text);
                }
                else if (item.Name == "thema")
                {
                    thema = item.Text;
                }
                else if (item.Name == "standpunt")
                {
                    standpunt = item.Text;
                }
            }

            _createorupdate = false;

            bhWijzigStandpunt wijzigstandpunt = new bhWijzigStandpunt(id, thema, standpunt, _createorupdate);
            this.Close();
            wijzigstandpunt.ShowDialog();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            string thema = "";
            string standpunt = "";

            _createorupdate = true;

            bhWijzigStandpunt wijzigstandpunt = new bhWijzigStandpunt(id, thema, standpunt, _createorupdate);
            this.Close();
            wijzigstandpunt.ShowDialog();
        }
    }
}
