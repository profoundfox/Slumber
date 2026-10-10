
using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.GueDeriving;
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

    public static void Enable(this StackPanel panel)
    {
      panel.IsEnabled = true;
      foreach (var child in panel.Children)
      {
        child.IsEnabled = true;
      }
    }

    public static void Disable(this StackPanel panel)
    {
      panel.IsEnabled = false;
      foreach (var child in panel.Children)
      {

        child.IsFocused = false;
        child.IsEnabled = false;
      }

    }
  }
}
