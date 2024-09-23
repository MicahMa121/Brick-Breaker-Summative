using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;

namespace Brick_Breaker_Summative
{
    public class Ball
    {
        private Texture2D _tex;
        private Rectangle _rect;
        private Vector2 _velocity;
        private Vector2 _position;
        private float _speed;
        private int _height;
        public float Speed { get { return _speed; } set { _speed = value; } }
        public Ball(Texture2D tex, Rectangle rect, Vector2 velocity)
        {
            _tex = tex;
            _rect = rect;
            _height = rect.Y * 4 / 3;
            _position = new Vector2(rect.X, rect.Y);
            _velocity = velocity;
            _speed = 0.1f;
        }
        public void Update(GameTime gameTime,Rectangle padRect,List<Brick> bricks)
        {
            if (_velocity != Vector2.Zero)
            {
                _velocity.Normalize();
            }
            _position += _velocity * _speed * gameTime.ElapsedGameTime.Milliseconds;
            _rect.X = (int)_position.X;
            _rect.Y = (int)_position.Y;
            if (_position.X < 0)
            {
                _position.X = 0;
                _velocity.X *= -1;
            }
            if (_position.Y < 0)
            {
                _position.Y = 0;
                _velocity.Y *= -1;
            }
            if (_position.X > _rect.Width * 35)
            {
                _position.X = _rect.Width * 35;
                _velocity.X *= -1;
            }
            if (_position.Y > _height - _rect.Height )
            {
                _position.Y = _height - _rect.Height;
                _velocity.Y *= -1;
            }
            if (_rect.Intersects(padRect))
            {
                if (_velocity.Y > 0)
                {
                    _position.Y = padRect.Top - _rect.Height;
                }
                else if (_velocity.Y < 0)
                {
                    _position.Y = padRect.Bottom;
                }
                _velocity.Y *= -1;
            }
            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rect.Intersects(bricks[i].Rectangle))
                {
                    bricks.RemoveAt(i);
                    i--;
                    if (_velocity.Y > 0)
                    {
                        _position.Y = bricks[i].Rectangle.Top - _rect.Height;
                    }
                    else if (_velocity.Y < 0)
                    {
                        _position.Y = bricks[i].Rectangle.Bottom;
                    }
                    _velocity.Y *= -1;
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_tex, _rect,Color.Black);
        }
    }
}
