
Bonjour !

Pour ce jeu, la taille de la fenêtre de l'écran se change dans
Globals, il est conseillé de jouer en 1920 (Width) * 1080 (Height) ou en 1000*1000 

Mais si cela n'est pas possible le jeu devrait normalement s'adapter pour un écran plus petit.

Pour Sauvegarder (par Serialization), il est nécéssaire de mettre échap pour quitter le jeu !
La sauvegarde se fait automatiquement sinon.


Bonne partie




















Devellopeurs Notes
__________________
Pour Ajouter un event 

aller dans la methode Update de la classe Game1:

faire ou tout autre opération sur le lieu dédié 0 pour auberge, 1 pour sorcier, 2 pour monstre, 3 pour roi 
if (Keyboard.GetState().IsKeyDown(Keys.O))
        {
            _lieux[3].eventNum = 2;
            
        }
        
aller dans lieu en bas trouver le lieuNum correspondant = numListe + 1, ici 4
mettre l'evenement dans le if  (eventNum == 2 ), ici cela imprime une autre image sur celle qu'on a de base 

 if (lieuNum == 4)
                {
                        spriteBatch.DrawString(text, "JE SUIS LE ROI \n", positionTexte, Color.White);
                        positionTexte.Y += (int)(Globals.EcranHeigth/30);
                        if (eventNum == 2 ) {
                            spriteBatch.Draw(
                                TextureBonus,
                                new Rectangle(0, 0, largeurImage, hauteurImage),
                                Color.White
                            );          