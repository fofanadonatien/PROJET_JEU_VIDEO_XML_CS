using System;
using System.Xml.Serialization;

namespace Test2
{
    [XmlRoot("joueur", Namespace = "https://www.univ-grenoble-alpes.fr/l3miage")]
    [Serializable]
    public class JoueurSauvegarde
    {
        [XmlAttribute("nom")] public string nom;
        [XmlElement("vie")] public int vie;
        [XmlElement("xpscore")] public int xpscore;
        [XmlElement("plume")] public int plume;
        [XmlElement("armure")] public int armure;


        public JoueurSauvegarde()
        {
            this.nom = "Joueur";
            this.vie = 3;
            this.xpscore = 100;
            this.plume = 0;
            this.armure = 0;

        }

        public void ChargerData()
        {
            this.nom = Globals.nom;
            this.vie = Globals.Life;
            this.xpscore = Globals.Xpscore;
            this.plume = Globals.plumeNbr;
            this.armure =  Globals.armureNbr;
        }
        
        public void RestaurerDonneesDansJeu()
        {
            Globals.nom = this.nom;
            Globals.Life = this.vie;
            Globals.Xpscore = this.xpscore;
            Globals.plumeNbr = this.plume;
            Globals.armureNbr = this.armure;
        }
    }
}