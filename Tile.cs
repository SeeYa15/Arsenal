using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/// <summary>
/// The purpose of this class is to 
/// </summary>
namespace Arsenal
{
    class Tile : Sprite
    {
        bool IsBlocked;

        public Tile(Vector2 position, Texture2D texture, SpriteBatch spriteBatch, bool isblocked)
        : base(position, texture, spriteBatch)        
        {
            IsBlocked = isblocked;
        }

        public override void Draw()
        {
            if (IsBlocked)
            {
                base.Draw(); 
            }
        }
    }
}
