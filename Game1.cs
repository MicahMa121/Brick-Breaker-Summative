using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Brick_Breaker_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<Microsoft.Xna.Framework.Color> _colors;
        Texture2D rectTex;
        List<Brick> bricks;
        Random gen = new Random();
        int width, height;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;
            width = _graphics.PreferredBackBufferWidth;
            height = _graphics.PreferredBackBufferHeight;
        }
        public Microsoft.Xna.Framework.Color XNAColor(System.Drawing.Color color)
        {
            return new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _colors = new List<Microsoft.Xna.Framework.Color>();
            foreach (KnownColor known in Enum.GetValues(typeof(KnownColor)))
            {
                _colors.Add(XNAColor(System.Drawing.Color.FromKnownColor(known)));
            }
            base.Initialize();
            bricks = new List<Brick>();
            GenerateBricks(15);
        }
        protected void GenerateBricks(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    bricks.Add(new(rectTex, new Microsoft.Xna.Framework.Rectangle(j*width/18, i*height/36, width / 18, height / 36), _colors[gen.Next(_colors.Count)]));
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            rectTex = Content.Load<Texture2D>("rectangle");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var item in bricks)
            {
                item.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
