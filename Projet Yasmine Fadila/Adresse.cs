using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public  class Adresse
    {
        string rue;
        string codePosatal;
        string ville;
        public Adresse(string rue, string codePosatal, string ville)
        {
            this.rue = rue;
            this.codePosatal = codePosatal;
            this.ville = ville;
        }

        public string Ville
        {
            get { return ville; }
        }
        public override string ToString()
        {
            return "Rue: "+rue+" Code Postal: "+codePosatal+" ville: "+ville;
        }
    }
}
