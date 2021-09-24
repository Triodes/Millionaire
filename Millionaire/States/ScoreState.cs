using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Millionaire.Objects;
using System.IO;

namespace Millionaire.States
{
    class ScoreState : ObjectRoot
    {
        public int score = 0;
        private string[] scores = new string[15];
        private string zeroScore;

        ScoreIndicator indicator;

        public ScoreState(Millionaire game) : base(game, "score")
        {
            StreamReader fileReader = new StreamReader("Scores.txt");

            zeroScore = fileReader.ReadLine();

            for (int i = 0; i < 15; i++)
            {
                scores[i] = fileReader.ReadLine();
            }
            fileReader.Close();

            SpriteGameObject scoreBg = new SpriteGameObject("scorebg", "Images/whitepixel");
            scoreBg.color = Color.Black;
            scoreBg.Position = new Vector2(100, 80);
            scoreBg.width = 400;
            scoreBg.height = Millionaire.Resolution.Y - 160;
            Add(scoreBg);

            for (int i = 0; i < 15; i++)
            {
                Color color = i % 5 == 0 ? Color.White : Color.Orange;

                TextGameObject txt = new TextGameObject("", (i+1).ToString());
                txt.Position = new Vector2(140, Millionaire.Resolution.Y - 130 - i * 38);
                txt.color = color;
                Add(txt);

                txt = new TextGameObject("", scores[i]);
                txt.Position = new Vector2(250, Millionaire.Resolution.Y - 130 - i * 38);
                txt.color = color;
                Add(txt);
            }

            indicator = new ScoreIndicator("indicator");
            Add(indicator);

            SpriteGameObject helpCallSprite = new SpriteGameObject("helpCallSprite", "Images/bellen");
            helpCallSprite.Position = new Vector2(1175, 200);
            helpCallSprite.width = helpCallSprite.height = 100;
            Add(helpCallSprite);

            TextGameObject helpCallText = new TextGameObject("helpCallText", "Vraag de leiding");
            helpCallText.Position = new Vector2(1150, 235);
            helpCallText.align = Alignment.Right;
            Add(helpCallText);

            SpriteGameObject audienceCallSprite = new SpriteGameObject("audienceCallSprite", "Images/publiek");
            audienceCallSprite.Position = new Vector2(1175, 400);
            audienceCallSprite.width = audienceCallSprite.height = 100;
            Add(audienceCallSprite);

            TextGameObject audienceCallText = new TextGameObject("audienceCallText", "Vraag de jeugd");
            audienceCallText.Position = new Vector2(1150, 435);
            audienceCallText.align = Alignment.Right;
            Add(audienceCallText);

            SpriteGameObject fiftyCallSprite = new SpriteGameObject("fiftyCallSprite", "Images/5050");
            fiftyCallSprite.Position = new Vector2(1175, 600);
            fiftyCallSprite.width = fiftyCallSprite.height = 100;
            Add(fiftyCallSprite);

            TextGameObject fiftyCallText = new TextGameObject("fiftyCallText", "2 foute antwoorden weggestreept");
            fiftyCallText.Position = new Vector2(1150, 635);
            fiftyCallText.align = Alignment.Right;
            Add(fiftyCallText);

            TextGameObject helpText = new TextGameObject("", "HULPLIJNEN");
            helpText.SetSpriteFont("Fonts/LargeSpriteFont");
            helpText.Position = new Vector2(750, 75);
            helpText.scale = 0.5f;
            Add(helpText);
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            if (inputHelper.IsKeyPressed(Keys.Space) && !activatedThisTick)
                game.SwitchRoot("questionstate");

            indicator.score = score;

            base.Update(gameTime, inputHelper);
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public string GetScoreText()
        {
            if (score == 0)
                return zeroScore;

            return scores[score-1];
        }

        public bool AllCorrect()
        {
            return score == 15;
        }
    }
}
