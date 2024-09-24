
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker_Summative
{
    public class Brick
    {
        private Texture2D _texture;
        private Rectangle _rect;
        public Rectangle Rectangle {  get { return _rect; } }
        private Rectangle _colored;
        private Color _color;
        private float _opacity;
        public int Health;
        private int _maxhealth;
        private Vector2 _vel;
        public Brick(Texture2D texture, Rectangle rect, Color color, int maxhealth)
        {
            _texture = texture;
            _rect = rect;
            _colored = new(rect.X + 2, rect.Y + 2, rect.Width- 4, rect.Height -4);
            _color = color;
            _opacity = 1.0f;
            _maxhealth = maxhealth;
            Health = maxhealth;
            _vel = new Vector2(0,-10);
        }
        public void Update()
        {
            Health = _maxhealth;
            _vel.Y += 1;
            _rect.Offset(_vel);
            _colored.Offset(_vel);
        }
        public void Draw(SpriteBatch sb)
        {
            _opacity = Health *1f/ _maxhealth;
            sb.Draw(_texture, _rect,Color.DarkGray*_opacity);
            sb.Draw(_texture, _colored, _color*_opacity);
        }
    }
}
