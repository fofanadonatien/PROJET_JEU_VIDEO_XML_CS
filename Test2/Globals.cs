using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test2;

public class Globals
{
    public static int EcranWidth = 1920 ;
    public static int EcranHeigth = 1080;
  
    public static string nom = "Bob";
    public static int Xpscore = 100;
    public static int Life = 3;
    public static int plumeNbr = 0;
    public static int armureNbr = 0;
    public static bool epeeRamasse = false;

    

 

   
    public static float TotalGameTimeSeconds { get; set; } = 0f;
    public SpriteBatch SpriteBatch { get; set; } =  null;

    
    public static string GetFormattedGameTime()
    {
        int minutes = (int)(TotalGameTimeSeconds / 60);
        int seconds = (int)(TotalGameTimeSeconds % 60);
        return $"{minutes:D2}:{seconds:D2}";
    }
}