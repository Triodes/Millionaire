using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{
    class ScoreIndicator : SpriteGameObject
    {
        public int score = 0;

        public ScoreIndicator(string id) : base(id, "Images/score_indicator")
        {

        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            this.position = new Vector2(120, Millionaire.Resolution.Y - 136 - score * 38);
            base.Update(gameTime, inputHelper);
        }
    }
}
