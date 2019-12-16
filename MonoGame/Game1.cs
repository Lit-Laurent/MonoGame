using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGame.Models;

namespace MonoGame
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Player> _pSprites;

        const int TargetWidth = 480;
        const int TargetHeight = 270;
        Matrix Scale;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                //PreferredBackBufferWidth = 640,
                //PreferredBackBufferHeight = 480,
                IsFullScreen = false
            };

            Content.RootDirectory = "Content";
            this.Window.Title = "Game";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            float scaleX = graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _pSprites = new List<Player>()
            {
                new Player(new Dictionary<string, Animation>()
                {
                    {"playerrunright", new Animation(Content.Load<Texture2D>("player/playerrunright"), 6) },
                    {"playerrunleft", new Animation(Content.Load<Texture2D>("player/playerrunleft"), 6) },
                    {"playeridleright", new Animation(Content.Load<Texture2D>("player/playeridleright"), 4) },
                    {"playeridleleft", new Animation(Content.Load<Texture2D>("player/playeridleleft"), 4) },
                })
                {
                    Position = new Vector2(100, 100),

                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                    },
                },
            };
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

            foreach (var player in _pSprites)
                player.Update(gameTime, _pSprites);

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

            base.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            foreach (var player in _pSprites)
                player.Draw(spriteBatch, 4);
            spriteBatch.End();
        }
    }
}
