using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace Test2
{
        public class Player
        {
                public float _positionX = 0;
                public float _positionY = 0;
                int _sizeX;
                int _sizeY;
                float _speed;
                Texture2D _texture;

                public float PositionX
                {
                        get => _positionX;
                        set => _positionX = value;
                }

                public float PositionY
                {
                        get => _positionY;
                        set => _positionY = value;
                }

                public int SizeX
                {
                        get => _sizeX;
                        set => _sizeX = value;
                }

                public int SizeY
                {
                        get => _sizeY;
                        set => _sizeY = value;
                }

                public float Speed
                {
                        get => _speed;
                        set => _speed = value;
                }

                public Texture2D Texture
                {
                        get => _texture;
                        set => _texture = value;
                }

                public Player(float x, float y, int sizeX, int sizeY, float speed, Texture2D texture2D)
                {
                        _positionX = x;
                        _positionY = y;
                        _sizeX = sizeX;
                        _sizeY = sizeY;
                        _speed = speed;
                        _texture = texture2D;
                }

                public void UpdatePlayer()
                { 
                        Mouvement();
                }

                private Vector2 velocity;
                
                
                
                public void Mouvement()
                {
                        velocity = Vector2.Zero;
                        if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                                velocity.Y--;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                                velocity.Y++;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                                velocity.X--;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                                velocity.X++;
                        }

                        if (velocity != Vector2.Zero)
                        {
                                velocity.Normalize();
                        }
                        PositionX += velocity.X * Speed;
                        PositionY += velocity.Y * Speed;
                        
                        //bloque au limites de l'écran de jeu
                        PositionX = MathHelper.Clamp(PositionX, 0, Globals.EcranWidth - SizeX);
                        PositionY = MathHelper.Clamp(PositionY, 0, Globals.EcranHeigth - SizeY);

                }
                

        }
                
}