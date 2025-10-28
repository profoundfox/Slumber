
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstructEngine.Util;
using ConstructEngine.Gum;
using MonoGameGum;
using Slumber.Screens;
using System;
using ConstructEngine;
using ConstructEngine.Graphics;


namespace Slumber;


public class MainMenu : Scene, Scene.IScene
{
    
    
    public MainMenu()
    {
        
        
    }

    public void Initialize()
    {
        GumHelper.Wipe();
        GumHelper.AddScreenToRoot(new TitleScreen());
        

        ParallaxBackground.AddBackground(new("Assets/Backgrounds/streetsbg", 0.1f,  ParallaxBackground.RepeatX, new Vector2(0,0)));

    }

    public void Load()
    {

        
    }

    public void Update(GameTime gameTime)
    {
        
        
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        ParallaxBackground.DrawParallaxBackgrounds(spriteBatch, Core.GraphicsDevice, SamplerState.LinearWrap);
    }

}
