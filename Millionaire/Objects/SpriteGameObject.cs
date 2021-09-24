using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{
    class SpriteGameObject : GameObject
    {
        private Texture2D sprite;

        public int width;
        public int height;

        public Color color = Color.White;
        public SpriteGameObject(string id, string assetName)
            : base(id)
        {
            visible = true;
            sprite = Millionaire.ResourceManager.GetSprite(assetName);
            width = sprite.Width;
            height = sprite.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
                spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, width, height), color);
        }
    }
}
