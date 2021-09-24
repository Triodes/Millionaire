using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Millionaire.Objects;
using Millionaire.States;
using System;
using System.Collections.Generic;

namespace Millionaire
{
    class Millionaire : Game
    {
        private GraphicsDeviceManager graphicsDevice;
        private SpriteBatch spriteBatch;
        private InputHelper inputHelper;
        private static ResourceManager resourceManager;

        private Dictionary<String, ObjectRoot> objectRoots;
        private ObjectRoot oldRoot;
        private ObjectRoot activeRoot;

        private IntroState introState;
        public ScoreState scoreState;
        public QuestionState questionState;
        public EndGameState endGameState;

        private static Matrix scaleMatrix;
        private static Point resolution = new Point(1366, 768);

        public Millionaire()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SetFullScreen(false);
            
            inputHelper = new InputHelper();
            resourceManager = new ResourceManager(Content);

            objectRoots = new Dictionary<string, ObjectRoot>();

            SpriteGameObject background = new SpriteGameObject("background", "Images/background");
            background.width = resolution.X;
            background.height = resolution.Y;

            introState = new IntroState(this);
            introState.AddFirst(background);
            objectRoots.Add(introState.Id, introState);

            scoreState = new ScoreState(this);
            scoreState.AddFirst(background);
            objectRoots.Add(scoreState.Id, scoreState);

            questionState = new QuestionState(this);
            questionState.AddFirst(background);
            objectRoots.Add(questionState.Id, questionState);

            endGameState = new EndGameState(this);
            endGameState.AddFirst(background);
            objectRoots.Add(endGameState.Id, endGameState);

            base.Initialize();
        }

        public void SwitchRoot(String id)
        {
            ObjectRoot newRoot = objectRoots.GetValueOrDefault(id);
            if (newRoot == null) return;

            if (activeRoot != null)
                activeRoot.Deactivate();
            oldRoot = activeRoot;
            activeRoot = newRoot;
            activeRoot.Activate();
            rootSwitched = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        bool rootSwitched = false;

        protected override void Update(GameTime gameTime)
        {
            rootSwitched = false;
            inputHelper.Update();

            if (inputHelper.IsKeyPressed(Keys.F))
                this.SetFullScreen(!graphicsDevice.IsFullScreen);

            if (inputHelper.IsKeyDown(Keys.Escape) && inputHelper.IsKeyDown(Keys.LeftShift))
                Exit();

            if (activeRoot == null && inputHelper.IsKeyPressed(Keys.Space))
                SwitchRoot("intro");

            if (activeRoot != null)
                activeRoot.Update(gameTime, inputHelper);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, ScaleMatrix);

            if (rootSwitched && oldRoot != null)
                oldRoot.Draw(spriteBatch);
            else if (activeRoot != null)
                activeRoot.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool SetFullScreen(bool fullscreen = true)
        {
            float scalex = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / (float)resolution.X;
            float scaley = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / (float)resolution.Y;
            float finalscale = 1f;

            if (!fullscreen)
            {
                if (scalex < 1f || scaley < 1f)
                    finalscale = Math.Min(scalex, scaley);
            }
            else
            {
                finalscale = scalex;
                if (Math.Abs(1 - scaley) < Math.Abs(1 - scalex))
                    finalscale = scaley;
            }

            graphicsDevice.PreferredBackBufferWidth = (int)(finalscale * resolution.X);
            graphicsDevice.PreferredBackBufferHeight = (int)(finalscale * resolution.Y);
            graphicsDevice.IsFullScreen = fullscreen;
            graphicsDevice.ApplyChanges();
            scaleMatrix = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / resolution.X, (float)GraphicsDevice.Viewport.Height / resolution.Y, 1);
            return graphicsDevice.IsFullScreen;
        }

        public static Matrix ScaleMatrix => scaleMatrix;

        public static ResourceManager ResourceManager => resourceManager;

        public static Point Resolution => resolution;
    }
}
