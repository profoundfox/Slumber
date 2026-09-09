

using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using Gum.Wireframe;
using RenderingLibrary.Graphics;

namespace Slumber;

public class TextAndPluralSpriteButton : CustomButton
{
  private Label leftLabel;
  private ContainerRuntime rightContainer;
  private SpriteRuntime rightSprite;

  public void Set(string leftText, TextureRegion[] rightTextures)
  {
    rightContainer.Children.Clear();

    leftLabel.Text = leftText;

    for (int i = rightTextures.Length - 1; i >= 0; i--)
    {
      var rightTexture = rightTextures[i];

      rightSprite = new SpriteRuntime();
      rightContainer.AddChild(rightSprite);
      
      rightSprite.FromRegion(rightTexture);

      rightSprite.WidthUnits = DimensionUnitType.Absolute;
      rightSprite.Width = rightSprite.SourceRectangle.Width * 2; 
      rightSprite.HeightUnits = DimensionUnitType.Absolute;
      rightSprite.Height = rightSprite.SourceRectangle.Height * 2;

      rightSprite.X = 16;
      rightSprite.Y = 8;
    }
  }

  public TextAndPluralSpriteButton(string leftText, TextureRegion[] rightTextures)
  {
    this.Text = string.Empty; 

    this.WidthUnits = DimensionUnitType.Absolute;
    this.Width = 400; 
    
    this.HeightUnits = DimensionUnitType.RelativeToChildren;
    this.Height = 12;

    leftLabel = new Label();
    leftLabel.WidthUnits = DimensionUnitType.RelativeToChildren;
    leftLabel.Width = 0;
    leftLabel.HeightUnits = DimensionUnitType.RelativeToChildren;
    leftLabel.Height = 0;
    leftLabel.X = 15;
    leftLabel.Y = 0;
    leftLabel.YUnits = GeneralUnitType.PixelsFromMiddle;
    leftLabel.YOrigin = VerticalAlignment.Center;
    leftLabel.Text = leftText;
    AddChild(leftLabel);

    var textLeftVisual = (Gum.Forms.DefaultVisuals.V3.LabelVisual)leftLabel.Visual;
    textLeftVisual.UseCustomFont = true;
    textLeftVisual.CustomFontFile = "Fonts/RainHearts.fnt";
    textLeftVisual.FontScale = 1.0f;
    textLeftVisual.Color = Color.White;

    rightContainer = new ContainerRuntime();
    rightContainer.XUnits = GeneralUnitType.PixelsFromLarge;
    rightContainer.X = 100;
    rightContainer.XOrigin = HorizontalAlignment.Right; 
    rightContainer.HeightUnits = DimensionUnitType.RelativeToParent;
    rightContainer.WidthUnits = DimensionUnitType.RelativeToChildren;

    rightContainer.ChildrenLayout = Gum.Managers.ChildrenLayout.LeftToRightStack;

    this.Visual.Children.Add(rightContainer);

    Set(leftText, rightTextures);
  }
}
