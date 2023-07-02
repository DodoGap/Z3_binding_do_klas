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
using System.Xml.Serialization;

namespace Z3_binding_do_klas
{
    public partial class MainWindow : Window
    {
        private const string sciezkaIO = "listaAlbumow.xml";
        private readonly ObservableCollection<Album> Albumy = new();


        public MainWindow()
        {
            InitializeComponent();
            lista = FindName("lista") as ListBox;
            DataContext = Albumy;


            // Deserializacja danych z pliku XML i przypisanie ich do kolekcji Albumy
            XmlSerializer serializer = new XmlSerializer(typeof(List<Album>));
            using (FileStream stream = new FileStream(sciezkaIO, FileMode.Open))
            {
                List<Album> deserializedAlbumy = (List<Album>)serializer.Deserialize(stream);
                foreach (Album album in deserializedAlbumy)
                {
                    Albumy.Add(album);
                }
            }
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            Album nowyAlbum = new Album();
            DodajAlbum oknoDodaj = new DodajAlbum(nowyAlbum);
            if (oknoDodaj.ShowDialog() == true)
            {
                viewModel.DodajAlbum(nowyAlbum);
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
            Albumy.Remove(
                (Album)lista.SelectedItem
                );
        }

        private void ImportujButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImportujAlbumy();
        }

        private void EksportujButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EksportujAlbumy();
        }

    }
}
