
using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using Gum.Wireframe;
using RenderingLibrary.Graphics;

namespace Slumber
{
  public static class GumT
  {
    public static SpriteRuntime FromRegion(this SpriteRuntime spriteRuntime, TextureRegion region)
    {
      spriteRuntime.TextureAddress = Gum.Managers.TextureAddress.Custom;
      
      spriteRuntime.Texture = region.Source;
      spriteRuntime.SourceRectangle = region.SourceRectangle;

      return spriteRuntime;
    }

    public static void Enable(this Panel panel)
    {
      panel.IsEnabled = true;
      foreach (var child in panel.Children)
      {
        child.IsEnabled = true;
      }
    }

    public static void Disable(this Panel panel)
    {
      panel.IsEnabled = false;
      foreach (var child in panel.Children)
      {

        //child.IsFocused = false;
        child.IsEnabled = false;
      }
    }
  
    public static void Enable(this ContainerRuntime container)
    {
      container.IsEnabled = true;
      foreach (var child in container.Children)
      {
        if (child is InteractiveGue interactiveGue)
          if (interactiveGue.FormsControlAsObject is FrameworkElement fe)
            fe.IsEnabled = true;
      }
    }

    public static void Disable(this ContainerRuntime container)
    {
      container.IsEnabled = false;
      foreach (var child in container.Children)
      {
        if (child is InteractiveGue interactiveGue)
          if (interactiveGue.FormsControlAsObject is FrameworkElement fe)
            fe.IsEnabled = false;
      }
    }
  }
}
