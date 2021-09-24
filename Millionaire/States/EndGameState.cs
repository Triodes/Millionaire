using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Millionaire.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Millionaire.States
{
    class EndGameState: ObjectRoot
    {
        TextGameObject finalScoreText;

        public EndGameState(Millionaire game) : base(game, "endgame")
        {
            SpriteGameObject logo = new SpriteGameObject("logo", "Images/logo");
            logo.width = (int)(0.6f * logo.width);
            logo.height = (int)(0.6f * logo.height);
            logo.Position = new Vector2(Millionaire.Resolution.X / 2f - logo.width / 2f, 0);
            Add(logo);
        }

        public override void Activate()
        {
            float baseX = Millionaire.Resolution.X * 0.125f;
            float windowWidth = Millionaire.Resolution.X * 0.75f;

            SpriteGameObject scoreTextBg = new SpriteGameObject("scorebg", "Images/whitepixel");
            scoreTextBg.color = Color.Black;
            scoreTextBg.width = (int)windowWidth;
            scoreTextBg.height = 200;
            scoreTextBg.Position = new Vector2(baseX, 300);
            Add(scoreTextBg);

            finalScoreText = new TextGameObject("finalScore", game.scoreState.GetScoreText());
            finalScoreText.SetSpriteFont("Fonts/LargeSpriteFont");
            finalScoreText.Position = new Vector2(0.5f * Millionaire.Resolution.X, 340);
            finalScoreText.align = Alignment.Center;
            Add(finalScoreText);

            Millionaire.ResourceManager.PlayMusic("Sound/main theme");
        }
    }
}
