using System.Collections.Generic;
using Gum.Converters;
using Gum.Forms.Controls;
using RenderingLibrary.Graphics;

namespace Slumber;

public class Keyboard : StackPanel
{
  public Keyboard()
  {
    this.XUnits = GeneralUnitType.PixelsFromMiddle;
    this.XOrigin = HorizontalAlignment.Center;
    
    this.YUnits = GeneralUnitType.PixelsFromMiddle;
    this.YOrigin = VerticalAlignment.Center;

    this.Height = 20;
    this.Width = 20;

    var lookUpTable = new Dictionary<string, Rectangle>
    {
      { "Key.A", new Rectangle(288, 176, 16, 16) },
      { "Key.D", new Rectangle(320, 176, 16, 16) },
      { "Key.S", new Rectangle(304, 176, 16, 16) },
      { "Key.W", new Rectangle(288, 160, 16, 16) },
      { "Key.Space", new Rectangle(496, 224, 48, 16) },
      { "Key.E", new Rectangle(304, 160, 16, 16) },
      { "Key.K", new Rectangle(400, 176, 16, 16) },
      { "Key.Escape", new Rectangle(272, 128, 16, 16) },
      { "Key.X", new Rectangle(320, 192, 16, 16) },
      { "Button.LeftThumbstickLeft", new Rectangle(192, 96, 16, 16) },
      { "Button.LeftThumbstickRight", new Rectangle(160, 96, 16, 16) },
      { "Button.LeftThumbstickDown", new Rectangle(176, 96, 16, 16) },
      { "Button.LeftThumbstickUp", new Rectangle(144, 96, 16, 16) },
      { "Button.B", new Rectangle(144, 0, 16, 16) },
      { "Button.Y", new Rectangle(176, 0, 16, 16) },
      { "Button.DPadLeft", new Rectangle(64, 48, 16, 16) },
      { "Button.DPadRight", new Rectangle(32, 48, 16, 16) },
      { "Button.DPadDown", new Rectangle(48, 48, 16, 16) },
      { "Button.DPadUp", new Rectangle(16, 48, 16, 16) },
      { "Button.X", new Rectangle(160, 0, 16, 16) },
      { "Button.LeftShoulder", new Rectangle(176, 272, 16, 16) },
      { "Button.Start", new Rectangle(224, 320, 16, 16) },
    };

    var source = Core.Resource.Load<Texture2D>("Graphics/Atlas/InputIconsWhite");

    var kbvBox = new StackPanel();
    AddChild(kbvBox);

    kbvBox.XUnits = GeneralUnitType.PixelsFromMiddle;
    kbvBox.XOrigin = HorizontalAlignment.Center;
    
    kbvBox.YUnits = GeneralUnitType.PixelsFromMiddle;
    kbvBox.YOrigin = VerticalAlignment.Center;

    foreach(var bind in Core.Input.Binds)
    {
      string valResult = "";
      foreach (var val in bind.Value)
      {
        string i = string.Empty;
        if (val.HasKey)
          valResult += $" Key.{val.Key} ";
        if (val.HasButton)
          valResult += $" Button.{val.Button} ";
        if (val.HasMouseButton)
          valResult += $" Mouse.{val.MouseButton} ";
      }
      
      string[] bindNames = valResult.Split(" ", StringSplitOptions.RemoveEmptyEntries);
      TextureAtlas bindIcons = new TextureAtlas(source);
      for (int i = 0; i < bindNames.Length; i++)
      {
        var name = bindNames[i];
        var lookUp = lookUpTable[name];
        bindIcons.AddRegion(name, lookUp.X, lookUp.Y, lookUp.Width, lookUp.Height);
      }

      var button = new TextAndPluralSpriteButton(bind.Key, bindIcons.GetAllRegions());
      button.Y = 5;

      kbvBox.AddChild(button);
    }

    var resetBtn = new CustomButton();
    AddChild(resetBtn);

    resetBtn.Y = 5;

    resetBtn.Text = "Reset";
    resetBtn.Click += (sender, args) =>
    {
      Core.Quit();
    };

  }
}
