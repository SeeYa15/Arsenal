using Arsenal.InteractableObject;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arsenal.InteractableObject.ObjectState;

namespace Arsenal
{
    class CustomTexture2D
    {
        public string Imagepath { get; set; }
        public Texture2D Texture { get; set; }
        public List<SpriteFrameInfo> SubTexture { get; set; }

        public CustomTexture2D(string imagepath, List<SpriteFrameInfo> subtexture)
        {
            Imagepath = imagepath;
            SubTexture = subtexture;
        }

        public void FetchTextureFromContent(Game1 game)
        {
           Texture = game.Content.Load<Texture2D>(Imagepath);
        }
    }
}
