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
        private Point _frameSize; //Width and Height of a single frame in spritesheet
        private Point _currentFrame; //Current sprite frame
        private Point _rac; //How many row and columns there in the spritesheet
        private int _amountOfFrames;
        private int _countframes;
        private SpriteBatch _spriteBatch;
        private Dictionary<StateOfObject, Texture2D> _animationDictionary;

        /// <summary>
        /// Animation should only handle sheets textures
        /// </summary>
        public Animator(SpriteBatch spriteBatch, Point rac, int amountOfFrames)
        {
            _spriteBatch = spriteBatch;
            //_frameSize = new Point(texture.Width / sheetSize.X, texture.Height / sheetSize.Y);
            _currentFrame = new Point(0, 0);
            _rac = rac;
            _amountOfFrames = amountOfFrames;
            _countframes = 0;
            _animationDictionary = new Dictionary<StateOfObject, Texture2D>();
            
        }

        public void DrawSpriteSheetFrame(Texture2D texture, Vector2 position)  
        {
            _spriteBatch.Begin();
            //_spriteBatch.Draw(texture, position, new Rectangle(_currentFrame.X * frameSize.X, _currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
            _spriteBatch.Draw(texture, position, new Rectangle(_currentFrame.X * (texture.Width / _rac.X), _currentFrame.Y * (texture.Height / _rac.Y), (texture.Width / _rac.X), (texture.Height / _rac.Y)), Color.White);
            _spriteBatch.End();
        }

        

        public void LoopTroughSheet()
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
        }
    }
}
