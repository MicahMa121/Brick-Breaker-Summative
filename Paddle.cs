using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brick_Breaker_Summative
{
    public class Paddle
    {
        private Texture2D _tex;
        private Rectangle _rect;
        private Vector2 _pos;
        private float _speed = 75f;
        private Vector2 _velocity = Vector2.Zero;
        public Rectangle Rectangle { get { return _rect; } }
        public Vector2 Velocity { get { return _velocity; } }
        private int width,height;
        public Paddle(Texture2D tex, Rectangle rect)
        {
            _tex = tex;
            _rect = rect;

            _pos = new Vector2(rect.X, rect.Y);
            width = rect.Width*12;
            height = rect.Height*36;
        }
        public int Growth = 0;
        public void Update(GameTime gameTime,KeyboardState keyboardState)
        {
            
            if (keyboardState.IsKeyDown(Keys.A)|| keyboardState.IsKeyDown(Keys.Left))
            {
                _velocity += new Vector2(-2, 0);
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                _velocity += new Vector2(2, 0);
            }
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                _velocity += new Vector2(0, -1);
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                _velocity += new Vector2(0, 1);
            }
            if (_velocity != Vector2.Zero)
            {
                //_velocity.Normalize();
                _velocity *= 0.75f;
            }

            _pos += _velocity  * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_pos.X < 0)
            {
                _pos.X = 0;
            }
            if (_pos.Y < height*2/3)
            { 
                _pos.Y = height * 2 / 3;
            }
            if (_pos.X > width - _rect.Width)
            {
                _pos.X = width - _rect.Width;
            }
            if (_pos.Y > height - _rect.Height)
            {
                _pos.Y = height - _rect.Height;
            }

            if (Growth > 0)
            {
                _pos.X -= 9;
                _pos.Y -= 2;
                _rect.Width += 18;
                _rect.Height += 4;
                Growth--;
            }
            else if (Growth < 0)
            {
                _pos.X += 9;
                _pos.Y += 2;
                _rect.Width -= 18;
                _rect.Height -= 4;
                Growth++;
            }
            _rect.X = (int)_pos.X;
            _rect.Y = (int)_pos.Y;


        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_tex, _rect,Color.White);
        }
    }
}
