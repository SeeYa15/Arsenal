using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arsenal.Player;
using System;

namespace Arsenal
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Board gameboard;
        Texture2D pixel;
        /*Tiles*/
        Tile blockTile;
        /*Sprites*/
        Player.Player _player;
        SpriteFont _sf;

        float tempy, tempx;

        public Game1()
        {
            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 200);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            float ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;            
            
            // TODO: Add your initialization logic here
            tempx = ScreenWidth / 32.0f;
            tempy = ScreenHeight / 32.0f;
            if (tempx == Math.Floor(tempx) && tempy == Math.Floor(tempy))
            {
                graphics.PreferredBackBufferHeight = (int)ScreenHeight;
                graphics.PreferredBackBufferWidth = (int)ScreenWidth;
            }
            //else
            //{
            //    tempx = (int)ScreenWidth / 32;
            //    tempy = (int)ScreenHeight / 32;
            //    ScreenWidth = tempx * 32;
            //    ScreenHeight = tempy * 32;
            //    graphics.ToggleFullScreen();
            //}
            graphics.PreferredBackBufferHeight = (int)ScreenHeight;
            graphics.PreferredBackBufferWidth = (int)ScreenWidth;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _sf = Content.Load<SpriteFont>("File");
            _player = new Player.Player(new Vector2(500), spriteBatch, this, _sf, GraphicsDevice);

            gameboard = new Board(Content.Load<Texture2D>("Block_32"), (int)tempy, (int)tempx, spriteBatch);
            gameboard.GenerateRandomBoard();
            //var uri = new Uri("./Content/GunWoman/gunwomanIDLE.sheet", UriKind.Relative).ToString();
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here 

            _player.KeyboardInput(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _player.Draw(gameTime);
            gameboard.Draw();
            base.Draw(gameTime);
        }        
    }
}
