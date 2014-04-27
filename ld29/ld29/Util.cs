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
    }
}
