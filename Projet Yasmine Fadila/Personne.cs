using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public abstract  class Personne
    {
        protected int numss;
        protected string nom;
        protected string prenom;
        protected Adresse adress;
        protected string telephone;

        public Personne(int numss, string nom, string prenom,  string telephone,string rue,string code,string ville)
        {
            this.numss = numss;
            this.nom = nom;
            this.prenom = prenom;
            this.adress = new Adresse(rue, code, ville);
            this.telephone = telephone;
        }

        public override string ToString()
        {
            return " Nom: "+ nom + " Prenom: " + prenom +" Adresse: " + adress.ToString() + " Telephone: " + telephone ;
        }

        public abstract string Nom {  get; }
    }
}
