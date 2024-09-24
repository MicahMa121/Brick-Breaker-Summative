using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Brick_Breaker_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //List<Microsoft.Xna.Framework.Color> _colors;
        List<Color> _colors;
        Texture2D rectTex,ballTex,padTex,powTex;
        List<Brick> _bricks;
        List<Pow> _pows;
        Random gen = new Random();
        int width, height;
        Paddle _pad;
        Ball _ball;
        SpriteFont _font;
        int score;
        float durability;
        enum screen 
        { 
            intro, 
            game, 
            end
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;
            width = _graphics.PreferredBackBufferWidth;
            height = _graphics.PreferredBackBufferHeight;
            _colors = new List<Color>();
            _colors.Add(Color.Red);
            _colors.Add(Color.Orange);
            _colors.Add(Color.Yellow);
            _colors.Add(Color.GreenYellow);
            _colors.Add(Color.Green);
            _colors.Add(Color.Blue);
            _colors.Add(Color.Indigo);
            _colors.Add(Color.Violet);
        }
        public Microsoft.Xna.Framework.Color XNAColor(System.Drawing.Color color)
        {
            return new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            /*
            _colors = new List<Microsoft.Xna.Framework.Color>();
            foreach (KnownColor known in Enum.GetValues(typeof(KnownColor)))
            {
                _colors.Add(XNAColor(System.Drawing.Color.FromKnownColor(known)));
            }*/
            base.Initialize();
            score = 0;
            durability = 1;
            _pad = new Paddle(padTex,new Rectangle(width/2-width/24, height*7/8, width/12, height/36));
            _bricks = new List<Brick>();
            _pows = new List<Pow>();
            _ball = new Ball(ballTex, new Rectangle(width/2- width / 72, height*3/4,width/36,width/36),
                new Vector2(gen.Next(1,5),-gen.Next(1,5)));
            GenerateBricks(15);
        }
        protected void GenerateBricks(int rows)
        {
            int health = rows/_colors.Count+2;
            for (int i = 0; i < rows; i++)
            {
                if (health > 1 && i%_colors.Count == 0)
                {
                    health--;
                }
                for (int j = 0; j < 18; j++)
                {
                    _bricks.Add(new Brick(rectTex, new Rectangle(j*width/18, i*height/36, width / 18, height / 36), _colors[i%_colors.Count],health));
                }
            }
            //this.Window.Title = health.ToString();
        }
        protected void GeneratePow(Point pos)
        {
            Pow pow = new Pow(powTex,new Rectangle(pos.X - width / 72, pos.Y - width / 72, width/36,width/36),new Vector2(0,1));
            _pows.Add(pow);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            rectTex = Content.Load<Texture2D>("rectangle");
            ballTex = Content.Load<Texture2D>("circle");
            padTex = Content.Load<Texture2D>("paddle");
            _font = Content.Load<SpriteFont>("spritefont");
            powTex = Content.Load<Texture2D>("pow");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _pad.Update(gameTime, Keyboard.GetState());
            _ball.Update(gameTime,_pad.Rectangle,_bricks);
            if (_ball.IsHit)
            {
                score += 100;
                if (score > 1000)//&&0== gen.Next(24))
                {
                    GeneratePow(_ball.CollisionPoint);
                }
            }
            for (int i = 0; i < _pows.Count; i++)
            {
                _pows[i].Update(gameTime);
            }
                //this.Window.Title = _pad.Rectangle.X.ToString();
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var item in _bricks)
            {
                item.Draw(_spriteBatch);
            }
            _pad.Draw(_spriteBatch);
            foreach(var item in _pows)
            {
                item.Draw(_spriteBatch);
            }
            _ball.Draw(_spriteBatch);
            _spriteBatch.DrawString(_font, "Score: " + score,new Vector2(width/24,height*11/12),Color.White);
            _spriteBatch.DrawString(_font, "Durability: " + durability.ToString("0%"), new Vector2(width* 18 / 24, height * 11 / 12), Color.White);
            //_spriteBatch.Draw(padTex, new Rectangle(0,0,100,100),Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
