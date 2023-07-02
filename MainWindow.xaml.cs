using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Threading;
using System.Xml.Serialization;


namespace Z3_binding_do_klas
{
    public partial class MainWindow : Window
    {
        private const string sciezkaIO = "listaAlbumow.xml";
        private readonly ObservableCollection<Album> Albumy = new();
        MainListaAlbumow ListaAlbumow = new MainListaAlbumow();



        public MainWindow()
        {
            InitializeComponent();
            ImportujButton_Click(this, new RoutedEventArgs());
            DataContext = Albumy;

        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            Album nowy = new Album();
            Albumy.Add(nowy);
            new OknoDodajAlbum((Album)lista.SelectedItem).Show();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.ImportujButton_Click(sender, e);
            }

        }

        private void EdytujButton_Click(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedItem != null)
            {
                new OknoEdycjaAlbumu((Album)lista.SelectedItem).Show();
            }
        }

        private void UsunButton_Click(object sender, RoutedEventArgs e)
        {
            Albumy.Remove((Album)lista.SelectedItem);
        }

        private void ImportujButton_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializator = new XmlSerializer(typeof(ObservableCollection<Album>));
            FileStream plik = new FileStream(sciezkaIO, FileMode.Open);
            ObservableCollection<Album> wczytane
                = (ObservableCollection<Album>)serializator.Deserialize(plik);
            Albumy.Clear();
            foreach (Album osoba in wczytane)
                Albumy.Add(osoba);
            statusText.Text = "Import danych z listaAlbumow.xml, zakończony.";

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                statusText.Text = ""; // Wyczyść tekst
            };
            timer.Start();
            plik.Close();
        }


        private void EksportujButton_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializator = new XmlSerializer(typeof(ObservableCollection<Album>));
            TextWriter strumieńZapisu = new StreamWriter(sciezkaIO);
            serializator.Serialize(
                strumieńZapisu,
                Albumy
                );
            statusText.Text = "Eksport danych do listaAlbumow.xml, zakończony.";

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                statusText.Text = ""; // Wyczyść tekst
            };
            timer.Start();
            strumieńZapisu.Close();
        }

    }
}
