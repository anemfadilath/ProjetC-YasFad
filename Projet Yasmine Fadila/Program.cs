using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Net.Sockets;
using System.Data.Common;
namespace Projet_Yasmine_Fadila
{
    public class Program
    {
        static void Main(string[] args)
        {
    Gestionnaire gestionnaire = new Gestionnaire(
     123456789,
    "Dupond",
    "Jean",
    "0123456789",
    "123 Rue de la République",
    "75001",
    "Paris",
    new DateTime(2024, 5, 10),
    "Directeur Generale",
    5000

           

);
            void AfficherNombreDeLivraisonParChauffeur()
            {
                var lignes = File.ReadAllLines("commandes.txt").ToList();
                var countdeslivrasons = new Dictionary<string, int>();
                foreach (var ligne in lignes)
                {
                    string[] parties = ligne.Split(';');
                    if(parties.Length > 10 && parties[7]=="True") {
                        string idchauffeur = parties[10];
                        if(countdeslivrasons.ContainsKey(idchauffeur))
                        {
                            countdeslivrasons[idchauffeur]++;
                        }
                        else
                        {
                            countdeslivrasons[idchauffeur]=1;
                        }
                    }
                }

                foreach (var p in countdeslivrasons) { 

                    Console.WriteLine("Id chauffeur: "+ p.Key+", Nombre de livraisons: "+p.Value);
                }
            }

            void AfficherCommandeParTemps(DateTime debut,DateTime fin)
            {

                var lignes = File.ReadAllLines("commandes.txt").ToList();
                foreach (var ligne in lignes)
                {
                    string[] parties = ligne.Split(';');
                    if (parties.Length > 6)
                    {
                        DateTime date = DateTime.Parse(parties[6]);
                        if (debut <= date && date <= fin)
                        {
                            Console.WriteLine(ligne);
                        }
                    }
                    
                }
            }

            void MoyennePrixCommande()
            {
                int nbcommande = 0;
                Double totalcommande = 0;
                var lignes = File.ReadAllLines("commandes.txt").ToList();
                foreach (var ligne in lignes)
                {
                    string[] parties = ligne.Split(';');
                   
                    if (parties.Length>11)
                    {
                        totalcommande += double.Parse(parties[11]);
                        nbcommande++;
                    }
                }
                if(nbcommande > 0)
                {
                    double moyenne=totalcommande/nbcommande;
                    Console.WriteLine("La moyenne de prix des commande : " + moyenne);
                }

            }

            void ListeCommandeClient(int idclient)
            {
                foreach(Client c in gestionnaire.ListeClients)
                {
                    if (c.Numss == idclient)
                    {
                        c.AfficherListeCmd();
                    }
                }
                
            }

           void MoyenneCommandeClient(int id)
            {


                Client client = null;
                foreach (Client c in gestionnaire.ListeClients)
                {
                    if (c.Numss == id)
                    {
                       client=c;
                        break;
                    }
                }
                if (client != null)
                {
                    double moyenne = client.MontantAchatCummule()/client.HistoriqueCommandes.NombreNoeuds();
                    Console.WriteLine("Moyenne des prix de commande du client: " + moyenne);
                }
                else
                {
                    Console.WriteLine("\n client non trouvé");
                }


            }

            void AffichageClient()
            {
                string entree;
                do
                {
                    Console.WriteLine("\n Entrez le numero de l'affichage souhaité \n");
                    Console.WriteLine("\n 1: Affichage simple\n");
                    Console.WriteLine("\n 2: Affichage par ville\n");
                    Console.WriteLine("\n 3: Affichage par montant d'achat cummulé\n");
                    Console.WriteLine("\n 4: Affichage par ordre alphabétique\n");
                    Console.WriteLine("\n 0: Retour\n");
                    entree = Console.ReadLine();
                    switch (entree)
                    {
                        case "1":
                            gestionnaire.AfficherListeclient();
                            break;
                        case "2":
                            gestionnaire.AfficherClientParVille();
                            break;
                        case "3":
                            gestionnaire.AfficherClientParAchat();
                            break;
                        case "4":
                            gestionnaire.AfficherClientParOrdreAlpha();
                            break;
                        case "0":
                            TraitementGestionnaire();
                            break;
                    }

                } while (entree != "0");
            }
            void TraitementChauffeur(int idchauffeur)

            {
               
                Chauffeur c= gestionnaire.TrouverChauffeur(idchauffeur);
                if (c != null)
                {
                    string entree;
                    do
                    {
                        Console.WriteLine("\n Entrez le numero de l'affichage souhaité \n");
                        Console.WriteLine("\n 1: Afficher livraisons\n");
                        Console.WriteLine("\n 2: Effectuer une livraison\n");
                        Console.WriteLine("\n 3: Afficher tarif horaire et salaire cummulé\n");
                        Console.WriteLine("\n 4: Retirer salaire\n");
                        Console.WriteLine("\n 0: Retour\n");
                        entree = Console.ReadLine();
                        switch(entree)
                        {
                            case "1":
                                c.AfficherLivrasons(); break;
                            case "2":
                                Console.WriteLine("\n Entrez le numero de la commande à effectué\n");
                                int id=int.Parse(Console.ReadLine());
                                c.EffectuerLivraison(id); break;
                            case "3":
                                Console.WriteLine("Tarif horaire: "+c.TarifHoraire+" Salaire: "+c.Salaire);
                                break;
                            case "4":
                                c.RetirerSalaire(); break;
                            case "0":
                                MenuPrincipale();break;
                        }
                    } while (entree != "0");
                }
            }


            void TraitementGestionnaire()
            {
                string entree;
                do
                {
                    Console.WriteLine("\n Entrez le numero de ce que vous voulez faire\n");
                    Console.WriteLine("\n 1: Ajouter un client\n");
                    Console.WriteLine("\n 2: Afficher la liste des client\n");
                    Console.WriteLine("\n 3: Modifier les informations d'un client\n");
                    Console.WriteLine("\n 4: Supprimer un client\n");
                    Console.WriteLine("\n 5: Associer un chauffeur à une commande");
                    Console.WriteLine("\n 6: Ajouter un nouveau salarié\n");
                    Console.WriteLine("\n 7: Afficher l'organigramme\n");
                    Console.WriteLine("\n 8: Supprimer un salarié\n");
                    Console.WriteLine("\n 9: Modifier un salarié\n");
                    Console.WriteLine("\n 10: Afficher la moyenne de prix des commandes\n");
                    Console.WriteLine("\n 11: Nombre de livraisons effectué par chauffeurs\n");
                    Console.WriteLine("\n 12: Afficher les commandes selon une période de temps\n");
                    Console.WriteLine("\n 13: Afficher la liste des commandes pour un client \n");
                    Console.WriteLine("\n 14: Afficher la moyenne de prix des commandes pour un client \n");

                    Console.WriteLine("\n 0: Retour\n");
                    entree = Console.ReadLine();
                    switch (entree)
                    {
                        case "1":
                            gestionnaire.AjouterClient();
                            break;
                        case "2":
                            AffichageClient();
                            break;
                        case "3":
                            Console.WriteLine("Entrez le numero du client à modifier");
                            int idc = int.Parse(Console.ReadLine());
                            gestionnaire.ModifierClient(idc);
                            break;
                        case "4":
                            Console.WriteLine("Entrez le numero du client à modifier");
                            int idc2 = int.Parse(Console.ReadLine());
                            gestionnaire.SupprimerClient(idc2);
                            break;
                        case "5":
                            Console.WriteLine("\n Entrez le numero de la commande\n");
                            int idc3 = int.Parse(Console.ReadLine());
                            Console.WriteLine("\n Entrez le le numero du chaauffeur\n");
                            int idchauffeur = int.Parse(Console.ReadLine());
                            Chauffeur c = gestionnaire.TrouverChauffeur(idchauffeur);
                            if (c != null)
                            {
                                gestionnaire.AssocierChauffeurACommande(idc3, c);
                            }
                            else
                            {
                                Console.WriteLine("Ce salarié n'est pas un chauffeur");
                            }
                            break;
                        case "6":
                            gestionnaire.AjouterSalarie();break;
                        case "7":
                            gestionnaire.AfficherOrganig();break;
                        case "8":
                            gestionnaire.SupprimerSalarie();break;
                        case "9":
                            gestionnaire.ModifierSlarie();break;
                        case "10":
                            MoyennePrixCommande(); break;
                        case "11":
                            AfficherNombreDeLivraisonParChauffeur(); break;
                        case "12":
                            Console.WriteLine("\n Date debut\n");
                            Console.WriteLine("Année");
                            int annee = int.Parse(Console.ReadLine());
                            Console.WriteLine("Mois");
                            int mois = int.Parse(Console.ReadLine());
                            Console.WriteLine("Jour");
                            int jour = int.Parse(Console.ReadLine());
                            DateTime debut = new DateTime(annee, mois, jour);
                            Console.WriteLine("\n Date fin\n");
                            Console.WriteLine("Année");
                            int annee2 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Mois");
                            int mois2 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Jour");
                            int jour2 = int.Parse(Console.ReadLine());
                            DateTime fin = new DateTime(annee2, mois2, jour2);
                            AfficherCommandeParTemps(debut, fin);
                            break;
                        case "13":
                            Console.WriteLine("\n identifiant du client\n");
                            int id = int.Parse(Console.ReadLine());
                            ListeCommandeClient(id);break;
                        case "14":
                            Console.WriteLine("\n identifiant du client\n");
                            int id2 = int.Parse(Console.ReadLine());
                            MoyenneCommandeClient(id2);break;


                        case "0":
                           
                            MenuPrincipale();
                            break;



                    }
                } while (entree != "0");
            }

             void TraitementClient(int idclient)
            {
                Client c = null;
                foreach (var client in gestionnaire.ListeClients)
                {
                    if (client.Numss == idclient)
                    {
                        c = client; break;
                    }
                }
                string entree;
                if (c != null)
                {
                    do
                    {
                        Console.WriteLine("\n Entrez le numero de ce que vous voulez faire \n");
                        Console.WriteLine("\n 1: Effectuer un commande\n");
                        Console.WriteLine("\n 2: Afficher liste des commande \n");
                        Console.WriteLine("\n 3: Annuler une commande \n");
                        Console.WriteLine("\n 4: Afficher le prix d'une commande \n");
                        Console.WriteLine("\n 5: Effectuer un payement \n");
                        Console.WriteLine("\n 6: Marqué une commande comme livré \n");
                        Console.WriteLine("\n 7: Afficher les commandes par dates \n");
                        Console.WriteLine("\n 8: Afficher une commande selon une date \n");
                        Console.WriteLine("\n 9: Afficher une commande par son identifiant \n");
                        Console.WriteLine("\n 0: Retour\n");
                        entree = Console.ReadLine();
                        switch (entree)
                        {
                            case "1":
                                c.AjouterCommande(); break;
                            case "2":
                                c.AfficherListeCmd();
                                break;
                            case "3":
                                Console.WriteLine("\nEntrez le numero de la commande à annulé\n");
                                int ncmd = int.Parse(Console.ReadLine());
                                c.SupprimerCommande(ncmd);
                                break;
                            case "4":
                                Console.WriteLine("\nEntrez le numero de la commande\n");
                                int ncmd2 = int.Parse(Console.ReadLine());
                                c.AfficherPrixLivraison(ncmd2);
                                break;
                            case "5":
                                Console.WriteLine("\nEntrez le numero de la commande\n");
                                int ncmd3 = int.Parse(Console.ReadLine());
                                c.EffectuerPayement(ncmd3);
                                break;
                            case "6":
                                Console.WriteLine("\nEntrez le numero de la commande\n");
                                int ncmd4 = int.Parse(Console.ReadLine());
                                c.MarquéCommandecommeLivré(ncmd4); break;
                            case "7":
                                c.AfficherCmdparOrdreDate();
                                break;
                            case "8":
                                Console.WriteLine("\n Date?\n");
                                Console.WriteLine("Année");
                                int annee = int.Parse(Console.ReadLine());
                                Console.WriteLine("Mois");
                                int mois = int.Parse(Console.ReadLine());
                                Console.WriteLine("Jour");
                                int jour = int.Parse(Console.ReadLine());
                                DateTime date = new DateTime(annee, mois, jour);
                                c.RenvoiCommandePourDate(date);
                                break;
                            case "9":
                                Console.WriteLine("\n Entrez le numero de la commande\n");
                                int idc3 = int.Parse(Console.ReadLine());
                                Commande cmd=c.TrouverCommande(idc3);
                                Console.WriteLine(cmd.ToString());
                                break;

                            case "0":
                               
                                MenuPrincipale();
                                break;

                        }

                    } while (entree != "0");
                }
                else
                {
                    Console.WriteLine("\n identifiant incorrecte\n");
                }
            }

            void MenuPrincipale()
            {
                string entree;
                do
                {

                    Console.WriteLine("\n Hello!!!!!\n");
                    Console.WriteLine("\n 1: Gestionnaire\n");
                    Console.WriteLine("\n 2: Client\n");
                    Console.WriteLine("\n 3: Chauffeur\n");
                    Console.WriteLine("\n 0:Retour\n");
                    entree= Console.ReadLine();

                    switch(entree)
                    {
                        case "1":
                            Console.WriteLine("\n mot de pass\n");
                            string p=Console.ReadLine();
                            if (p == gestionnaire.Pass)
                            {
                                TraitementGestionnaire();
                            }
                            else
                            {
                                Console.WriteLine("\n mot de pass incorrect\n");

                            }
                            break;
                        case "2":
                            Console.WriteLine("\n Indentifiant\n");
                            int id=int.Parse(Console.ReadLine());
                            TraitementClient(id);
                            break;
                        case "3":
                            Console.WriteLine("\n Indentifiant\n");
                            int id2 = int.Parse(Console.ReadLine());
                            TraitementChauffeur(id2);break;
                        case "0":
                            Console.WriteLine("Quitter");
                            Environment.Exit(0);
                            break;

                    }
                } while (entree != "0");

            }

            MenuPrincipale();


        }
    }
}
