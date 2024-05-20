using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public abstract class Vehicule
    {
        string imatriculation;
        string marque;
        public Vehicule(string imatriculation,string marque) {
            this.imatriculation = imatriculation;
            this.marque = marque;

        }

        public override string ToString()
        {
            return "Marque: " + marque + " Imatriculation: " + imatriculation;
        }

    }
}
