using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class CamionBenne:PoidsLourd
    {
        int nbbennes;
        int grue;
        public CamionBenne(string imatriculation, string marque, double volume, string matérielTransporté,int nbbennes):base(imatriculation, marque, volume, matérielTransporté)
        {
            if (nbbennes != 1 && nbbennes != 2 && nbbennes != 3)
            {
                Console.WriteLine("nbbennes doit être 1, 2 ou 3");
            }
            else
            {
                this.nbbennes = nbbennes;
                this.grue = 1;
            }

        }
        public int Nbbennes {  get { return nbbennes; }
            set { nbbennes = value; }
        }
        public override string ToString()
        {
            return base.ToString()+ " Nombres de bennes: "+nbbennes;
        }




    }
}
