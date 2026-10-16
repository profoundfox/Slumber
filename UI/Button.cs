using Gum.Converters;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using Gum.Wireframe;
using RenderingLibrary.Graphics;

namespace Slumber;

public class CustomButton : Button
{
  public bool BorderEnabled { get; set; } = true;

  public CustomButton()
  {
    var buttonVisual = (Gum.Forms.DefaultVisuals.V3.ButtonVisual)Visual;

    float offset = 20f;
    
    NineSliceRuntime background = buttonVisual.Background;

    buttonVisual.Background.Texture = Core.Resources.Pixel.Source;

    TextRuntime text = buttonVisual.TextInstance;

    text.WidthUnits = DimensionUnitType.RelativeToChildren;
    text.Width = 0;

    buttonVisual.WidthUnits = DimensionUnitType.RelativeToChildren;
    buttonVisual.Width = 10;

    //XOrigin = HorizontalAlignment.Center;

    text.UseCustomFont = true;
    text.CustomFontFile = "Fonts/RainHearts.fnt";
    text.FontScale = 2.0f;
    
    background.Color = Color.White * 0f;
    text.Color = Color.White;


    var focusThingy = new SpriteRuntime();
    AddChild(focusThingy);

    focusThingy.FromRegion(new TextureRegion(Core.Resource.Load<Texture2D>("Maps/Atlas"), new Rectangle(416, 592, 8, 8)));
    focusThingy.WidthUnits = DimensionUnitType.Absolute;
    focusThingy.Width = focusThingy.SourceRectangle.Width * 4; 
    focusThingy.HeightUnits = DimensionUnitType.Absolute;
    focusThingy.Height = focusThingy.SourceRectangle.Height * 4;

    focusThingy.X = 10;
    focusThingy.Y = 20;

    focusThingy.Visible = false;

    StateSave enabledState = buttonVisual.States.Enabled;
    enabledState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = Color.White;
      text.X = 0;
      focusThingy.Visible = false;
    };

    StateSave pushedState = buttonVisual.States.Pushed;
    pushedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = ColorExtension.Hex("#b8c3d9");
      text.X = offset;
      focusThingy.Visible = true;
    };

    StateSave focusedState = buttonVisual.States.Focused;
    focusedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = ColorExtension.Hex("#b8c3d9");
      IsFocused = true;
      text.X = offset;
      focusThingy.Visible = true;
    };
    
    StateSave disabledState = buttonVisual.States.Disabled;
    disabledState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = Color.Gray * 0.42f;
      text.X = 0;
      focusThingy.Visible = false;
    };

    StateSave highlightedFocused = buttonVisual.States.HighlightedFocused;
    highlightedFocused.Apply = focusedState.Apply;

    StateSave highlighted = buttonVisual.States.Highlighted;
    highlighted.Apply = focusedState.Apply;
  }

  public NineSliceRuntime CreateBorderLine()
  {
    var line = new NineSliceRuntime();
    line.Texture = Core.Resources.Pixel.Source;
    line.Color = Color.White;
    return line;
  }
}
