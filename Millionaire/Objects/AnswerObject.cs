using Microsoft.Xna.Framework;

namespace Millionaire.Objects
{
    class AnswerObject : TextBoxObject
    {
        public readonly int ansNumber;
        TextGameObject ansLetter;
        public bool canPick = true;

        public AnswerObject(string id, int ansNumber) : base (id)
        {
            this.ansNumber = ansNumber;

            ansLetter = new TextGameObject("", new string[4] { "A", "B", "C", "D" }[ansNumber]);
            ansLetter.SetSpriteFont("Fonts/LargeSpriteFont");
            ansLetter.color = new Color(0, 100, 240);
            ansLetter.scale = 0.5f;

            Add(ansLetter);
        }

        public AnswerObject Select()
        {
            boxSpriteFill.color = new Color(255, 185, 0);
            return this;
        }

        public void Correct()
        {
            boxSpriteFill.color = Color.LimeGreen;
        }

        public void False()
        {
            boxSpriteFill.color = Color.Red;
        }

        public override void Clear()
        {
            boxSpriteFill.color = Color.Black;
            visible = true;
            canPick = true;
            base.Clear();
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            ansLetter.Position = new Vector2(position.X + 30, position.Y + 20);
            
            base.Update(gameTime, inputHelper);
        }
    }
}
