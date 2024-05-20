using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class Salarie : Personne
    {
        protected DateTime dateEntree;
        protected string poste;
        protected double salaire;
        public Salarie(int numss, string nom, string prenom, string telephone, string rue, string code, string ville, DateTime dateEntree, string poste, double salaire) : base(numss, nom, prenom,telephone,rue,code,ville)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }
        public Salarie(int numss, string nom, string prenom, string telephone, string rue, string code, string ville, DateTime dateEntree, string poste) : base(numss, nom, prenom, telephone,rue,code,ville)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
        }
        public double Salaire
        {
            get { return this.salaire; }
            set { this.salaire = value; }
        }
        public override string Nom
        {
            get { return this.nom; }
        }
        public string Poste
        {
              get  { return this.poste;} 
            set { this.poste = value; }
        }

        public string Telephone
        {
            get { return this.telephone; }
            set { this.telephone = value; }
        }
        public string Prenom
        {
            get { return this.prenom; }
        }

        public int Numss
        {
            get { return numss; }
        }
        public override string ToString()
        {
            return base.ToString() + "Date d'entrée:  " + dateEntree.ToLongDateString() + " Poste: " + poste;
        }

    }
}
