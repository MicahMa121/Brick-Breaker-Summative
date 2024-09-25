
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker_Summative
{
    public class Pow
    {
        private Texture2D _texture;
        public Texture2D Texture { get { return _texture; } }
        private Rectangle _rect;
        private Vector2 _pos;
        private Vector2 _vel;
        private float _speed;
        public Rectangle Rectangle { get { return _rect; } }
        public Pow(Texture2D texture, Rectangle rect, Vector2 vel)
        {
            _texture = texture;
            _rect = rect;
            _pos = new Vector2(rect.X,rect.Y);
            _vel = vel;
            _speed = 100f;
        }
        public void Update(GameTime gameTime)
        {
            _pos += _vel * (float)gameTime.ElapsedGameTime.TotalSeconds* _speed;
            _rect.X = (int) _pos.X;
            _rect.Y = (int) _pos.Y;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture,_rect,Color.White);
        }
    }
}
