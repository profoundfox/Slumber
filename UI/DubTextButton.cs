

using Gum.Converters;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using RenderingLibrary.Graphics;

namespace Slumber;

public class DubTextButton : Button 
{
  
  public Label LeftLabel { get; set; }
  public Label RightLabel { get; set; }

  public bool BorderEnabled { get; set; } = true;

  public SpriteRuntime FocusThingy { get; set; }

  public bool NormalDisabledFocus { get; set; } = true;

  public DubTextButton(string leftText, string rightText)
  {
    var buttonVisual = (Gum.Forms.DefaultVisuals.V3.ButtonVisual)Visual;

    float offset = 50f;
    
    NineSliceRuntime background = buttonVisual.Background;

    buttonVisual.Background.Texture = Core.Resources.Pixel.Source;

    this.Text = string.Empty; 

    this.WidthUnits = DimensionUnitType.Absolute;
    this.Width = 400; 
    
    this.HeightUnits = DimensionUnitType.RelativeToChildren;
    this.Height = 12;

    LeftLabel = new Label();
    LeftLabel.WidthUnits = DimensionUnitType.RelativeToChildren;
    LeftLabel.Width = 0;
    LeftLabel.HeightUnits = DimensionUnitType.RelativeToChildren;
    LeftLabel.Height = 0;
    LeftLabel.X = 15;
    LeftLabel.Y = 0;
    LeftLabel.YUnits = GeneralUnitType.PixelsFromMiddle;
    LeftLabel.YOrigin = VerticalAlignment.Center;
    LeftLabel.Text = leftText;
    AddChild(LeftLabel);

    var textLeftVisual = (Gum.Forms.DefaultVisuals.V3.LabelVisual)LeftLabel.Visual;
    textLeftVisual.UseCustomFont = true;
    textLeftVisual.CustomFontFile = "Fonts/RainHearts.fnt";
    textLeftVisual.FontScale = 2.0f;
    textLeftVisual.Color = Color.White;

    RightLabel = new Label();
    RightLabel.WidthUnits = DimensionUnitType.RelativeToChildren;
    RightLabel.Width = 0;
    RightLabel.HeightUnits = DimensionUnitType.RelativeToChildren;
    RightLabel.Height = 0;
    RightLabel.XUnits = GeneralUnitType.PixelsFromLarge;
    RightLabel.X = -15;
    RightLabel.XOrigin = HorizontalAlignment.Right;
    RightLabel.Y = 0;
    RightLabel.YUnits = GeneralUnitType.PixelsFromMiddle;
    RightLabel.YOrigin = VerticalAlignment.Center;
    RightLabel.Text = rightText;
    AddChild(RightLabel);

    var textRightVisual = (Gum.Forms.DefaultVisuals.V3.LabelVisual)RightLabel.Visual;

    textRightVisual.UseCustomFont = true;
    textRightVisual.CustomFontFile = "Fonts/RainHearts.fnt";
    textRightVisual.FontScale = 2.0f;
    textRightVisual.Color = Color.White;

    background.Color = Color.White * 0f;

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
      textLeftVisual.Color = Color.White;
      textLeftVisual.X = 0;
      FocusThingy.Visible = false;
    };

    StateSave pushedState = buttonVisual.States.Pushed;
    pushedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      textLeftVisual.Color = ColorExtension.Hex("#b8c3d9");
      textLeftVisual.X = offset;
      FocusThingy.Visible = true;
    };

    StateSave FocusedState = buttonVisual.States.Focused;
    FocusedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      textLeftVisual.Color = ColorExtension.Hex("#b8c3d9");
      IsFocused = true;
      textLeftVisual.X = offset;
      FocusThingy.Visible = true;
      FocusThingy.Color = Color.White;
    };

    StateSave disabledFoucsState = buttonVisual.States.DisabledFocused;
    disabledFoucsState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      textLeftVisual.Color = ColorExtension.Hex("#808796");
      IsFocused = true;
      textLeftVisual.X = offset;
      FocusThingy.Visible = true;
      FocusThingy.Color = ColorExtension.Hex("#808796");

    };
    
    StateSave disabledState = buttonVisual.States.Disabled;
    disabledState.Apply = () =>
    {
      if (NormalDisabledFocus)
      {
        background.Color = Color.White * 0f;
        textLeftVisual.Color = ColorExtension.Hex("#969696");
        textLeftVisual.X = 0;
        FocusThingy.Visible = false;
      }
      else 
      {
        DisabledFocused();
      }
    };

    StateSave highlightedFocused = buttonVisual.States.HighlightedFocused;
    highlightedFocused.Apply = FocusedState.Apply;
    highlightedFocused.Apply += () =>
    {
      IsFocused = true;
    };

    StateSave highlighted = buttonVisual.States.Highlighted;
    highlighted.Apply = FocusedState.Apply;
    highlighted.Apply += () =>
    {
      IsFocused = true;
    };
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
