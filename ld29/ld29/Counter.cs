using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld29
{
    public enum CounterType
    {
        Happy,
        Sad
    }

    class Counter
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public float Amplitude { get; set; }
        public float Period { get; set; }
        public Rectangle Rectangle { get; set; }
        public string Text { get; set; }
        public Color ColorFrom { get; set; }
        public Color ColorTo { get; set; }

        public CounterType Type { get; set; }

        float initialDuration;

        public Action OnCompleted;
        public float Elapsed { get; set; }
        public float Duration { get; set; }
        public bool Active
        {
            get { return Duration > 0 && Position.Y > 0; }
        }

        public static Counter CreateCounter()
        {
            return new Counter(Vector2.Zero);
        }

        public Counter(Vector2 position, float duration = 1f)
        {
            Position = position;
            Duration = duration;
            initialDuration = duration;
            Speed = Util.Range(80, 150) * -1;
            Amplitude = Util.Range(50, 400);
            Period = Util.Range(20, 50);

            if (Util.ShouldSpawnHappyThought())
            {
                ColorFrom = Color.Red;
                ColorTo = Color.Red;
                Text = "Happy happy happy";
                Type = CounterType.Happy;
            }
            else
            {
                ColorFrom = Color.Green;
                ColorTo = Color.Honeydew;
                Text = "saaaaad...";
                Type = CounterType.Sad;
            }
        }

        public void Update(float elapsed)
        {
            Duration -= elapsed;
            float x = (float)Math.Sin(Position.Y / Period) * Amplitude;
            Position += new Vector2(x, Speed) * elapsed;

            if (!Active)
            {
                if (OnCompleted != null)
                {
                    OnCompleted();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int padding = 10;

            //Text = "Hola [" + Duration.ToString("0.0") + "] ";
            Vector2 size = Util.Font.MeasureString(Text);
            Rectangle = new Rectangle((int)Position.X - padding, (int)Position.Y - padding, (int)size.X + padding * 2, (int)size.Y + padding * 2);
            spriteBatch.Draw(Util.Texture, Rectangle, Color.Lerp(ColorTo, ColorFrom, Duration / initialDuration));
            spriteBatch.DrawString(Util.Font, Text, Position, Color.Black);
        }
    }
}
