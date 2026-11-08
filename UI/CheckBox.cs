using Gum.Converters;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using RenderingLibrary.Graphics;

namespace Slumber;

public class CustomCheckBox : Button
{
  public SpriteRuntime FocusThingy { get; set; }

  public bool NormalDisabledFocus { get; set; } = true;

  public bool IsChecked { get; set; }

  public CustomCheckBox()
  {
    var buttonVisual = (Gum.Forms.DefaultVisuals.V3.ButtonVisual)Visual;

    float offset = 20f;
    
    NineSliceRuntime background = buttonVisual.Background;

    buttonVisual.Background.Texture = Core.Resources.Pixel.Source;

    buttonVisual.TextInstance.RemoveFromRoot();

    buttonVisual.WidthUnits = DimensionUnitType.RelativeToChildren;
    buttonVisual.Width = 10;
    
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

    var checkVisual = new SpriteRuntime();
    AddChild(checkVisual);

    checkVisual.Texture = Core.Resource.Load<Texture2D>("Graphics/CheckButton");
    checkVisual.TextureAddress = TextureAddress.Custom;
    checkVisual.SourceRectangle = new Rectangle(0, 0, 32, 16);
    checkVisual.WidthUnits = DimensionUnitType.Absolute;
    checkVisual.Width = checkVisual.SourceRectangle.Width * 4; 
    checkVisual.HeightUnits = DimensionUnitType.Absolute;
    checkVisual.Height = checkVisual.SourceRectangle.Height * 4;

    StateSave checkedState = new StateSave();
    checkedState.Apply = () =>
    {
      
    };

    StateSave notCheckedState = new StateSave();
    checkedState.Apply = () =>
    {

    };

    StateSave enabledState = buttonVisual.States.Enabled;
    enabledState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      FocusThingy.Visible = false;
    };

    StateSave pushedState = buttonVisual.States.Pushed;
    pushedState.Apply = () =>
    {
      IsChecked = !IsChecked;

      if (IsChecked)
      {
        background.Color = Color.White * 0f;
        FocusThingy.Visible = true;
      }
    };

    StateSave focusedState = buttonVisual.States.Focused;
    focusedState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      IsFocused = true;
      FocusThingy.Visible = true;
      FocusThingy.Color = Color.White;
    };

    StateSave disabledFoucsState = buttonVisual.States.DisabledFocused;
    disabledFoucsState.Apply = () =>
    {
      background.Color = Color.White * 0f;
      IsFocused = true;
      FocusThingy.Visible = true;
      FocusThingy.Color = ColorExtension.Hex("#808796");
    };

    StateSave disabledState = buttonVisual.States.Disabled;
    disabledState.Apply = () =>
    {
      if (NormalDisabledFocus)
      {
        background.Color = Color.White * 0f;
        FocusThingy.Visible = false;
      }
      else 
      {
        DisabledFocused();
      }
    };


    StateSave highlightedFocused = buttonVisual.States.HighlightedFocused;
    highlightedFocused.Apply = focusedState.Apply;
    highlightedFocused.Apply += () =>
    {
      IsFocused = true;
    };

    StateSave highlighted = buttonVisual.States.Highlighted;
    highlighted.Apply = focusedState.Apply;
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
