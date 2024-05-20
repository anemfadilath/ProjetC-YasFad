using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Yasmine_Fadila
{
    public class ListeGenerique<T> where T : IComparable<T>
    {
        public delegate void AfficheDelegue(T donnee);// delegation pour afficher les noeud

        private NoeudGenerique<T> tete;
        private NoeudGenerique<T> queue;

        public NoeudGenerique<T> Tete
        {
            get { return tete; }
        }

            //pour ajouter un element dans la liste chainer, nous avons choisit de rajouter les nouveau elements  à chaque fois à 
            //   la tête de la liste. si la tête de la liste est null on ajoute l'element à la tete de la liste et à la queue
            //    si non on deplace l'element à la tête de la liste et on rajoute le nouveau element à la tête

        public void Ajouttete(T t)
        {
            var noeud=new NoeudGenerique<T> { Donnee=t };
            if(tete==null)
            {
                tete = noeud;
                queue = noeud;
            }
            else
            {
                noeud.Suivant = tete;
                tete = noeud;
            }

        }


        public void Afficher( AfficheDelegue affiche)
        {
            var noeud = tete;
            while (noeud != null)
            {
                affiche(noeud.Donnee);
                noeud = noeud.Suivant;
            }

        }
        //;

        //fonction pour supprimer un noeud de la liste chaine
        public void SupprimerNoeud(T t)
        {
            if (tete == null)
            {
                return; 
            }

            if (tete.Donnee.Equals(t))
            {
                tete = tete.Suivant; 
                return;
            }

            var noeud = tete;

            while (noeud.Suivant != null)
            {
                if (noeud.Suivant.Donnee.Equals(t))
                {
                    noeud.Suivant = noeud.Suivant.Suivant; 
                    return;
                }
                noeud = noeud.Suivant;
            }
        }


        public void sort()
        {
            NoeudGenerique<T> courant = this.tete;
            while( courant != null)
            {
                NoeudGenerique<T> min = courant;
                NoeudGenerique<T> r = courant.Suivant;
                while( r != null)
                {
                    if(min.Donnee.CompareTo(r.Donnee)>0)
                    {
                        min = r;
                    }
                    r= r.Suivant;
                }
                T noeud=courant.Donnee;
                courant.Donnee = min.Donnee;
                min.Donnee = noeud;
                courant = courant.Suivant;
            }
        }
        public List<NoeudGenerique<T>> FindAll(Predicate<T> predicat)
        {
            List<NoeudGenerique<T>> resultat = new List<NoeudGenerique<T>>();
            NoeudGenerique<T> courant = tete;

            while (courant != null)
            {
                if (predicat(courant.Donnee))
                {
                    resultat.Add(courant);
                }
                courant = courant.Suivant;
            }

            return resultat;
        }

        public int NombreNoeuds()
        {
            int nombre = 0;
            var noeud = this.tete;
            while (noeud != null)
            {
                nombre += 1;
                noeud = noeud.Suivant; 
            }
            return nombre;
        }



    }
}
