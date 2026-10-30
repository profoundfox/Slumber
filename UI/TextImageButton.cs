
using Gum.Converters;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using RenderingLibrary.Graphics;

namespace Slumber;

public class TextAndSpriteButton : Button 
{
  public string LeftText { get; set; }
  public TextureRegion RightTexture { get; set; }

  public bool BorderEnabled { get; set; } = true;

  public SpriteRuntime FocusThingy { get; set; }

  public bool NormalDisabledFocus { get; set; } = true;

  public TextAndSpriteButton(string leftText, TextureRegion rightTexture)
  {
    var buttonVisual = (Gum.Forms.DefaultVisuals.V3.ButtonVisual)Visual;

    float offset = 50f;
    
    NineSliceRuntime background = buttonVisual.Background;

    buttonVisual.Background.Texture = Core.Resources.Pixel.Source;

    LeftText = leftText;
    RightTexture = rightTexture;

    this.Text = string.Empty; 

    this.WidthUnits = DimensionUnitType.Absolute;
    this.Width = 400; 
    
    this.HeightUnits = DimensionUnitType.RelativeToChildren;
    this.Height = 12;

    var leftLabel = new Label();
    leftLabel.WidthUnits = DimensionUnitType.RelativeToChildren;
    leftLabel.Width = 0;
    leftLabel.HeightUnits = DimensionUnitType.RelativeToChildren;
    leftLabel.Height = 0;
    leftLabel.X = 15;
    leftLabel.Y = 0;
    leftLabel.YUnits = GeneralUnitType.PixelsFromMiddle;
    leftLabel.YOrigin = VerticalAlignment.Center;
    leftLabel.Text = LeftText;
    AddChild(leftLabel);

    var textLeftVisual = (Gum.Forms.DefaultVisuals.V3.LabelVisual)leftLabel.Visual;
    textLeftVisual.UseCustomFont = true;
    textLeftVisual.CustomFontFile = "Fonts/RainHearts.fnt";
    textLeftVisual.FontScale = 2.0f;
    textLeftVisual.Color = Color.White;

    var rightSprite = new SpriteRuntime();
    
    rightSprite.WidthUnits = DimensionUnitType.Absolute;
    rightSprite.Width = 32; 
    rightSprite.HeightUnits = DimensionUnitType.Absolute;
    rightSprite.Height = 32;
    rightSprite.XUnits = GeneralUnitType.PixelsFromLarge;
    rightSprite.X = -15;
    rightSprite.XOrigin = HorizontalAlignment.Right;
    rightSprite.Y = 0;
    rightSprite.YUnits = GeneralUnitType.PixelsFromMiddle;
    rightSprite.YOrigin = VerticalAlignment.Center;

    rightSprite.FromRegion(RightTexture);

    rightSprite.WidthUnits = DimensionUnitType.Absolute;
    rightSprite.Width = rightSprite.SourceRectangle.Width * 4; 
    rightSprite.HeightUnits = DimensionUnitType.Absolute;
    rightSprite.Height = rightSprite.SourceRectangle.Height * 4;

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

    StateSave highlighted = buttonVisual.States.Highlighted;
    highlighted.Apply = FocusedState.Apply;

    this.Visual.Children.Add(rightSprite);
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
