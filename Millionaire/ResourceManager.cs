using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Millionaire
{
    public class ResourceManager // Keeps track of all loaded content. Requested content is loaded if this is not yet done.
    {
        protected ContentManager contentManager;
        protected Dictionary<string, Texture2D> sprites;
        protected Dictionary<string, SpriteFont> fonts;
        protected Dictionary<string, SoundEffect> sounds;
        protected Dictionary<string, Song> music;
        protected Dictionary<string, Effect> effects;
        protected Dictionary<string, Video> videos;

        public ResourceManager(ContentManager Content)
        {
            this.contentManager = Content;
            this.sprites = new Dictionary<string, Texture2D>();
            this.fonts = new Dictionary<string, SpriteFont>();
            this.sounds = new Dictionary<string, SoundEffect>();
            this.music = new Dictionary<string, Song>();
            this.effects = new Dictionary<string, Effect>();
            this.videos = new Dictionary<string, Video>();
        }

        public VideoPlayer PlayVideo(string assetName)
        {
            if (!this.videos.ContainsKey(assetName))
                this.videos[assetName] = contentManager.Load<Video>(assetName);
            VideoPlayer temp = new VideoPlayer();
            temp.Play(this.videos[assetName]);
            return temp;
        }

        public Effect GetEffect(string assetName)
        {
            if (assetName == "" || assetName == null)
                return null;
            if (!this.effects.ContainsKey(assetName))
                this.effects[assetName] = contentManager.Load<Effect>(assetName);
            return this.effects[assetName];
        }

        public Texture2D GetSprite(string assetName)
        {
            if (assetName == "")
                return null;
            if (!this.sprites.ContainsKey(assetName))
                this.sprites[assetName] = contentManager.Load<Texture2D>(assetName);
            return this.sprites[assetName];
        }

        public SpriteFont GetFont(string assetName)
        {
            if (assetName == "")
                return null;
            if (!this.fonts.ContainsKey(assetName))
                this.fonts[assetName] = contentManager.Load<SpriteFont>(assetName);
            return this.fonts[assetName];
        }

        public void PlaySound(string assetName, float volume = 1.0f, float pitch = 0.0f, float balance = 0.0f)
        {
            if (!this.sounds.ContainsKey(assetName))
                this.sounds[assetName] = contentManager.Load<SoundEffect>(assetName);
            this.sounds[assetName].Play(volume, pitch, balance);
        }

        public SoundEffectInstance GetSoundInstance(string assetName)
        {
            if (!this.sounds.ContainsKey(assetName))
                this.sounds[assetName] = contentManager.Load<SoundEffect>(assetName);
            return this.sounds[assetName].CreateInstance();
        }

        public void PlayMusic(string assetName, bool repeat = false)
        {
            if (!this.music.ContainsKey(assetName))
                this.music[assetName] = contentManager.Load<Song>(assetName);
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Play(this.music[assetName]);
        }
        
        public void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public bool IsMusicPlaying => MediaPlayer.State == MediaState.Playing;

        public void SetMusicVolume(float volume)
        {
            MediaPlayer.Volume = volume / 100;
        }

        public void SetSoundVolume(float volume)
        {
            SoundEffect.MasterVolume = volume / 100;
        }

        public ContentManager Content
        {
            get { return contentManager; }
        }
    }
}