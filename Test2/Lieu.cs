using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace Test2
{
    internal class Lieu
    {
        public string Nom { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture2D TexturePorte { get; set; } // Image sur le fond
        public Texture2D TextureLieu { get; set; } // Image qui s'affiche

        private SpriteFont text { get; set; }

        private int lieuNum { get; set; }

        public Texture2D TextureBonus { get; set; }

        public int eventNum { get; set; }

        private bool contactImage = false;

        private int largeurEcran = Globals.EcranWidth;
        private int hauteurEcran = Globals.EcranHeigth;

        private string messageResultat = ""; // le message de fin de reponse (Reussi,ou non)


        public Lieu(string nom, float x, float y, int width, int height,
            Texture2D porte, Texture2D imageLieu, SpriteFont _xp, int lieu_num, Texture2D texture_bonus, int event_num)
        {
            Nom = nom;
            X = x;
            Y = y;

            Width = width;
            Height = height;
            TexturePorte = porte;
            TextureLieu = imageLieu;
            text = _xp;
            lieuNum = lieu_num;
            eventNum = event_num;
            TextureBonus = texture_bonus;
        }

        // Vérifier si le joueur touche image
        public bool VerifierContact(float playerX, float playerY, int playerWidth, int playerHeight)
        {
            bool contact = playerX < X + Width && playerX + playerWidth > X && playerY < Y + Height &&
                           playerY + playerHeight > Y;

            //bool local dans bool Global
            if (contact)
            {
                contactImage = true;
                return true;
            }
            else
            {
                contactImage = false;
                return false;
            }
        }

        // Dessin Portes
        public void DrawPorte(SpriteBatch spriteBatch)
        {
            int tailleX = (int)hauteurEcran / 13;
            int tailleY = (int)largeurEcran / 13;
            spriteBatch.Draw(TexturePorte,
                new Rectangle((int)X, (int)Y, tailleX, tailleY),
                Color.White);
        }

        // Dessiner l'image du lieu si actif
        public void DrawLieu(SpriteBatch spriteBatch)
        {
            // ----------------------------------------------------------------------------------
            //imprime toutes les images si contact
            if (contactImage)
            {
                int largeurZone = (int)(largeurEcran * 0.75);
                int hauteurZone = hauteurEcran;

                float echelleX = (float)largeurZone / TextureLieu.Width;
                float echelleY = (float)hauteurZone / TextureLieu.Height;
                float facteurEchelle;

                if (echelleX < echelleY)
                {
                    facteurEchelle = echelleX;
                }
                else
                {
                    facteurEchelle = echelleY;
                }

                int largeurImage = (int)(TextureLieu.Width * facteurEchelle);
                int hauteurImage = (int)(TextureLieu.Height * facteurEchelle);

                // imprime l'image de lieu 
                spriteBatch.Draw(
                    TextureLieu,
                    new Rectangle(0, 0, largeurImage, hauteurImage),
                    Color.White
                );

                // rectangle noir droite 
                int xNoir = largeurImage;
                int largeurNoir = largeurEcran - largeurImage;

                if (largeurNoir > 0)
                {
                    spriteBatch.Draw(
                        TextureLieu,
                        new Rectangle(xNoir, 0, largeurNoir, hauteurEcran),
                        Color.Black
                    );
                }

                // rectnagle noir bas
                if (hauteurImage < hauteurEcran)
                {
                    spriteBatch.Draw(
                        TextureLieu,
                        new Rectangle(0, hauteurImage, largeurImage, hauteurEcran - hauteurImage),
                        Color.Black
                    );
                }

                bool haut = false;
                // positioner le texte en haut ou en bas en fonction
                Vector2 positionTexte;
                if (hauteurImage + 20 < hauteurEcran)
                {
                    positionTexte = new Vector2(40, hauteurImage + 20);
                    haut = true;
                }
                else if (largeurImage + 20 < largeurEcran)
                {
                    positionTexte = new Vector2(largeurImage + 20, 40);
                }
                else
                {
                    positionTexte = new Vector2(10, 10);
                }

                Vector2 positionBase = positionTexte;

                //Dessine Inventaire qui change de position si le jeu est en bas ou à droite
                if (lieuNum > 0)
                {
                    Vector2 positionInventaire;
                    if (haut == true)
                    {
                        positionInventaire = new(positionTexte.X + largeurEcran / 1.6f + (largeurEcran / 25),
                            hauteurImage + 20);
                    }
                    else
                    {
                        positionInventaire = new(largeurImage + 20,
                            positionTexte.Y + hauteurEcran / 1.6f + (largeurEcran / 25));
                    }

                    //sur le coté traits inventaire
                    for (float i = positionInventaire.Y - 6; i < hauteurEcran; i++)
                    {
                        Vector2 imprimer = new Vector2(positionInventaire.X, i);
                        spriteBatch.DrawString(text, "|", imprimer, Color.White);
                    }

                    // en haut trait inventaire
                    for (float i = positionInventaire.X; i < largeurEcran; i++)
                    {
                        Vector2 imprime2 = new Vector2(i, positionInventaire.Y - 15);
                        spriteBatch.DrawString(text, "-", imprime2, Color.White);
                    }

                    //mettre des items dans l'inventaire
                    positionInventaire = new Vector2(positionInventaire.X + 10, positionInventaire.Y);
                    string invent = "INVENTAIRE:";
                    if (Globals.armureNbr > 0)
                    {
                        invent = invent + "\n" + "\n" +
                                 "Potion de savoir \n (Ameliore temporairement \n la comprehension des ennemis)";
                    }

                    if (Globals.plumeNbr > 0)
                    {
                        invent = invent + "\n" + "\n" +
                                 "Plume du Sage \n (Un artefact magique permettant \n  de taper plus fort)";
                    }

                    if (Globals.epeeRamasse == true)
                    {
                        invent = invent + "\n" + "\n" + "Super epee du magicien \n A utiliser sur le roi(I)";
                    }

                    spriteBatch.DrawString(text, invent, positionInventaire, Color.White);
                }

                //------------------------------------------------------------------------------------------------------------------------------------------
                string imprime = "";
                string Question = JeuXmlReader.imprimQuest;

                // pour extraire prochaine question sachant carac separateur |
                string ExtraireProchaineQuestion()
                {
                    int indexZero = Question.IndexOf('0');
                    if (indexZero != -1)
                    {
                        string questionRecuperee = Question.Substring(0, indexZero);
                        Question = Question.Substring(indexZero + 1);
                        return questionRecuperee;
                    }

                    return "";
                }

                string imprimera = ExtraireProchaineQuestion();
                // spécificité des lieux visités 
                //tavernier
                if (lieuNum == 1)
                {
                    imprime = "JE SUIS LE TAVERNIER  appuie sur (o)";
                    positionTexte = positionBase;
                    positionTexte.Y += (int)(hauteurEcran / 30);

                    if (eventNum == 10)
                    {
                        spriteBatch.DrawString(text, "\n HIC...  j'ai un peu bu", positionTexte, Color.White);
                        
                    }

                    spriteBatch.DrawString(text, imprime, positionTexte, Color.White);
                }

                //sorcier
                if (lieuNum == 2)
                {
                    string[]questions = JeuXmlReader.imprimQuest.Split('|'); //liste de questions et pipe "|" c'est le separateur
                    positionTexte = positionBase;
                    string texteFinal = "JE SUIS LE SORCIER DU ROYAUME, reponds a mes questions  \n \n";

                    if (eventNum < questions.Length) // On affiche la question correspondant au numéro actuel

                    {
                        texteFinal += questions[eventNum];

                    }
                    else
                    {
                        texteFinal += "Je n'ai plus de questions.";
                    }

                    if (messageResultat != "")
                    {
                        texteFinal += "\n\n--> " + messageResultat;
                    }

                    spriteBatch.DrawString(text, texteFinal, positionTexte, Color.White);
                }

                //monstre
                //monstre
                if (lieuNum == 3)
                {
                    if (eventNum == 0)
                    {
                        imprime = "JE SUIS LE MONSTRE \n ET JE VAIS TE MANGER \n \n <Il a l'air vraiment dangereux>\n (E) - Engager le combat \n (Up) - Fuir ce terrible monstre";
                        spriteBatch.DrawString(text, imprime, positionTexte, Color.White);
                    }

                    if (eventNum == 10)
                    {
                        spriteBatch.Draw(TextureBonus, new Rectangle(0, 0, largeurImage, hauteurImage), Color.White);
                    }

                    if (Globals.armureNbr >= 0)
                    {
                        Game1.data.CAperso = 18;
                    }

                    if (Globals.plumeNbr >= 0)
                    {
                        Game1.data.degatsPerso = 10;
                    }
    
                    if (eventNum == 20)
                    {  
                        int viePerso, vieMonstre, caPerso, caMonstre, degatPerso, degatMonstre;
                        viePerso = Game1.data.viePerso;
                        vieMonstre = Game1.data.vieMonstre;
                        caPerso = Game1.data.CAperso;
                        caMonstre = Game1.data.CAmonstre;
                        degatPerso = Game1.data.degatsPerso;
                        degatMonstre = Game1.data.degatsMonstre;
                        Random generateur = new Random();
                        imprime += "\n <Bruit de Bataille>:";

                        while (vieMonstre > 0)
                        {
                            int tirageM = generateur.Next(0, 20);
                            int trirageP = generateur.Next(0, 20);
                            if (tirageM >= caPerso)
                            {
                                viePerso = viePerso - degatMonstre;

                            }
                            if (trirageP >= caMonstre)
                            {
                                vieMonstre = vieMonstre - degatPerso;
                            }

                            
                            spriteBatch.DrawString(text, imprime, positionTexte, Color.White);
                            if (viePerso < 0)
                            {
                                Globals.Life = Globals.Life - 1;
                                viePerso = 25;
                            }
                            
                        }
                        
                        if (vieMonstre < 0)
                         {
                             imprime +=  imprime + "\n" + "AAAARGH";
                             imprime +=  imprime + "\n " + "PvMonstre " + vieMonstre;



                             spriteBatch.Draw(TextureBonus, new Rectangle(0, 0, largeurImage, hauteurImage), Color.White);
                         }
                        
                        spriteBatch.DrawString(text, imprime, positionTexte, Color.White);

                        
                        Game1.data.vieMonstre = vieMonstre;
                        Game1.data.viePerso = viePerso;

                    }
                    
                }                               
      


                //roi
                if (lieuNum == 4)
                {
                    imprime = "JE SUIS LE ROI, RIEN NE M ATTEINT, MEME PAS TOI JEUNE CHEVALIER \n \n Frapper avec (I)";
                    positionTexte = positionBase;
                    spriteBatch.DrawString(text, imprime, positionTexte, Color.White);

                    if (eventNum == 10) //image roi fache
                    {
                        spriteBatch.Draw(TextureBonus, new Rectangle(0, 0, largeurImage, hauteurImage), Color.White);
                        string imprimeEnRouge = "AIEEE !!! \n \n c'est l'epee du magicien, mais comment l'as tu eue !? \n  \n  Je n'aurais jamais pense perdre de cette facon la MAUDIT SOIT TU ....";
                        Vector2 positionRouge = new Vector2(positionTexte.X, positionTexte.Y+100); // pour ne pas avoir les deux textes au meme endroit
                        spriteBatch.DrawString(text, imprimeEnRouge, positionRouge, Color.Red);

                    }

                    if (eventNum == 20) // image du roi COOL
                    {
                        spriteBatch.Draw(TextureLieu, new Rectangle(0, 0, largeurImage, hauteurImage), Color.White);

                    }
                }
            }
        }

        // Cette méthode gère les entrées clavier pour le questionnaire : elle valide la bonne réponse pour passer à la suivante ou pénalise les erreurs en modifiant le score.
        public void UpdateLieu(KeyboardState clavierActuel, KeyboardState clavierAvant)
        {
            if (lieuNum == 2 && contactImage)//SORCIER          // if (lieuNum != 2 || !contactImage) return;
            {
                if (eventNum >= JeuXmlReader.listeBonneReponses.Count)
                {
                    messageResultat =
                        "Tu as repondu a toutes les questions Bravo !\n \nAvant de partir , j'ai cache ma super epee que tu trouveras vers le lac .\n Tu pourras le recuperer en appuyant sur (I) pour vaincre le Roi.\n \n(Eloigne toi pour quitter)";
                    return;
                }

                string reponseAttendue = JeuXmlReader.listeBonneReponses[eventNum].Trim();
                string mauvaisesReponses = JeuXmlReader.listeMauvaiseReponses[eventNum].Trim();

                if (Enum.TryParse(reponseAttendue, true, out Keys toucheBonne))
                {
                    if (clavierActuel.IsKeyDown(toucheBonne) && !clavierAvant.IsKeyDown(toucheBonne))
                    {
                        Globals.Xpscore += 50;
                        eventNum++;
                        Globals.plumeNbr = Globals.plumeNbr + 1;
                        Globals.armureNbr = Globals.armureNbr + 1;
                        

                        messageResultat = "Bonne reponse ! Suivant...";
                        return;
                    }
                }

                for (int i = 0; i < mauvaisesReponses.Length; i++)
                {
                    if (Enum.TryParse(mauvaisesReponses[i].ToString(), true, out Keys toucheMauvaise))
                    {
                        if (clavierActuel.IsKeyDown(toucheMauvaise) && !clavierAvant.IsKeyDown(toucheMauvaise))
                        {
                            Globals.Xpscore -= 50;
                            messageResultat = "Pas bon ! ReadTheFuckingManual !";
                        }
                    }
                }
            }

            if (lieuNum == 4 && contactImage)//ROI
            {
                
            }
        }
    }
}
          
           
            
        
    
