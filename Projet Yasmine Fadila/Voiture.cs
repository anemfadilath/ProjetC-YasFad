using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class Voiture:Vehicule
    {
        int passagers;

        public Voiture(string imatriculation, string marque,int passagers):base(imatriculation, marque)
        {
            this.passagers = passagers;
        }
        public override string ToString()
        {
            return base.ToString()+ " Nombre de passagers: " + passagers;
        }
        public int Passager {  
            get { return passagers; } 
            set { passagers = value; }
         }

    }
}
