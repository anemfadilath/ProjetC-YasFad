using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;// nous avons trouver ce package là sur internet qui permet de lire les fichier excel avec c# pour pouvoir l'utilisé nous avons intallé le package EPPlus


namespace Projet_Yasmine_Fadila
{
    public class Commande: IComparable<Commande>
    {
        int numCmd;
        string pointA;
        string pointB;
        Chauffeur chauffeur;
        DateTime dateLivraison;
        bool statusPayment;
        bool statusLivraison;
        Client client;
        Random rand = new Random();// class integrer dans c# qui permet de generer des nombres aléatoire
        Vehicule vehicule;
       

        public Commande(string pointA, string pointB, DateTime dateLivraison, Client client, Vehicule vehicule)
        {
            this.numCmd = rand.Next(1000, 10000);
            this.pointA = pointA;
            this.pointB = pointB;
            this.dateLivraison = dateLivraison;
            this.statusPayment = false;
            this.statusLivraison = false;
            this.chauffeur = null;
            this.client = client;
         this.vehicule = vehicule;
         }

        public Commande() { }

        




        // ici nous rajoutons des accesseurs pour l'attribut livraison effectuer car on veut pouvoir le modifier 


        public bool StatusPayment
        {
            get { return statusPayment; }
            set { statusPayment = value; }
        }
        public bool StatusLivraison
        {
            get { return statusLivraison; }
            set { statusLivraison = value; }
        }

        public int NumCmnd
        {
            get { return numCmd; }
        }
        public DateTime DateLivraison
        {
            get { return dateLivraison; }
            set { this.dateLivraison = value; }
        }


        public Chauffeur Chauffeur
        {
            get { return chauffeur;}
            set { chauffeur = value;}
        }

        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }
        public override string ToString()

        {
            string aff = " ";
            if (this.chauffeur != null)
            {
                aff+=" Numero de Livraison: " + numCmd + " Date de livraion: " + dateLivraison.ToLongDateString() + "PointA: " + pointA + "PointB: " + pointB + "Idchauffeur " + chauffeur.Numss + "prix "+this.Prix;
            }
            else
            {
                aff += " Numero de Livraison: " + numCmd + " Date de livraion: " + dateLivraison.ToLongDateString() + "PointA: " + pointA + "PointB: " + pointB + "Idchauffeur ";
            }
            return aff;
        }

        private (int d,string t)DistanceVille(string VilleA,string VilleB)
        {
            int dist = 0;
            string temps = "";
            using (var package =new ExcelPackage(new FileInfo("Distances.xlsx")))
            {
                var feuille = package.Workbook.Worksheets[0];
                int nbligne = feuille.Dimension.Rows;
                for (int ligne=1; ligne<=nbligne; ligne++) {

                    if (feuille.Cells[ligne,1].Value.ToString() == VilleA && feuille.Cells[ligne, 2].Value.ToString() == VilleB ||
                        feuille.Cells[ligne, 1].Value.ToString() == VilleB && feuille.Cells[ligne, 2].Value.ToString() == VilleA) {
                        dist=int.Parse(feuille.Cells[ligne, 3].Value.ToString());
                        temps = feuille.Cells[ligne, 4].Value.ToString();
                    }
                }
            }
            return (dist,temps);
        }

        // convertis le temps en heures
        private float ConvertirTemps(string t)
        {
            if(t.Contains("h"))
            {
                var HeurMin = t.Split('h');
                int h = int.Parse((HeurMin[0]));
                int m = int.Parse((HeurMin[1]));
                float TempsCoverti = h + (m / 60);
                return TempsCoverti;
            }
            else
            {
                int m = int.Parse(t);
                float TempsCoverti=m / 60;
                return TempsCoverti;
            }
        }


        //ici nous avons calculé le prix en suposant que
        //1km vaut 10 euro et en multipliant le temps du trajet par le tarif horaire du chauffeur
        //    et on modifie le salaire du chauffeur 
        public double Prix
        {
            get
            {
                (int DistanceEntreVille , string tempsTotal)= DistanceVille(pointA, pointB);
                float tempsConv=ConvertirTemps(tempsTotal);
                double prix = (DistanceEntreVille * 10) + (tempsConv * chauffeur.TarifHoraire);
                //chauffeur.Salaire = chauffeur.Salaire + (tempsConv * chauffeur.TarifHoraire);
                return prix;

            }
        }


        public int CompareTo(Commande c)
        {
            if (c == null) return 1;
            return this.dateLivraison.CompareTo(c.dateLivraison);

        }

  



        //fonction pour modifier une commande
        public void ModifierCommande(Commande c)
        {
            string entree;
            do
            {
                Console.WriteLine("Choisissez une option: \n");
                Console.WriteLine("1 pour modifier le point de départ de la livraison \n");
                Console.WriteLine("2 pour modifier le point d'arrivée de la livraison \n");
                Console.WriteLine("3 pour modifier la date de la livraison \n");
                Console.WriteLine("4 pour modifier le chauffeur ou assigner un chauffeur \n");
                Console.WriteLine("0 pour quitter");
                entree = Console.ReadLine();

                switch (entree)
                {
                    case "1":
                        Console.WriteLine("Entrez le nouveau point de livraison \n");
                        string pointA = Console.ReadLine();
                        c.pointA = pointA;
                        break;
                    case "2":
                        Console.WriteLine("Entrez le nouveau point de livraison \n");
                        string pointB = Console.ReadLine();
                        c.pointB = pointB;
                        break;
                    case "3":
                        Console.WriteLine("Entrez une date sous format jj/mm/aaaa \n");
                        string date = Console.ReadLine();
                        DateTime dt = DateTime.Parse(date);
                        c.dateLivraison = dt;
                        break;
                    case "4":
                        Console.WriteLine("Entrez le numéro du chauffeur");

                        // mettre le code pour modifier le chauffeur
                        break;
                    case "0":
                        Console.WriteLine("Quitter");
                        Environment.Exit(0);
                        break;

                }
            } while (entree != "0");


        }
    }
}
