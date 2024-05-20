using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Projet_Yasmine_Fadila
{
    public class Client:Personne,IComparable
    {
        ListeGenerique<Commande> historiqueCommandes = new ListeGenerique<Commande>();

        Random rand = new Random();
        public Client(int numss, string nom, string prenom, string telephone, string rue, string code, string ville) : base(numss, nom, prenom, telephone, rue, code, ville)
        {
            historiqueCommandes = new ListeGenerique<Commande>();

        }
        public Client(int numss, string nom, string prenom, string telephone, string rue, string code, string ville, ListeGenerique<Commande> historiqueCommandes) : base(numss, nom, prenom, telephone, rue, code, ville)
        {
            this.historiqueCommandes = historiqueCommandes;
        }


        public override string Nom
        {
            get { return nom; }
        }
        public Adresse Adresse
        {
            get { return adress; }
            set { adress = value; }
        }
        public string Telephone
        {
            get { return telephone; }
            set {  telephone = value; }
        }
        public String Prenom
        {
            get { return prenom; }
        }
        public int Numss
        {
            get { return numss; }
        }
        public ListeGenerique<Commande> HistoriqueCommandes
        {
            get { return historiqueCommandes; }
        }
        private (int d, string t) DistanceVille(string VilleA, string VilleB)
        {
            int dist = 0;
            string temps = "";
            using (var package = new ExcelPackage(new FileInfo("Distances.xlsx")))
            {
                var feuille = package.Workbook.Worksheets[0];
                int nbligne = feuille.Dimension.Rows;
                for (int ligne = 1; ligne <= nbligne; ligne++)
                {

                    if (feuille.Cells[ligne, 1].Value.ToString() == VilleA && feuille.Cells[ligne, 2].Value.ToString() == VilleB ||
                        feuille.Cells[ligne, 1].Value.ToString() == VilleB && feuille.Cells[ligne, 2].Value.ToString() == VilleA)
                    {
                        dist = int.Parse(feuille.Cells[ligne, 3].Value.ToString());
                        temps = feuille.Cells[ligne, 4].Value.ToString();
                    }
                }
            }
            return (dist, temps);
        }
        private float ConvertirTemps(string t)
        {
            if (t.Contains("h"))
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
                float TempsCoverti = m / 60;
                return TempsCoverti;
            }
        }



        // Méthode permettant au client de passer commande
        public void AjouterCommande()
        {
            Console.WriteLine("Entrez la ville de depart de la livraison \n");
            string pointA = Console.ReadLine();
            Console.WriteLine("\n Entrez la ville d'arrivée  de la livraison \n");
            string pointB = Console.ReadLine();
            Console.WriteLine("\n Entrez la date de livraison souhaité l'annee puis le jours puis le mois \n");
            Console.WriteLine("Année");
            int annee = int.Parse(Console.ReadLine());
            Console.WriteLine("Mois");
            int mois = int.Parse(Console.ReadLine());
            Console.WriteLine("Jours");
            int jours = int.Parse(Console.ReadLine());
            Console.WriteLine("\nSpecifier le type de vehicule\n");
            Console.WriteLine("\n 1:Voiture\n");
            Console.WriteLine("\n 2:Camionnette\n");
            Console.WriteLine("\n 3: Pois lourd\n");
            Vehicule v = null;
            string entree = Console.ReadLine();

            switch (entree)
            {
                case "1":
                    Console.WriteLine("Precisé le nombre de passagé");
                    int passager = int.Parse(Console.ReadLine());
                    v = new Voiture("1234", "Peugeot", passager);
                    break;
                case "2":
                    Console.WriteLine("entrez l'usage\n");
                    string usage = Console.ReadLine();
                    v = new Camionnette("123", "Peugeot", usage);
                    break;
                case "3":
                    Console.WriteLine("\n Que voulez vous transporté?\n");
                    Console.WriteLine("\n 1: produits chimique et agroalimentaire");
                    Console.WriteLine("\n 2:  sable, de la terre, du gravier \n");
                    Console.WriteLine("\n 3: marchandises périssables");
                    string entr = Console.ReadLine();
                    switch (entr)
                    {
                        case "1":
                            Console.WriteLine("\n entrez le type de produit à transporter\n");
                            Console.WriteLine("\n 1: liqude\n");
                            Console.WriteLine("\n 2: Gaz\n");
                            string entr2 = Console.ReadLine();
                            string cuve = "";
                            string materiel = "";
                            switch (entr2)
                            {
                                case "1":
                                    cuve = "cylindrique";
                                    materiel = "Liquide";
                                    break;
                                case "2":
                                    cuve = "conique";
                                    materiel = "Gaz";
                                    break;
                            }
                            Console.WriteLine("\n Entrez le volume à transporté\n");
                            double volume = double.Parse(Console.ReadLine());
                            v = new CamionCiterne("123", "Peugeot", volume, materiel, cuve);
                            break;
                        case "2":
                            Console.WriteLine("\n Entrez le type de travaux à faire \n");
                            Console.WriteLine("\n 1:travaux publics ");
                            Console.WriteLine("\n 2: travaux de voirie");
                            string entr3 = Console.ReadLine();
                            string m = "";
                            int nbbennes = 0;
                            switch (entr3)
                            {
                                case "1":
                                    Console.WriteLine("\n Entrez ce que vous voulez transporté\n");
                                    m = Console.ReadLine();
                                    nbbennes = 3;

                                    break;
                                case "2":
                                    Console.WriteLine("\n Entrez ce que vous voulez transporté\n");
                                    m = Console.ReadLine();
                                    nbbennes = 2;
                                    break;

                            }
                            Console.WriteLine("\n Entrez le volume à transporté\n");
                            double volume2 = double.Parse(Console.ReadLine());
                            v = new CamionBenne("123", "Peugeot", volume2, m, nbbennes);
                            break;
                        case "3":
                            int nbgroupes = 0;
                            string m2 = "marchandises périssables";
                            (int DistanceEntreVille, string tempsTotal) = DistanceVille(pointA, pointB);
                            float tempsConv = ConvertirTemps(tempsTotal);
                            if (tempsConv < 1)
                            {
                                nbgroupes = 2;
                            }
                            else if (tempsConv <= 1.5)
                            {
                                nbgroupes = 4;
                            }
                            else
                            {
                                nbgroupes = 6;
                            }
                            Console.WriteLine("\n Entrez le volume à transporté\n");
                            double volume3 = double.Parse(Console.ReadLine());

                            v = new CamionFrigorifique("123", "Peugeot", volume3, m2, nbgroupes);
                            break;
                    }
                    break;
            }
            if (v != null)
            {
                Commande commande = new Commande(pointA, pointB, new DateTime(annee, mois, jours), this, v);
                //Ajout de la commade à la liste chainée de commande

                this.historiqueCommandes.Ajouttete(commande);
                // Ajout de la commande au fichier
                using (StreamWriter sw = File.AppendText("commandes.txt"))
                {
                    sw.WriteLine($"{commande.NumCmnd};{this.Nom};{this.Prenom};{this.Numss};{pointA};{pointB};{commande.DateLivraison.Date};{commande.StatusPayment};{commande.StatusLivraison};{commande.Vehicule.ToString()}");
                }
            }
            else
            {
                Console.WriteLine("Erreur lors de la creation de la commande");
            }
        }

        public void SupprimerCommande(int idcommande)
        {
            NoeudGenerique<Commande> acc = historiqueCommandes.Tete;
            NoeudGenerique<Commande> noeudAsupp = null;

            while (acc != null)
            {
                if (acc.Donnee.NumCmnd == idcommande)
                {
                    noeudAsupp = acc;
                    break;
                }
                acc = acc.Suivant;
            }

            if (noeudAsupp != null)
            {
                this.historiqueCommandes.SupprimerNoeud(noeudAsupp.Donnee);
            }
        


        //supprime la commande du fichier de commande
        //on lit le fichier en le convetissant en une liste et on parcours la liste pour
        //    supprimer la commande et on reecrit le fichier
        var lignes = File.ReadAllLines("commandes.txt").ToList();
            int index = -1;
            for (int i=0; i < lignes.Count; i++)
            {
                var parties = lignes[i].Split(';');
                if (parties[0] == idcommande.ToString())
                {
                    index=i; break;
                }
            }
            if (index != -1)
            {
                lignes.RemoveAt(index);
                File.WriteAllLines("commandes.txt",lignes);
            }
        }

        public double MontantAchatCummule()
        {
            double montant = 0;
            NoeudGenerique<Commande> acc = this.historiqueCommandes.Tete;
            while(acc != null)
            {
                montant += acc.Donnee.Prix;
                acc=acc.Suivant;
            }
            return montant;

        }
        // on implemente la methode compareto de l'interface icomparable ici
        // pour pouvoir comparer deux client selon leurs montant d'achat cummulé
        //renvoi moins de 0 si l'objet est inferieur à l'objet passer en paramètre
        //    0 si ils sont egaux 
        //    et plus de 0 si il est superieur
        public int CompareTo(object client)
        {
            Client cl = client as Client;
            return this.MontantAchatCummule().CompareTo(cl.MontantAchatCummule());
        }

        public void EffectuerPayement(int idcmand)
        {
            NoeudGenerique<Commande> acc = this.historiqueCommandes.Tete;
            NoeudGenerique<Commande> noeudmodifier = null;
            while (acc != null)
            {
                if (acc.Donnee.NumCmnd == idcmand)
                {
                    noeudmodifier = acc;
                    break;
                    //quand le noeud qui corespond à la commande chercher avec le numero
                    //    de commande est trouvé, on quitte la boucle


                }
                acc = acc.Suivant;
               
            }
            if (noeudmodifier != null)

            {
                if (noeudmodifier.Donnee.StatusPayment == false)
                {
                    noeudmodifier.Donnee.StatusPayment = true;
                }
                else
                {
                    Console.WriteLine("Vous avez dejà payé cette commande");
                }
            }


            var lignes = File.ReadAllLines("commandes.txt").ToList();
            int index = -1;
            for (int i = 0; i < lignes.Count; i++)
            {
                var parties = lignes[i].Split(';');
                if (parties[0] == idcmand.ToString())
                {
                    index = i;
                    if (parties[7] == "False")
                    {
                        parties[7] = "True";
                        lignes[i] = string.Join(";", parties);
                    }
                    else
                    {
                        Console.WriteLine("Vous avez dejà payé cette commande");
                    }
                    break;
                }
            }
            if (index != -1)
            {
                File.WriteAllLines("commandes.txt", lignes);
            }

        }
        public Commande TrouverCommande(int idcmd)
        {
            NoeudGenerique<Commande> acc = this.historiqueCommandes.Tete;
            NoeudGenerique<Commande> noeud = null;
            while (acc != null)
            {
                if (acc.Donnee.NumCmnd == idcmd)
                {
                    noeud = acc;
                    break;
                    //quand le noeud qui corespond à la commande chercher avec le numero
                    //    de commande est trouvé, on quitte la boucle


                }
                acc = acc.Suivant;

            }
            return noeud.Donnee;

        }

        public void AfficherPrixLivraison(int idcmd)
        {
            var lignes = File.ReadAllLines("commandes.txt").ToList();
            int index = -1;
            for (int i = 0; i < lignes.Count; i++)
            {
                var parties= lignes[i].Split(';');
                if(parties[0] == idcmd.ToString())
                {
                    index= i;break;
                }
            }
            if(index != -1)
            {
                var partiecmdtrouve = lignes[index].Split(';');
                if (partiecmdtrouve.Length >10)
                {
                    Commande commande = TrouverCommande(idcmd);
                    Console.WriteLine(commande.Prix);
                    //Console.WriteLine(partiecmdtrouve[10]);
                }
                else
                {
                    Console.WriteLine("Le prix de la commande n'a pas encore été calculé");
                }
            }
            else
            {
                Console.WriteLine("Commande non trouvé");
            }


        }

        public void MarquéCommandecommeLivré(int idcmd)
        {
            NoeudGenerique<Commande> acc = this.historiqueCommandes.Tete;
            NoeudGenerique<Commande> noeudAmodifier = null;
            while (acc != null)
            {
                if (acc.Donnee.NumCmnd == idcmd)
                {
                    noeudAmodifier = acc;
                    break;
                }
                acc=acc.Suivant;
            }
            if(noeudAmodifier != null)
            {
                if (noeudAmodifier.Donnee.StatusLivraison == false)
                {
                    noeudAmodifier.Donnee.StatusLivraison = true;
                }
                else
                {
                    Console.WriteLine("Commande déjà marqué comme livré");
                }
            }
            else
            {
                Console.WriteLine("Commande non trouvée");
            }

        }
        public void AfficherListeCmd()
        {
            this.historiqueCommandes.Afficher(data => Console.WriteLine(data.ToString()));
        }
       
        public void AfficherCmdparOrdreDate()
        {
            historiqueCommandes.sort();
            this.historiqueCommandes.Afficher(data => Console.WriteLine(data.ToString()));
        }

        public void RenvoiCommandePourDate(DateTime date)
        {
            Predicate<Commande> predicat=(Commande c)=>c.DateLivraison.Date==date.Date;
            List<NoeudGenerique<Commande>> commandes = historiqueCommandes.FindAll(predicat);
            foreach (NoeudGenerique<Commande> noeud in commandes)
            {
                Commande cmd = noeud.Donnee;
               
             Console.WriteLine( cmd.ToString());
            }
        }

        public void ModifierCommande(int numCmd)
        {
            Console.WriteLine(" \n");
            
        }
    }
}
