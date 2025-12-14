using System;
using System.Xml;
using System.Collections.Generic;
namespace Test2
{


    public static class JeuXmlReader
    {
        public static String imprimQuest = "";
        public static List<string> listeBonneReponses = new List<string>();
        public static List<string> listeMauvaiseReponses = new List<string>();


        public static string QuestionsReader(string filename)
        {
            XmlReader reader = XmlReader.Create(@filename);
            imprimQuest = "";
            listeBonneReponses.Clear();
            listeMauvaiseReponses.Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "question")
                    {
                        Console.WriteLine("Jeu xml start");
                    }

                    if (reader.Name == "texte")
                    {
                        Console.WriteLine("texte !");
                        imprimQuest += reader.ReadElementContentAsString() + "\n";
                    }

                    if (reader.Name == "bonneReponse")
                    {
                        Console.WriteLine("Reponse !");
                        string reponseLue = reader.ReadElementContentAsString();
                        listeBonneReponses.Add(reponseLue);
                    }
                    
                    if (reader.Name == "mauvaiseReponse")
                    {
                        Console.WriteLine("Mauvaise reponse !");
                        string mauvaiseReponseLue = reader.ReadElementContentAsString();
                        listeMauvaiseReponses.Add(mauvaiseReponseLue);
                    }

                    if (reader.Name == "Proposition")
                    {
                        Console.WriteLine("Option1");
                        imprimQuest += reader.ReadElementContentAsString() + "\n";
                    }

                    if (reader.Name == "Jeu")
                    {
                        Console.WriteLine("Jeu xml read");
                    }
                }

                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (reader.Name == "question")
                    {
                        imprimQuest += "|"; // Utilisation du pipe '|' comme separateur final de chaque question
                    }
                }
            }

            reader.Close();
            return imprimQuest;
        }
    }

}