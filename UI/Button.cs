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

  public SpriteRuntime FocusThingy { get; set; }

  public bool NormalDisabledFocus { get; set; } = true;

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


    FocusThingy = new SpriteRuntime();
    AddChild(FocusThingy);

    FocusThingy.FromRegion(new TextureRegion(Core.Resource.Load<Texture2D>("Maps/Atlas"), new Rectangle(416, 592, 8, 8)));
    FocusThingy.WidthUnits = DimensionUnitType.Absolute;
    FocusThingy.Width = FocusThingy.SourceRectangle.Width * 4; 
    FocusThingy.HeightUnits = DimensionUnitType.Absolute;
    FocusThingy.Height = FocusThingy.SourceRectangle.Height * 4;

    FocusThingy.X = 10;
    FocusThingy.Y = 20;

    FocusThingy.Visible = false;

    StateSave enabledState = buttonVisual.States.Enabled;
    enabledState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = Color.White;
      text.X = 0;
      FocusThingy.Visible = false;
    };

    StateSave pushedState = buttonVisual.States.Pushed;
    pushedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = ColorExtension.Hex("#b8c3d9");
      text.X = offset;
      FocusThingy.Visible = true;
    };

    StateSave FocusedState = buttonVisual.States.Focused;
    FocusedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = ColorExtension.Hex("#b8c3d9");
      IsFocused = true;
      text.X = offset;
      FocusThingy.Visible = true;
      FocusThingy.Color = Color.White;
    };

    StateSave disabledFoucsState = buttonVisual.States.DisabledFocused;
    disabledFoucsState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      text.Color = ColorExtension.Hex("#808796");
      IsFocused = true;
      text.X = offset;
      FocusThingy.Visible = true;
      FocusThingy.Color = ColorExtension.Hex("#808796");

    };
    
    StateSave disabledState = buttonVisual.States.Disabled;
    disabledState.Apply = () =>
    {
      if (NormalDisabledFocus)
      {
        background.Color = Color.White * 0f;
        text.Color = ColorExtension.Hex("#969696");
        text.X = 0;
        FocusThingy.Visible = false;
      }
      else 
      {
        DisabledFocused();
      }
    };


    StateSave highlightedFocused = buttonVisual.States.HighlightedFocused;
    highlightedFocused.Apply = FocusedState.Apply;

    StateSave highlighted = buttonVisual.States.Highlighted;
    highlighted.Apply = FocusedState.Apply;

  }

  public void DisabledFocused()
  {
    var buttonVisual = (Gum.Forms.DefaultVisuals.V3.ButtonVisual)Visual;

    buttonVisual.Background.Color = Color.White * 0f;
    buttonVisual.TextInstance.Color = ColorExtension.Hex("#808796");
    IsFocused = true;
    buttonVisual.TextInstance.X = 20f;
    FocusThingy.Visible = true;
    FocusThingy.Color = ColorExtension.Hex("#808796");
  }

  public NineSliceRuntime CreateBorderLine()
  {
    var line = new NineSliceRuntime();
    line.Texture = Core.Resources.Pixel.Source;
    line.Color = Color.White;
    return line;
  }
}
