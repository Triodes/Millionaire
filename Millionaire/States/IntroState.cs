using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Millionaire.Objects;

namespace Millionaire.States
{
    class IntroState : ObjectRoot
    {
        public IntroState(Millionaire game) : base(game, "intro")
        {
            SpriteGameObject logo = new SpriteGameObject("logo", "Images/logo");
            logo.Position = new Vector2(Millionaire.Resolution.X / 2f - logo.width / 2f, Millionaire.Resolution.Y / 2f - logo.height / 2f);
            Add(logo);
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            if (!activatedThisTick && (!Millionaire.ResourceManager.IsMusicPlaying || inputHelper.IsKeyPressed(Keys.Space)))
            {
                game.SwitchRoot("score");
            }

            base.Update(gameTime, inputHelper);
        }

        public override void Activate()
        {
            Millionaire.ResourceManager.PlayMusic("Sound/main theme");

            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
