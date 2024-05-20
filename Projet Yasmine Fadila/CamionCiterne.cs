using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class CamionCiterne:PoidsLourd
    {
        public string TypeCuve { get; set; }

        public CamionCiterne(string imatriculation, string marque, double volume, string matérielTransporté, string typeCuve): base(imatriculation, marque, volume, matérielTransporté)
        { 
           
            TypeCuve = typeCuve;
        }
        public override string ToString()
        {
            return base.ToString()+" Type de cuve: "+TypeCuve;
        }
    }
}
