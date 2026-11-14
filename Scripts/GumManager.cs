using System.Collections.Generic;
using RenderingLibrary;
using RenderingLibrary.Graphics;

namespace Slumber;

public class GumManager 
{
  public GumBatch GumBatch { get; set; }

  public List<IRenderableIpso> ToSubmit { get; set; } = new();

  public void Initialize()
  {
    SystemManagers.Default = new SystemManagers();
    SystemManagers.Default.Initialize(Core.Instance.GraphicsDevice);
    GumBatch = new GumBatch();
    
    Core.Batch.OnDraw += Draw;
  }

  public void Update(GameTime gameTime)
  {
    SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
  }

  public void Submit(IRenderableIpso renderable)
  {
    ToSubmit.Add(renderable);
  }

  public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, BaseRenderer2D renderer)
  {
    GumBatch.Begin();
  
    for (int i = 0; i < ToSubmit.Count; i++)
      GumBatch.Draw(ToSubmit[i]);
    ToSubmit.Clear();

    GumBatch.End();
  }
}
