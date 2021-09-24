using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{

    public enum Alignment { Left, Right, Center }

    class TextGameObject : GameObject
    {
        private SpriteFont spriteFont;

        public Color color = Color.White;
        public string text;

        public Alignment align = Alignment.Left;

        public float scale = 1f;

        public TextGameObject(string id, string text)
            : base(id)
        {
            visible = true;
            spriteFont = Millionaire.ResourceManager.GetFont("Fonts/SpriteFont");
            this.text = text;
        }

        public void SetSpriteFont(string asset)
        {
            spriteFont = Millionaire.ResourceManager.GetFont(asset);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                Vector2 size = spriteFont.MeasureString(text);

                Vector2 drawPosition;
                if (align == Alignment.Right)
                    drawPosition = new Vector2(position.X - size.X * scale, position.Y);
                else if (align == Alignment.Center)
                    drawPosition = new Vector2(position.X - size.X * scale / 2f, position.Y);
                else
                    drawPosition = position;
                    

                spriteBatch.DrawString(spriteFont, text, drawPosition, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
                
        }
    }
}
