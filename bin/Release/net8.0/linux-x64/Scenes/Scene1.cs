
using System;
using ConstructEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ConstructEngine.Graphics;
using ConstructEngine.Util;
using System.Linq;
using Construct.Graphics;
using Slumber.Entities;
using ConstructEngine.Components.Entity;
using ConstructEngine.Gum;
using ConstructEngine.Object;
using ConstructEngine.Util.Tween;
using Graphics;
using Gum.Forms.Controls;
using Slumber.Screens;


namespace Slumber;


public class Scene1 : Scene, Scene.IScene
{
    
    
    
    

    public RoomCamera _camera { get; set; }
    private SpriteFont _font;
    private ContentManager contentManager;

    
    public float Value { get; set; } = 1;

    private Vector2 LoadPosition;

    private Rectangle Rectangle;
    
    
    public Scene1()
    {
        
        this.contentManager = Core.Content;
        
    }

    public void Initialize()
    {
        
        GumHelper.RemoveScreenOfType<TitleScreen>();
        //Main.CurrentScreen.RemoveFromRoot();
    }
    public void Load()
    {
        InstantiateEntities(contentManager, "Content/Data/Scene1.json");
        
        //playerCharacter.Load();
        
        _font = Core.Content.Load<SpriteFont>("Assets/Fonts/Font");
            
        TilemapFromOgmo.FromFile(contentManager, "Content/Data/Scene1.json", "0 0 512 512", "Assets/Tileset/SlumberTilesetAtlas");
        TilemapFromOgmo.GetCollisions(contentManager, "Content/Data/Scene1.json");
        TilemapFromOgmo.SearchForObjects("Content/Data/Scene1.json", Entity.EntityList.OfType<Player>().FirstOrDefault(), Core.SceneManager);

        Rectangle = TilemapFromOgmo.GetMapWidth("Content/Data/Scene1.json");
        
        
        _camera = new RoomCamera(Entity.EntityList.OfType<Player>().FirstOrDefault().KinematicBase.Hitbox); 
        
        ParallaxBackground.BackgroundList.Add(new("Assets/Backgrounds/Main", contentManager, 0.1f,  ParallaxBackground.RepeatYX, _camera));
        ParallaxBackground.BackgroundList.Add(new("Assets/Backgrounds/Clouds5", contentManager, 0.2f, ParallaxBackground.RepeatX, _camera));
        ParallaxBackground.BackgroundList.Add(new("Assets/Backgrounds/Clouds4", contentManager, 0.3f, ParallaxBackground.RepeatX, _camera));
        ParallaxBackground.BackgroundList.Add(new("Assets/Backgrounds/Clouds3", contentManager, 0.4f, ParallaxBackground.RepeatX, _camera));
        ParallaxBackground.BackgroundList.Add(new("Assets/Backgrounds/Clouds2", contentManager, 0.5f, ParallaxBackground.RepeatX, _camera));
        ParallaxBackground.BackgroundList.Add(new("Assets/Backgrounds/Clouds1", contentManager, 0.6f, ParallaxBackground.RepeatX, _camera));

        
        
    }





    public void Update(GameTime gameTime)
    {

        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
        {
            Core.Exit();
        }

        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            Core.SceneManager.ReloadCurrentScene();
        }

        TilemapFromOgmo.UpdateObjects(gameTime);

        UpdateEntities(gameTime);


        //_sceneAreaTransition.Update(Entity.EntityList.OfType<Player>().FirstOrDefault().KinematicBase.Hitbox );

        _camera.Follow(Entity.EntityList.OfType<Player>().FirstOrDefault().KinematicBase.Hitbox, Entity.EntityList.OfType<Player>().FirstOrDefault());
        
        
    }



    public void Draw(SpriteBatch spriteBatch)
    {
        
        ParallaxBackground.DrawParallaxBackgrounds(spriteBatch, Core.GraphicsDevice, SamplerState.LinearWrap);
        
        spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            samplerState: SamplerState.PointClamp,   
            transformMatrix: _camera.Transform
        );
        
        
        Tilemap.DrawTilemaps(spriteBatch);
        
        DrawEntities(spriteBatch);
        
        spriteBatch.End();
        
    }

}
