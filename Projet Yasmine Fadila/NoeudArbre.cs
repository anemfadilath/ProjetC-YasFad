using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public  class NoeudArbre
    {
       public  Salarie salarie;
        public List<NoeudArbre> subordonnes;
        public NoeudArbre(Salarie salarier)
        {
            this.salarie = salarier;
            subordonnes = new List<NoeudArbre>();
        }

        public Salarie Salarie {  get { return salarie; }
            set { salarie = value; }
              }
        public List<NoeudArbre> Subordonnes { get { return subordonnes; } }
    }
}
