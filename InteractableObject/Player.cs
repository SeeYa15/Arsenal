using Arsenal.InteractableObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arsenal.InteractableObject.ObjectState;

namespace Arsenal.Player
{
    class Player
    {
        #region Private fields
        private Animator _animator;
        private Game1 _game;
        private StateOfObject _playerstate;
        private SpriteFont _sf;        
        #endregion
        #region Public Properties
        public Vector2 Position { get; set; }
        public Vector2 Movement { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public CustomTexture2D ct2d { get; set; }                
        #endregion
        #region Constructor
        public Player(Vector2 position, SpriteBatch spriteBatch, Game1 game, SpriteFont sf, GraphicsDevice gd)
        {
            _game = game;
            SpriteBatch = spriteBatch;
            _playerstate = StateOfObject.IDLE;
            Position = position;
            ct2d = JSONToTextureConverter.FetchJSONInformation(new Uri(@"./Content/", UriKind.Relative), "GunWomanSpriteInfo");
            ct2d.FetchTextureFromContent(game);
            //We know that our main character is going to have animation so we don't need to check if it is a spritesheet or not. We know it is.
            _animator = new Animator(spriteBatch, 400, gd);
            _sf = sf;
            
        }
        #endregion
        #region Public Methods
        public void KeyboardInput(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            var check = keyboardState.GetPressedKeys();
            if (check.Length > 0)
            {
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    _playerstate = StateOfObject.WALK;
                    Movement += new Vector2(-1, 0);
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    _playerstate = StateOfObject.WALK;
                    Movement += new Vector2(1, 0);
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    _playerstate = StateOfObject.ATTACK;
                }
            }
            else _playerstate = StateOfObject.IDLE;
            

            //Simulate friciton
            Movement -= Movement * new Vector2(.1f, .1f);
            //Update position based on movement
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 30;
        }

        public void Draw(GameTime gt)
        {
            _animator.DrawSpriteSheetFrame(ct2d, Position, _playerstate, gt, _sf);            
        }

        public void GetListOfSheets(string dir)
        {
            var uri = new Uri("./Content/" + dir, UriKind.Relative);
            if (uri == null)
            {
                throw new DirectoryNotFoundException();
            }
            foreach (var texture in Directory.GetFiles(uri.ToString()))
            {
                var texturename = Path.GetFileNameWithoutExtension(texture);
                var searchstring = string.Concat(dir, "\\", texturename);
                if (searchstring.Contains("IDLE"))
                {
                    //_listofsheets.Add(StateOfObject.Idle, _game.Content.Load<Texture2D>(searchstring));
                }
                if (searchstring.Contains("WALK"))
                {
                   // _listofsheets.Add(StateOfObject.Walking, _game.Content.Load<Texture2D>(searchstring));
                }
            }
        }
        
        #endregion
    }
}
