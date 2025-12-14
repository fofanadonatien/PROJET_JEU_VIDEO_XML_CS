using System.Collections.Generic;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Test2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Texture2D _gem;
    private Texture2D _player;
    private SpriteFont _xp;
    private Texture2D _portes;
    
    //Epee
    private Texture2D _textureEpee;
    private Vector2 _positionEpee;
    private bool _estDansUnDonjon = false;
        
    private List<Lieu> _lieux;
    private Texture2D _auberge;
    private Texture2D _sorcier;
    private Texture2D _monstre;
    private Texture2D _king;
    private Texture2D _monstreB;
    private Texture2D _king2;
    private Song musique;

    // parser et DOM
    public Player _player2;
    private XmlSerializerJeu Jsauvegarde;
    public static DataCombat.DOM2Xpath app = new DataCombat.DOM2Xpath("../../../XML/DataCombat.xml");     
    public static DataCombat data = new DataCombat(app);          

    private KeyboardState _oldKeyboardState; //Pour memoriser l'etat precedent ajout max
    
    
    




    public Game1()
    {
        // appel de classe qui Reader
        string cheminfile = "../../../XML/XmlJeu.xml";
        JeuXmlReader.QuestionsReader(cheminfile);
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = Globals.EcranWidth;
        _graphics.PreferredBackBufferHeight = Globals.EcranHeigth;
    }

    protected override void Initialize()
    {
        //charger donnees
        Jsauvegarde = new XmlSerializerJeu(); 
        Jsauvegarde.Charger();
        
        //Initialiser la classe
        DataCombat.VersCSharp(app, data);
        
        
        // Charge Texture depuis Content
        _player = Content.Load<Texture2D>("player/little-man-1-player(1)");
        _xp = Content.Load<SpriteFont>("texte/xp");

        _portes = Content.Load<Texture2D>("images/portails3");
        _auberge = Content.Load<Texture2D>("lieux/auberge");
        _sorcier = Content.Load<Texture2D>("lieux/sorcier");
        _monstre = Content.Load<Texture2D>("lieux/monstre");
        _monstreB = Content.Load<Texture2D>("lieux/monstrebattu");
        _king = Content.Load<Texture2D>("lieux/king");
        _king2 = Content.Load<Texture2D>("lieux/king2");
        _positionEpee = new Vector2(Globals.EcranWidth/ 2 + 100,Globals.EcranHeigth/ 2 + 50);

        int sizeXp = (int)((Globals.EcranWidth) / _player.Width) * 2;
        int sizeYp = (int)((Globals.EcranHeigth) / _player.Height) * 3;

        _player2 = new Player(0, 0, sizeXp, sizeYp, 15.0f, _player);

        int xHaut, yHaut, xbas, ybas;
        int xGauche, yGauche, xDroite, yDroite;
        xHaut = Globals.EcranWidth / 2;
        yHaut = 0;
        xbas = Globals.EcranWidth / 2;
        ybas = Globals.EcranHeigth - _portes.Height / 5;
        xGauche = 0;
        yGauche = Globals.EcranHeigth / 2;
        xDroite = Globals.EcranWidth - _portes.Width / 5;
        yDroite = Globals.EcranHeigth / 2;

        _lieux = new List<Lieu>();
        _lieux.Add(new Lieu("auberge", xHaut, yHaut, 128, 128, _portes, _auberge, _xp, 1, _auberge, 0));
        _lieux.Add(new Lieu("sorcier", xGauche, yGauche, 128, 128, _portes, _sorcier, _xp, 2, _sorcier, 0));
        _lieux.Add(new Lieu("monstre", xbas, ybas, 128, 128, _portes, _monstre, _xp, 3, _monstreB, 0));
        _lieux.Add(new Lieu("king", xDroite, yDroite, 128, 128, _portes, _king, _xp, 4, _king2, 0));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gem = Content.Load<Texture2D>("images/cartes2");
        _xp = Content.Load<SpriteFont>("texte/xp");
        _textureEpee = Content.Load<Texture2D>("images/epee3");
        musique = Content.Load<Song>("musique/medieval-background-348171");
        
        
        MediaPlayer.Play(musique);
        MediaPlayer.IsRepeating = true;
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        KeyboardState currentKeyboardState = Keyboard.GetState(); //recupere l'etat actuel du clavier

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            currentKeyboardState.IsKeyDown(Keys.Escape))
        {
            Exit();
            //sauvegarder donnees
            Jsauvegarde.Sauvegarder();
            DataCombat.VersXml(Game1.app, Game1.data);      
            Game1.app.save("../../../XML/DataCombat.xml");  
        }
        
        _player2.UpdatePlayer();
        Globals.TotalGameTimeSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        _estDansUnDonjon = false;
        
        for (int i = 0; i < _lieux.Count; i++)
        {
            var lieu = _lieux[i];
            bool contact = lieu.VerifierContact(_player2.PositionX, _player2.PositionY,
                _player2.SizeX, _player2.SizeY);
            if (contact == true)
            {
                _estDansUnDonjon = true;
            }
            //ROI
            // changement d'image roi pas sympa
            if (Keyboard.GetState().IsKeyDown(Keys.I) && contact == true && i == 3 && Globals.epeeRamasse == true)
            {
                _lieux[3].eventNum = 10;
            }

            // changement d'image roi plus cool
            if (Keyboard.GetState().IsKeyDown(Keys.P) && contact == true && i == 3)
            {
                _lieux[3].eventNum = 20;
            }

            //Aubergiste        
            // Aubergiste qui boit un peu trop 
            if (Keyboard.GetState().IsKeyDown(Keys.O) && contact == true && i == 0)
            {
                _lieux[0].eventNum = 10;
            }

            //Monstre
            // Le monstre est battu 
            if (Keyboard.GetState().IsKeyDown(Keys.O) && contact == true && i == 2)
            {
                _lieux[2].eventNum = 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && contact == true && i == 2)
            {   
                _lieux[2].eventNum = 20;
            }

            //sorcier        
            if (contact == true && i == 1)
            {
                // On verifie : Est-ce que j'appuie sur O maintenant ET je n'appuyais pas avant (pour eviter OOOOOOO et ne voir que la derniere question)
                lieu.UpdateLieu(currentKeyboardState, _oldKeyboardState);
            }

            //xp
            // boost Xp
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Globals.Xpscore += 50;
            }
        }
        //epee
        Vector2 positionJoueur= new Vector2(_player2.PositionX, _player2.PositionY);
        float distance = Vector2.Distance(positionJoueur,_positionEpee);

        if (Globals.epeeRamasse == false && _estDansUnDonjon == false && distance < 120 && Keyboard.GetState().IsKeyDown(Keys.E))
        {
            Globals.epeeRamasse = true;
            //ajouter dans l'inventaire
            Globals.Xpscore += 10000;
        }
        _oldKeyboardState = currentKeyboardState; //met a jour l'ancien etat 
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        // Begin the sprite batch to prepare for rendering.
        _spriteBatch.Begin(default, null, SamplerState.PointWrap);
        float scaleX = (float)Window.ClientBounds.Width / _gem.Width;
        float scaleY = (float)Window.ClientBounds.Height / _gem.Height;

        // Impression Carte
        _spriteBatch.Draw(
            _gem,
            Vector2.Zero,
            null,
            Color.White,
            0.0f,
            Vector2.Zero,
            new Vector2(scaleX, scaleY),
            SpriteEffects.None,
            0.0f
        );
        // Impression portes
        for (int i = 0; i < _lieux.Count; i++)
        {
            _lieux[i].DrawPorte(_spriteBatch);
        }

        // Impression Player
        _spriteBatch.Draw(
            _player2.Texture,
            new Rectangle((int)_player2.PositionX, (int)_player2.PositionY, _player2.SizeX, _player2.SizeY),
            Color.White
        );
        _spriteBatch.End();

        // Images des lieux (par dessus tout si contact)
        _spriteBatch.Begin(default, null, SamplerState.PointWrap);
        for (int i = 0; i < _lieux.Count; i++)
        {
            _lieux[i].DrawLieu(_spriteBatch);
        }
        _spriteBatch.End();

        // Texte Xp et temps
        _spriteBatch.Begin(default, samplerState: SamplerState.PointWrap);
        _spriteBatch.DrawString(_xp, "Xp du Joueur : " + Globals.Xpscore, new Vector2(Globals.EcranWidth / 4, 20), Color.White,
            0.0f,
            Vector2.Zero,
            new Vector2(1.3f, 1.3f),
            SpriteEffects.None,
            0.0f
        );
        _spriteBatch.DrawString(_xp, "Vie : " + Globals.Life, new Vector2(Globals.EcranWidth / 4, 40), Color.White,
            0.0f,
            Vector2.Zero,
            new Vector2(1.3f, 1.3f),
            SpriteEffects.None,
            0.0f
        );
        
        //epee
        if (Globals.epeeRamasse == false && _estDansUnDonjon == false) 
        {
            _spriteBatch.Draw(_textureEpee,_positionEpee, null,Color.White,0f,Vector2.Zero,0.07f,SpriteEffects.None,0f);//dessine l epee
            Vector2 positionJoueur = new Vector2(_player2.PositionX, _player2.PositionY);
            if (Vector2.Distance(positionJoueur, _positionEpee) < 120)
            {
                _spriteBatch.DrawString(_xp, "Appuie sur E", new Vector2(_positionEpee.X - 20, _positionEpee.Y - 30), Color.Yellow);
            }
        }
        
        _spriteBatch.DrawString(_xp, "Temps: " + Globals.GetFormattedGameTime(), new Vector2(10, 10), Color.White);
        _spriteBatch.End();
        
        

        base.Draw(gameTime);
    }
}