using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsenal
{
    class Sprite
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X,(int)Position.Y,Texture.Width, Texture.Height);
            }
        }
        public SpriteBatch SpriteBatch;

        public Sprite(Vector2 position, Texture2D texture, SpriteBatch spriteBatch)
        {
            Position = position;
            Texture = texture;
            SpriteBatch = spriteBatch;
        }

        public virtual void Draw()
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture,Position, Color.White);            
            SpriteBatch.End();
        }

        //public virtual void DrawWithRectangle(int row, int colum)
        //{
        //    SpriteBatch.Begin();
        //    SpriteBatch.Draw(Texture, Position, new Rectangle(new Point(), new Point(Texture.Width/colum,Texture.Height/row)), Color.White);
        //    SpriteBatch.End();
        //}
    }
}   
