
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker_Summative
{
    public class Brick
    {
        private Texture2D _texture;
        private Rectangle _rect;
        private Rectangle _colored;
        private Color _color;
        public Brick(Texture2D texture, Rectangle rect, Color color)
        {
            _texture = texture;
            _rect = rect;
            _colored = new(rect.X + 2, rect.Y + 2, rect.Width- 4, rect.Height -4);
            _color = color;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, _rect,Color.DarkGray);
            sb.Draw(_texture, _colored, _color);
        }
    }
}
