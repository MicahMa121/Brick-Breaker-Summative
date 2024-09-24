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
        private float _speed = 0.5f;
        private Vector2 _velocity = Vector2.Zero;
        public Rectangle Rectangle { get { return _rect; } }
        public Paddle(Texture2D tex, Rectangle rect)
        {
            _tex = tex;
            _rect = rect;

            _pos = new Vector2(rect.X, rect.Y);
        }
        public void Update(GameTime gameTime,KeyboardState keyboardState)
        {
            _velocity = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A)|| keyboardState.IsKeyDown(Keys.Left))
            {
                _velocity += new Vector2(-1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                _velocity += new Vector2(1, 0);
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
                _velocity.Normalize();
            _pos += _velocity  * _speed * gameTime.ElapsedGameTime.Milliseconds;
            if (_pos.X < 0)
            {
                _pos.X = 0;
            }
            if (_pos.Y < _rect.Height * 24)
            { 
                _pos.Y = _rect.Height * 24;
            }
            if (_pos.X > _rect.Width * 11)
            {
                _pos.X = _rect.Width * 11;
            }
            if (_pos.Y > _rect.Height * 35)
            {
                _pos.Y = _rect.Height * 35;
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
