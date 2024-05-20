using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public  class Arbre
    {
         NoeudArbre racine;
        public Arbre(Salarie salarier) { 
            racine=new NoeudArbre(salarier);
        }

        public NoeudArbre Racine {  get { return racine; } }    
        public NoeudArbre TrouverSalarier(int num)
        {
            return TrouverSalarier(this.racine,num);
        }
        private NoeudArbre TrouverSalarier(NoeudArbre noeud,int num)
        {
            if (noeud == null)
            {
                return null;
            }
            else if (noeud.Salarie.Numss == num) {
                return noeud;
            }
            else
            {
                foreach(NoeudArbre subordonne in noeud.Subordonnes)
                {
                    NoeudArbre noeudtrouve=TrouverSalarier(subordonne,num);
                    if(noeudtrouve!=null)
                    {
                        return noeudtrouve;
                    }
                }
                return null;
            }
        }
        public void AjouterSubordonne(int numSuperieur, Salarie salarie)
        {
            NoeudArbre superieur = TrouverSalarier(numSuperieur);
            if(superieur != null)
            {
                superieur.Subordonnes.Add(new NoeudArbre(salarie));
            }
        }

        public void SupprimerSalarie(int num)
        {
            bool supprime=SupprimerSalarie(null, this.racine, num);
            if (supprime)
            {
                Console.WriteLine("\n Employé supprimé\n");
            }
            else
            {
                Console.WriteLine("\n Employé non trouvé \n");
            }
        }

        private bool SupprimerSalarie(NoeudArbre noeudparent,NoeudArbre noeud,int num)
        {
            if (noeud == null)
            {
                return false;
            }
            else if (noeud.Salarie.Numss == num)
            {
                if(noeudparent != null)
                {
                    noeudparent.Subordonnes.Remove(noeud);
                }
                else
                {
                    this.racine = null;
                }
                return true;
            }
            else
            {
                foreach(NoeudArbre subordonne in noeud.Subordonnes)
                {
                    if (SupprimerSalarie(noeud, subordonne, num))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public Salarie RetounerSalarie(int num)
        {
            NoeudArbre noeud = TrouverSalarier(num);
            if(noeud != null)
            {
                return noeud.Salarie;
            }
            else
            {
                return null;
            }
        }

        //private void AfficherNoeud(NoeudArbre noeud, string identation)
        //{
        //    if (noeud != null)
        //    {
        //        Console.WriteLine(identation + noeud.Salarie.Numss + "/" + noeud.Salarie.Nom + "  " + noeud.Salarie.Prenom + " /" + noeud.Salarie.Poste);
        //        foreach (NoeudArbre subordonne in noeud.Subordonnes)
        //        {
        //            AfficherNoeud(subordonne, identation + " \t");
        //        }
        //    }
        //}
        public void AfficherArbre()
        {
            AfficherNoeud(this.racine, " ");
        }

        private void AfficherNoeud(NoeudArbre noeud, string identation)
        {
            if (noeud != null)
            {
                Console.WriteLine(identation + noeud.Salarie.Numss + "/" + noeud.Salarie.Nom + "  " + noeud.Salarie.Prenom + " /" + noeud.Salarie.Poste);
                foreach (NoeudArbre subordonne in noeud.Subordonnes)
                {
                    Console.WriteLine(" ");
                    AfficherNoeud(subordonne, " | ");
                }
            }
        }



    }
}
