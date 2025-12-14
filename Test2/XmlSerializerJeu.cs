using System;
using System.IO;
using System.Xml.Serialization;

namespace Test2;

public class XmlSerializerJeu
{ 
        string pathJS = "../../../XML/JoueurSauvegarde.xml";
        JoueurSauvegarde b = new JoueurSauvegarde();

        public void Charger()
        {
            
            using (TextReader reader = new StreamReader(pathJS))
            {
                var xmlJS = new XmlSerializer(typeof(JoueurSauvegarde));
                b = (JoueurSauvegarde)xmlJS.Deserialize(reader);
            }
            b.RestaurerDonneesDansJeu();

            Console.WriteLine("SAUVEGARDE LES DONNEES" + b);
        }

        public void Sauvegarder()
        {
            b.ChargerData();
            using (TextWriter writer = new StreamWriter(pathJS))
            {
                var serialiseurJeu = new XmlSerializer(typeof(JoueurSauvegarde));
                serialiseurJeu.Serialize(writer, b);
            }

            Console.WriteLine("DONNEES CHARGER" + b);
        }
}
