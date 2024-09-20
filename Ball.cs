using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker_Summative
{
    public class Ball
    {
        private Texture2D _tex;
        private Rectangle _rect;
        private Vector2 _velocity;
        public Ball(Texture2D tex, Rectangle rect)
        {
            _tex = tex;
            _rect = rect;
            _velocity = Vector2.Zero;
        }
        public void Update()
        {
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_tex, _rect,Color.Black);
        }
    }
}
