using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{
    class GameObject
    {
        private readonly string id;

        protected Vector2 position = Vector2.Zero;
        protected bool visible = true;

        public GameObject(string id)
        {
            this.id = id;
        }

        public virtual void Update(GameTime gameTime, InputHelper inputHelper)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public string Id => id;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public bool Visible // Only use this for visibility. Not for object existance etc.
        {
            get { return visible; }
            set { visible = value; }
        }
    }
}
