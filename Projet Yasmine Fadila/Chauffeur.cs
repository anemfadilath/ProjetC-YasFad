using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Projet_Yasmine_Fadila
{
    public class Chauffeur:Salarie
    {
        
        double tarifHoraire;
       

       public Chauffeur(int numss, string nom, string prenom,  string telephone, string rue,string code, string ville, DateTime dateEntree,double tarifHoraire) :base(numss, nom, prenom,  telephone,rue,code,ville,dateEntree,"Chauffeur",0)
        {
            
            this.tarifHoraire = tarifHoraire;
           
        }
        
        public int Numss
        {
            get { return numss; }
        }


        public double Ancienneté
        {
            get
            {
                //l'ancienneté du chauffeur est un attribut calculé. on le calcul à chaque fouis qu'on y accède
                return DateTime.Now.Year - dateEntree.Year;
            }
           
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

        public float ConvertirTemps(string t)
        {
            if (t.Contains("h"))
            {
                var HeurMin = t.Split('h');
                int h = int.Parse((HeurMin[0]));
                int m = int.Parse((HeurMin[1]));
                float TempsCoverti = h + (m / 60f);
                return TempsCoverti;
            }
            else
            {
                int m = int.Parse(t);
                float TempsCoverti = m / 60f;
                return TempsCoverti;
            }
        }

        public double TarifHoraire
        {
            get
            {
                // nous avons suposé ici que le tarrif horaire du chauffeur augmente de 10% chaque année
                // ici nous calculons le tarifhoraire du chauffeur à chaque fois qu'on veut y acceder et on affecte la nouvelle valeur calculer à l'attribut tarifhoraire
                double NtarifHoraire= tarifHoraire * (1 +0.1* Ancienneté);
                tarifHoraire= NtarifHoraire;
                return tarifHoraire;
            }
            set { tarifHoraire=value; }
        }

        public void RetirerSalaire()
        {
            this.salaire = 0;

        }

        //public void AfficherLivrasons()
        //{
        //    var lignes=File.ReadAllLines("commandes.txt").ToList();

        //    for(int i = 0; i < lignes.Count; i++)
        //    {
        //        if (lignes[i].Split(';')[10] == this.numss.ToString())
        //        {

        //            Console.WriteLine("\n"+lignes[i]+"\n");
        //        }

        //    }
        //}
        public void AfficherLivrasons()
        {
            var lignes = File.ReadAllLines("commandes.txt").ToList();

            for (int i = 0; i < lignes.Count; i++)
            {
                string[] parties = lignes[i].Split(';');

                
                if (parties.Length > 10)
                {
                    if (parties[10] == this.numss.ToString())
                    {
                        Console.WriteLine("\n" + lignes[i] + "\n");
                    }
                }
                
            }
        }

        public void EffectuerLivraison(int idcmd)
        {
            var lignes = File.ReadAllLines("commandes.txt").ToList();
            int index = -1;
            for (int i = 0; i < lignes.Count; i++)
            {
                var parties = lignes[i].Split(';');
                if (parties[0] == idcmd.ToString())
                {
                    index = i;
                    if (parties[7] == "True")
                    {
                        if (parties[8] == "False")
                        {
                            (int d, string t) = DistanceVille(parties[4], parties[5]);
                            float tempsConv = ConvertirTemps(t);
                            parties[8] = "True";

                            lignes[i] = string.Join(";", parties);
                            this.salaire += this.TarifHoraire * tempsConv;
                     
                            //supprimer la commande effectué du fichier des commandes et la rajouter dans le fichier des historique de commandes
                            if (index != -1)
                            {
                                lignes.RemoveAt(index);
                                File.WriteAllLines("commandes.txt", lignes);
                                using (StreamWriter sw = File.AppendText("historiquecommandes.txt"))
                                {
                                    sw.WriteLine(lignes[i]);
                                }
                            }




                        }
                        else
                        {
                            Console.WriteLine("Cette livraison a déjà été effectuée");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n Cette commande n'a pas encore été payé\n");
                    }
                }
                



            }
            if (index != -1)
            {
                File.WriteAllLines("commandes.txt", lignes);
            }
            else
            {
                Console.WriteLine("ID de commande non trouvé");
            }
        }



      

        public override string ToString()
        {
            return base.ToString()+"\n"+"Ancienneté : "+this.Ancienneté+" TarifHoraire Calculé: "+this.TarifHoraire+ " Salaire mensuel calculé: "+this.Salaire;
        }

    }
}
