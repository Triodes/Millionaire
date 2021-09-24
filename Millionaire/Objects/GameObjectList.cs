using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Millionaire.Objects
{
    class GameObjectList : GameObject
    {
        private List<GameObject> gameObjects;
        private List<GameObject> toRemove;
        public GameObjectList(string id)
            : base (id)
        {
            gameObjects = new List<GameObject>();
            toRemove = new List<GameObject>();
        }

        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public void AddFirst(GameObject gameObject)
        {
            gameObjects.Insert(0, gameObject);
        }

        public void Remove(GameObject gameObject)
        {
            if (!updating)
                gameObjects.Remove(gameObject);
            else
                toRemove.Add(gameObject);

        }

        public GameObject Find(string id)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Id == id)
                    return gameObject;

                if (gameObject is GameObjectList)
                {
                    GameObject foundObject = ((GameObjectList)gameObject).Find(id);
                    if (foundObject != null)
                    {
                        return foundObject;
                    }
                }
            }

            return null;
        }

        bool updating = false;

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            updating = true;
            gameObjects.ForEach(delegate (GameObject gameObject)
            {
                gameObject.Update(gameTime, inputHelper);
            });
            updating = false;

            toRemove.ForEach(delegate (GameObject gameObject)
            {
                gameObjects.Remove(gameObject);
            });
            toRemove.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                gameObjects.ForEach(delegate (GameObject gameObject)
                {
                    gameObject.Draw(spriteBatch);
                });
            }
        }
    }
}
