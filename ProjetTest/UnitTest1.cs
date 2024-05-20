using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_Yasmine_Fadila;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjetTest
{
    [TestClass]
    public class Test
    {
        private Chauffeur chauffeur;
        private ListeGenerique<int> liste;
        private Arbre arbre;
        [TestInitialize]

        public void Setup()
        {
            chauffeur = new Chauffeur(123456789, "chauf", "feur", "0123456789", "R", "C", "V", DateTime.Now, 20.0);
            liste = new ListeGenerique<int>();
            Salarie salarie1 = new Salarie(
         numss: 123, nom: "n1", prenom: "p1", telephone: "01234", rue: "adresse1", code: "C", ville: "V", dateEntree: DateTime.Now, poste: "P", salaire: 2000);

            Salarie salarie2 = new Salarie(
                numss: 456, nom: "n2", prenom: "p2", telephone: "05678", rue: "adresse2", code: "C", ville: "V", dateEntree: DateTime.Now, poste: "P", salaire: 3000);

            Salarie salarie3 = new Salarie(
                numss: 789, nom: "n3", prenom: "p3", telephone: "09101", rue: "adresse3", code: "C", ville: "V", dateEntree: DateTime.Now, poste: "P", salaire: 4000);

            arbre = new Arbre(salarie1);
            arbre.AjouterSubordonne(salarie1.Numss, salarie2);
            arbre.AjouterSubordonne(salarie1.Numss, salarie3);

        }

        [TestMethod]
  

        public void TestConversionTemps()
        {
            using (var reader = new StreamReader("temps.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    string temps = parts[0];
                    float conversionAttendue = float.Parse(parts[1]);

                    float conversionObtenue = chauffeur.ConvertirTemps(temps);

                    Assert.AreEqual(conversionAttendue, conversionObtenue, $"La conversion de {temps} devrait être {conversionAttendue}");
                }
            }
        }
        [TestMethod]
        public void TestNombreNoeuds()
        {
           
            liste.Ajouttete(1);
            liste.Ajouttete(2);
            liste.Ajouttete(3);

            
            int nombreNoeuds = liste.NombreNoeuds();
            Assert.AreEqual(3, nombreNoeuds, "Le nombre de nœuds devrait être 3");
        }
        [TestMethod]
       
        public void TestFindAll()
        {
            liste.Ajouttete(1);
            liste.Ajouttete(2);
            liste.Ajouttete(3);
            liste.Ajouttete(4);
            liste.Ajouttete(5);

           
            Predicate<int> estPair = x => x % 2 == 0;

            
            var noeudsPairs = liste.FindAll(estPair);

            foreach (var noeud in noeudsPairs)
            {
                Assert.IsTrue(estPair(noeud.Donnee), $"{noeud.Donnee} devrait être pair");
            }

            Assert.AreEqual(2, noeudsPairs.Count, "Il devrait y avoir 2 nœuds pairs");

            var valeursAttendues = new List<int> { 2, 4 };
            var valeursObtenues = noeudsPairs.Select(n => n.Donnee).ToList();
            CollectionAssert.AreEquivalent(valeursAttendues, valeursObtenues, "Les nœuds trouvés ne sont pas ceux attendus");
        }


        [TestMethod]
        public void TestRetournerSalarie()
        {
           
            Salarie salarieTrouve = arbre.RetounerSalarie(123);
            Assert.IsNotNull(salarieTrouve, "Le salarié devrait être trouvé");
            Assert.AreEqual("n1", salarieTrouve.Nom, "Le nom du salarié trouvé devrait être 'n'");

            Salarie salarieNonTrouve = arbre.RetounerSalarie(999);
            Assert.IsNull(salarieNonTrouve, "Le salarié ne devrait pas être trouvé");
        }

        [TestMethod]
        public void TestSupprimerSalarie()
        {
            
         

           
            arbre.SupprimerSalarie(789);

           
            Assert.IsNull(arbre.RetounerSalarie(789), "Le salarié devrait être supprimé de l'arbre");
        }




    }
}
