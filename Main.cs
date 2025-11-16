using Microsoft.Xna.Framework;
using ConstructEngine;
using ConstructEngine.Util;

namespace Slumber
{
    public class Main : Engine 
    {
        public Main() : base(new EngineConfig
        {
            Title = "Slumber",
            VirtualWidth = 640,
            VirtualHeight = 360,
            Fullscreen = false,
            IntegerScaling = true,
            AllowUserResizing = true,
            IsBorderless = true,
            IsFixedTimeStep = false,
            SynchronizeWithVerticalRetrace = true,
            FontPath = "Assets/Fonts/Font",
            GumProject = "GumProject/GumProject.gumx",

        }) {  }

        protected override void Initialize()
        {
            base.Initialize();
            SceneManager.AddScene(new MainMenu());
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
