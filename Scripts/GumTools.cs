
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
  }
}
