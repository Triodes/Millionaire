using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Millionaire.Objects
{
    class ObjectRoot : GameObjectList
    {
        protected Millionaire game;
        protected bool activatedThisTick = false;

        public ObjectRoot(Millionaire game, string id) : base(id)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            activatedThisTick = false;
            base.Update(gameTime, inputHelper);
        }

        public virtual void Activate()
        {
            activatedThisTick = true;
        }

        public virtual void Deactivate()
        {

        }
    }
}
