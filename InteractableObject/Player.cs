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
    class Player : MoveAbleBase
    {
        #region Private fields
        private Animator _animator;
        private Dictionary<StateOfObject, Texture2D> _listofsheets;
        private Game _game;
        private StateOfObject _playerstate;
        private Texture2D _currentTexture;
        #endregion
        #region Constructor
        public Player(Vector2 position, Texture2D texture, SpriteBatch spriteBatch, Game game, int rows = 1, int columns = 1, int amountframes = 1) : base(position, texture, spriteBatch)
        {
            _game = game;
            _playerstate = StateOfObject.Idle;
            _listofsheets = new Dictionary<StateOfObject, Texture2D>();
            GetListOfSheets("GunWoman/GunWoman");
            //We know that our main character is going to have animation so we don't need to check if it is a spritesheet or not. We know it is.
            _animator = new Animator(spriteBatch, new Point(columns, rows), amountframes);            
        }
        #endregion
        #region Public Methods
        public void KeyboardInput(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _playerstate = StateOfObject.Walking;
                Movement += new Vector2(-1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _playerstate = StateOfObject.Walking;
                Movement += new Vector2(1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.S)) { Movement += new Vector2(0, 1); }
            if (keyboardState.IsKeyDown(Keys.W)) { Movement += new Vector2(0, -1); }
            else _playerstate = StateOfObject.Idle;
            //Simulate friciton
            Movement -= Movement * new Vector2(.1f, .1f);
            //Update position based on movement
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 30;
        }

        public override void Draw()
        {
            _animator.DrawSpriteSheetFrame(_listofsheets[_playerstate], Position);
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
                    _listofsheets.Add(StateOfObject.Idle, _game.Content.Load<Texture2D>(searchstring));
                }
                if (searchstring.Contains("WALK"))
                {
                    _listofsheets.Add(StateOfObject.Walking, _game.Content.Load<Texture2D>(searchstring));
                }
            }
        }
        #endregion
    }
}
