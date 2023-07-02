using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Z3_binding_do_klas
{
    public class MainListaAlbumow : INotifyPropertyChanged
    {
        private const string sciezkaIO = "./listaAlbumow.xml";
        public ObservableCollection<Album> ListaAlbumow { get; set; } = new ObservableCollection<Album>();

        private Album wybranyAlbum;
        public Album WybranyAlbum
        {
            get { return wybranyAlbum; }
            set
            {
                wybranyAlbum = value;
                OnPropertyChanged();
            }
        }

        public MainListaAlbumow()
        {
            ImportujAlbumy();
        }

        public void DodajAlbum(string tytul, string artysta, string gatunek)
        {
            Album album = new Album
            {
                Tytul = tytul,
                Artysta = artysta,
                Gatunek = gatunek
            };

            ListaAlbumow.Add(album);
            ZapiszAlbumy();
        }

        public void EdytujAlbum(Album album)
        {
            ZapiszAlbumy();
        }

        public void UsunAlbum(Album album)
        {
            ListaAlbumow.Remove(album);
            ZapiszAlbumy();
        }

        public void ImportujAlbumy()
        {
            if (File.Exists(sciezkaIO))
            {
                XmlSerializer serializator = new XmlSerializer(typeof(ObservableCollection<Album>));
                using (FileStream strumienOdczytu = new FileStream(sciezkaIO, FileMode.Open))
                {
                    ObservableCollection<Album> albumy = (ObservableCollection<Album>)serializator.Deserialize(strumienOdczytu);
                    ListaAlbumow = albumy;
                }
            }
        }

        public void ZapiszAlbumy()
        {
            XmlSerializer serializator = new XmlSerializer(typeof(ObservableCollection<Album>));
            using (TextWriter strumienZapisu = new StreamWriter(sciezkaIO))
            {
                serializator.Serialize(strumienZapisu, ListaAlbumow);
            }
        }

        public void EksportujAlbumy()
        {
            XmlSerializer serializator = new XmlSerializer(typeof(ObservableCollection<Album>));
            using (TextWriter strumienZapisu = new StreamWriter(sciezkaIO))
            {
                serializator.Serialize(strumienZapisu, ListaAlbumow);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
