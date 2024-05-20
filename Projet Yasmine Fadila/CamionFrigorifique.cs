using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class CamionFrigorifique:PoidsLourd
    {
        public int NombreGroupesElectrogenes { get; set; }

        public CamionFrigorifique(string imatriculation, string marque, double volume, string matérielTransporté, int nombreGroupesElectrogenes)
            : base(imatriculation, marque, volume, matérielTransporté) { 
            NombreGroupesElectrogenes = nombreGroupesElectrogenes;
        }

        public override string ToString()
        {
            return base.ToString()+ " Nombre de groupes electrogènes: "+NombreGroupesElectrogenes;

        }
    }
}
