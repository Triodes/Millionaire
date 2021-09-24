using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{
    class TextBoxObject : GameObjectList
    {
        SpriteGameObject boxSprite;
        protected SpriteGameObject boxSpriteFill;

        TextGameObject textObject;

        public int width;
        public int height;

        public Vector2 finalPosition;

        private bool animate = false;

        public TextBoxObject(string id, Alignment align = Alignment.Left, string asset = "Images/answer") : base (id)
        {
            boxSprite = new SpriteGameObject("", asset);
            Add(boxSprite);

            boxSpriteFill = new SpriteGameObject("", asset + "_fill");
            boxSpriteFill.color = Color.Black;
            Add(boxSpriteFill);

            textObject = new TextGameObject("", "");
            textObject.SetSpriteFont("Fonts/SpriteFont");
            textObject.align = align;

            Add(textObject);

            width = boxSprite.width;
            height = boxSprite.height;
        }

        public void SetText(string text)
        {
            textObject.text = text;
        }

        public void Animate()
        {
            animate = true;
        }

        public virtual void Clear()
        {
            animate = false;
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            float x = Position.X;
            if (Position.X > finalPosition.X && animate)
                x = Math.Max(Position.X - 1000f * (float)gameTime.ElapsedGameTime.TotalSeconds, finalPosition.X);

            if (Position.X < finalPosition.X && animate)
                x = Math.Min(Position.X + 1000f * (float)gameTime.ElapsedGameTime.TotalSeconds, finalPosition.X);

            position = new Vector2(x, position.Y);

            boxSprite.Position = position;
            boxSprite.width = width;
            boxSprite.height = height;

            boxSpriteFill.Position = position;
            boxSpriteFill.width = width;
            boxSpriteFill.height = height;

            bool twoLine = textObject.text.Contains('\n');

            if (textObject.align == Alignment.Center)
                textObject.Position = new Vector2(position.X + width / 2, position.Y + (twoLine ? 19 : 35));
            else
                textObject.Position = new Vector2(position.X + 80, position.Y + (twoLine ? 19 : 35));

            base.Update(gameTime, inputHelper);
        }
    }
}
