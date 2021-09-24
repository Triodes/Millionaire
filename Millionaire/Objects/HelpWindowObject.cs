using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{
    class HelpWindowObject : GameObjectList
    {
        bool usedCall = false;
        bool usedAudience = false;
        bool used5050 = false;
        public bool active = false;
        private bool oneTickActive = false;

        SpriteGameObject helpCallSprite;
        SpriteGameObject audienceCallSprite;
        SpriteGameObject fiftyCallSprite;

        TextGameObject helpCallText;
        TextGameObject audienceCallText;
        TextGameObject fiftyCallText;
        private readonly Millionaire game;

        public HelpWindowObject(Millionaire game) : base("helpwindow")
        {
            this.game = game;

            float baseX = Millionaire.Resolution.X * 0.125f;
            float baseY = Millionaire.Resolution.Y * 0.125f;
            float windowWidth = Millionaire.Resolution.X * 0.75f;
            float windowHeight = Millionaire.Resolution.Y * 0.75f;

            SpriteGameObject helpWindowBg = new SpriteGameObject("scorebg", "Images/whitepixel");
            helpWindowBg.color = Color.Black;
            helpWindowBg.Position = new Vector2(baseX, baseY);
            
            helpWindowBg.width = (int)windowWidth;
            helpWindowBg.height = (int)windowHeight;
            Add(helpWindowBg);

            helpCallSprite = new SpriteGameObject("helpCallSprite", "Images/bellen");
            helpCallSprite.width = helpCallSprite.height = 100;
            helpCallSprite.Position = new Vector2(baseX + 0.25f * windowWidth - 0.5f * helpCallSprite.width, baseY + 0.35f * windowHeight);
            Add(helpCallSprite);

            helpCallText = new TextGameObject("", "1");
            helpCallText.Position = new Vector2(baseX + 0.25f * windowWidth - 7, baseY+0.55f*windowHeight);
            Add(helpCallText);

            audienceCallSprite = new SpriteGameObject("audienceCallSprite", "Images/publiek");
            audienceCallSprite.width = audienceCallSprite.height = 100;
            audienceCallSprite.Position = new Vector2(baseX + 0.5f * windowWidth - 0.5f * audienceCallSprite.width, baseY + 0.35f * windowHeight);
            Add(audienceCallSprite);

            audienceCallText = new TextGameObject("", "2");
            audienceCallText.Position = new Vector2(baseX + 0.5f * windowWidth - 6, baseY + 0.55f * windowHeight);
            Add(audienceCallText);

            fiftyCallSprite = new SpriteGameObject("fiftyCallSprite", "Images/5050");
            fiftyCallSprite.width = fiftyCallSprite.height = 100;
            fiftyCallSprite.Position = new Vector2(baseX + 0.75f * windowWidth - 0.5f * fiftyCallSprite.width, baseY + 0.35f * windowHeight);
            Add(fiftyCallSprite);

            fiftyCallText = new TextGameObject("", "3");
            fiftyCallText.Position = new Vector2(baseX + 0.75f * windowWidth - 6, baseY + 0.55f * windowHeight);
            Add(fiftyCallText);
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            if (active)
            {
                if (inputHelper.IsKeyPressed(Keys.D1) && !usedCall) 
                {
                    usedCall = true;
                    helpCallSprite.color = Color.Red;
                    helpCallText.color = Color.Red;
                    ((SpriteGameObject)game.scoreState.Find("helpCallSprite")).color = Color.Red;
                    ((TextGameObject)game.scoreState.Find("helpCallText")).color = Color.Red;
                }

                if (inputHelper.IsKeyPressed(Keys.D2) && !usedAudience)
                {
                    usedAudience = true;
                    audienceCallSprite.color = Color.Red;
                    audienceCallText.color = Color.Red;
                    ((SpriteGameObject)game.scoreState.Find("audienceCallSprite")).color = Color.Red;
                    ((TextGameObject)game.scoreState.Find("audienceCallText")).color = Color.Red;
                }

                if (inputHelper.IsKeyPressed(Keys.D3) && !used5050)
                {
                    used5050 = true;
                    fiftyCallSprite.color = Color.Red;
                    fiftyCallText.color = Color.Red;
                    ((SpriteGameObject)game.scoreState.Find("fiftyCallSprite")).color = Color.Red;
                    ((TextGameObject)game.scoreState.Find("fiftyCallText")).color = Color.Red;
                    game.questionState.DoFiftyFifty();
                    active = false;
                    oneTickActive = false;
                }

                if (inputHelper.IsKeyPressed(Keys.H) && oneTickActive)
                {
                    active = false;
                    oneTickActive = false;
                }

                base.Update(gameTime, inputHelper);
            }

            oneTickActive = active;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active) base.Draw(spriteBatch);
        }
    }
}
