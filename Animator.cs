using Arsenal.InteractableObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arsenal.InteractableObject.ObjectState;

namespace Arsenal
{
    class Animator
    {
        private int _currentframe;
        private int _frameUpdateSpeed;
        private int _timeSinceLastFrame;
        private Texture2D pixel;
        private SpriteBatch _spriteBatch;       
        private Dictionary<StateOfObject, Texture2D> _animationDictionary;
        private StateOfObject _currentstate;

        /// <summary>
        /// Animation should only handle sheets textures
        /// </summary>
        public Animator(SpriteBatch spriteBatch, int frameUpdateSpeed, GraphicsDevice gd)
        {
            _spriteBatch = spriteBatch;
            _frameUpdateSpeed = frameUpdateSpeed;
            _animationDictionary = new Dictionary<StateOfObject, Texture2D>();
            pixel = new Texture2D(gd, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.Black });// so that we can draw whatever color we want on top of it
        }
        public void DrawSpriteSheetFrame(CustomTexture2D ct, Vector2 position, StateOfObject soo, GameTime gt, SpriteFont sf)
        {
            if (!_currentstate.Equals(soo))
            {
                _currentframe = 0;
            }
            _currentstate = soo;
            
            List<SpriteFrameInfo> currentLoopsheet = ct.SubTexture.FindAll(x => x.name.Contains(soo.ToString()));
            _timeSinceLastFrame += gt.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > _frameUpdateSpeed)
            {
                _timeSinceLastFrame -= _frameUpdateSpeed;
                if (_currentframe <= currentLoopsheet.Count - 1)
                {
                    _currentframe++;
                }
                else _currentframe = 0; 
            }
            var t = currentLoopsheet.Where(x => x.name.Contains(_currentframe.ToString())).FirstOrDefault();
            Vector2 origin = new Vector2(t.width, t.height);
            var rec = new Rectangle(0,0, 400,200);
            var e = new Rectangle(new Point(t.x, t.y), new Point(t.width, t.height));

            _spriteBatch.Begin();
            _spriteBatch.DrawString(sf, "Currentsheet: " + t.name, new Vector2(100), Color.White);
            _spriteBatch.DrawString(sf, "Position X: " + position.X, new Vector2(100,150), Color.White);
            _spriteBatch.DrawString(sf, "Position Y: " + position.Y, new Vector2(100,200), Color.White);
            _spriteBatch.DrawString(sf, "SheetCord X: " + t.x, new Vector2(100, 250), Color.White);
            _spriteBatch.DrawString(sf, "SheetCord Y: " + t.y, new Vector2(100, 300), Color.White);
            _spriteBatch.DrawString(sf, "Rectangle Width: " + e.Width, new Vector2(100, 350), Color.White);
            _spriteBatch.DrawString(sf, "Rectangle H: " + e.Height, new Vector2(100, 400), Color.White);
            if (t.name.Contains("AttackLeft09"))
            {
                _spriteBatch.Draw(ct.Texture, position, new Rectangle(new Point(t.x, t.y), new Point(t.width - 40, t.height)), Color.White, 0, origin, 1, SpriteEffects.None, 1);
            }
            else { _spriteBatch.Draw(ct.Texture, position, new Rectangle(new Point(t.x, t.y), new Point(t.width, t.height)), Color.White, 0, origin, 1, SpriteEffects.None, 1); }

            DrawBorder(new Rectangle(new Point((int)position.X, (int)position.Y), new Point(t.width, t.height)), 1, Color.Black, position);

            _spriteBatch.End();
        }

        /*public void LoopTroughSheet()
        {            
            ++_currentFrame.X;
            ++_countframes;
            if (_countframes >= _amountOfFrames)
            {
                _currentFrame.X = 0;
                _currentFrame.Y = 0;
                _countframes = 0;
            }

            if (_currentFrame.X >= _rac.X)
            {
                _currentFrame.X = 0;
                ++_currentFrame.Y;
                if (_currentFrame.Y >= _rac.Y)
                {
                    _currentFrame.Y = 0;
                    ++_countframes;
                    if (_countframes >= _amountOfFrames)
                    {
                        _countframes = 0;
                        _currentFrame.X = 0;
                        _currentFrame.Y = 0;
                    }
                }
            }
        }*/
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, Vector2 position)
        {
            // Draw top line
            _spriteBatch.Draw(pixel,new Rectangle((int)position.X-rectangleToDraw.Width, (int)position.Y-rectangleToDraw.Height, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            _spriteBatch.Draw(pixel,new Rectangle((int)position.X-rectangleToDraw.Width, (int)position.Y-rectangleToDraw.Height, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            //// Draw right line
            _spriteBatch.Draw(pixel,new Rectangle((int)position.X,(int)position.Y-rectangleToDraw.Height,thicknessOfBorder,rectangleToDraw.Height), borderColor);
            //// Draw bottom line
            _spriteBatch.Draw(pixel,new Rectangle((int)position.X-rectangleToDraw.Width,(int)position.Y,rectangleToDraw.Width,thicknessOfBorder), borderColor);
        }
    }
    public static class GG
    {
        public static void CreateBorder(this Texture2D texture, int borderWidth, Color borderColor)
        {
            Color[] colors = new Color[texture.Width * texture.Height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    bool colored = false;
                    for (int i = 0; i <= borderWidth; i++)
                    {
                        if (x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i)
                        {
                            colors[x + y * texture.Width] = borderColor;
                            colored = true;
                            break;
                        }
                    }

                    if (colored == false)
                        colors[x + y * texture.Width] = Color.Transparent;
                }
            }

            texture.SetData(colors);
        }
    }
}
