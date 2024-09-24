using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
        public Point CollisionPoint;
        public bool IsHit;
        public bool PlayerHit;
        private List<Brick> brickFall = new List<Brick>();
        public Ball(Texture2D tex, Rectangle rect, Vector2 velocity)
        {
            _tex = tex;
            _rect = rect;
            _height = rect.Y * 4 / 3;
            _position = new Vector2(rect.X, rect.Y);
            _velocity = velocity;
            _speed = 0.75f;
        }
        public void Update(GameTime gameTime,Rectangle padRect,List<Brick> bricks)
        {
            IsHit = false;
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
                PlayerHit = true;
            }
            if (_rect.Intersects(padRect))
            {
                Collision(padRect);
                
            }
            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rect.Intersects(bricks[i].Rectangle))
                {
                    Collision(bricks[i].Rectangle);
                    bricks[i].Health -= 1;
                    if (bricks[i].Health <= 0)
                    {
                        IsHit = true;
                        CollisionPoint = bricks[i].Rectangle.Center;
                        brickFall.Add(bricks[i]);
                        bricks.RemoveAt(i);
                        i--;
                    }
                    break;
                }
            }
            for (int i = 0; i < brickFall.Count; i++)
            {
                brickFall[i].Update();
                if (brickFall[i].Rectangle.Y > _height)
                {
                    brickFall.RemoveAt(i);
                    i--;
                }
            }
        }
        private void Collision(Rectangle rect)
        {
            int myhalfw = _rect.Width / 2;
            int myhalfh = _rect.Height / 2;
            int halfw = rect.Width / 2;
            int halfh = rect.Height / 2;

            int xmindis = myhalfw + halfw;
            int ymindis = myhalfh + halfh;
            int xdiff = _rect.Center.X - rect.Center.X;
            int ydiff = _rect.Center.Y - rect.Center.Y;

            int xdepth;
            if (xdiff > 0)
            {
                xdepth = xmindis - xdiff;
            }
            else
            {
                xdepth = -xmindis - xdiff;
            }

            int ydepth;
            if (ydiff > 0)
            {
                ydepth = ymindis - ydiff;
            }
            else
            {
                ydepth = -ymindis - ydiff;
            }

            if (Math.Abs(xdepth) < Math.Abs(ydepth))
            {
                if (xdepth < 0)
                {
                    _position.X = rect.Left - _rect.Width;
                }
                else 
                {
                    _position.X = rect.Right;
                }
                _velocity.X *= -1;
            }
            else
            {
                if (ydepth < 0)
                {
                    _position.Y = rect.Top - _rect.Height;
                }
                else if (ydepth > 0)
                {
                    _position.Y = rect.Bottom;
                }
                _velocity.Y *= -1;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_tex, _rect,Color.Black);
            foreach (var item in brickFall)
            {
                item.Draw(sb);
            }
        }
    }
}
