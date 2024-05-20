using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class NoeudGenerique<T> where T : IComparable<T>
    {
        T donnee;
        NoeudGenerique<T> suivant;
        public T Donnee
        {
            get { return donnee; }
            set { donnee = value; }
        }

        public NoeudGenerique<T> Suivant
        {
            get { return suivant; }
            set { suivant = value; }
        }
    }
}
