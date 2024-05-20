using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class Camionnette:Vehicule
    {
        private string usage;
        //public void SpécifierUsageCamionnette()
        //{
     
        //    string usage = Console.ReadLine();
        //}
        public Camionnette(string imatriculation, string marque,string usage):base(imatriculation, marque)
        {
            this.usage=usage;
        }
        public string Usage {  
            get { return usage; } 
            set { usage = value; }
        }
        public override string ToString()
        {
            return base.ToString()+" Usage : " + usage;
        }
    }
}
