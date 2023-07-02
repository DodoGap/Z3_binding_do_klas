﻿using System;
using System.Collections.Generic;
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

namespace Z3_binding_do_klas
{
    public partial class OknoDodajAlbum : Window
    {
        public OknoDodajAlbum(Album album)
        {
            InitializeComponent();
            DataContext = album;
        }
        private void OK(object sender, RoutedEventArgs e)
        {
            string tytul = box_tytul.Text;
            string artysta = box_artysta.Text;
            string gatunek = box_gatunek.Text;

            MainListaAlbumow mainListaAlbumow = new MainListaAlbumow();
            mainListaAlbumow.DodajAlbum(tytul, artysta, gatunek);
        }

    }
}
