using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld29
{
    static class Util
    {
        public static SpriteFont Font;
        public static Texture2D Texture;
        public static Random Random = new Random();
        private static Random random = new Random();

        static int happyClicks = 0;
        static int sadClicks = 0;
        
        public static Feelings Feelings = Feelings.VeryHappy;

        static float counter = 0;

        public static void LoadContent(Game game)
        {
            Font = game.Content.Load<SpriteFont>("Font");

            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[1] { Color.White });
        }

        public static int Range(int min, int max)
        {
            return Random.Next(min, max + 1);
        }

        public static bool Chance(double chance)
        {
            return random.NextDouble() < chance;
        }

        public static void Update(float elapsed)
        {
            counter += elapsed;

            if (counter >= 5)
            {
                GetSadder();
                counter -= 5;
            }
        }

        public static void GetHappier()
        {
            if (Feelings != ld29.Feelings.VeryHappy)
            {
                Feelings = (Feelings)(int)Feelings - 1;
            }
        }

        public static void GetSadder()
        {
            if (Feelings != ld29.Feelings.VerySad)
            {
                Feelings = (Feelings)(int)Feelings + 1;
            }
        }

        public static void Clicked(CounterType type)
        {
            switch (type)
            {
                case CounterType.Happy:
                    happyClicks++;
                    break;
                case CounterType.Sad:
                    sadClicks++;
                    if (sadClicks >= 4)
                    {
                        sadClicks = 0;
                        GetHappier();
                    }
                    break;
                default:
                    break;
            }
        }

        public static bool ShouldSpawnHappyThought()
        {
            switch (Feelings)
            {
                case Feelings.VeryHappy:
                    return Util.Chance(0.5);
                case Feelings.Happy:
                    return Util.Chance(0.4);
                case Feelings.Indiferent:
                    return Util.Chance(0.3);
                case Feelings.Sad:
                    return Util.Chance(0.2);
                case Feelings.VerySad:
                    return Util.Chance(0.3);
                default:
                    return Util.Chance(0.9);
            }
        }
    }

    public enum Feelings
    {
        VeryHappy,
        Happy,
        Indiferent,
        Sad,
        VerySad
    }
}
