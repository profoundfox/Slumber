
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstructEngine.Util;
using ConstructEngine.Gum;
using MonoGameGum;
using Slumber.Screens;
using System;
using ConstructEngine;


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
        

    }

    public void Load()
    {

        
    }

    public void Update(GameTime gameTime)
    {
        

        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        
    }

}
