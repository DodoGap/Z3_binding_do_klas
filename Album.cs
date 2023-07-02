using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Z3_binding_do_klas
{
    public class Album : INotifyPropertyChanged
    {
        private string tytul;
        private string artysta;
        private string gatunek;

        public string Tytul
        {
            get { return tytul; }
            set
            {
                tytul = value;
                ZmienWlasciwosc();
            }
        }

        public string Artysta
        {
            get { return artysta; }
            set
            {
                artysta = value;
                ZmienWlasciwosc();
            }
        }

        public string Gatunek
        {
            get { return gatunek; }
            set
            {
                gatunek = value;
                ZmienWlasciwosc();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void ZmienWlasciwosc([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
