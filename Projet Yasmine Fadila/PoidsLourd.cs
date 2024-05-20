using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public abstract class PoidsLourd:Vehicule
    {
        
         double Volume { get; set; }
        string MatérielTransporté { get; set; }

        public PoidsLourd(string imatriculation, string marque, double volume, string matérielTransporté) : base(imatriculation, marque)
        {
           
            this.Volume = volume;
            this.MatérielTransporté = matérielTransporté;
        }
        public override string ToString()
        {
            return base.ToString()+" Matériel transporté: "+MatérielTransporté+" Volume "+Volume;
        }
    }
}
