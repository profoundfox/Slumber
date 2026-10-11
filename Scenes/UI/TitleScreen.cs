


using System.IO;
using System.Linq;
using DotTiled.Serialization;
using Gum.Converters;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals;
using Gum.Wireframe;
using MonoTile;
using RenderingLibrary.Graphics;

namespace Slumber;

public class TitleScreen : Scene
{
  Grid Root;
  ListBox Settings;
  StackPanel MenuChooser;

  public override void EnterTree()
  {
    base.EnterTree();

    Core.Time.TimeScale = 1f; 

    BuildUI();
    
    new CanvasAnchor().Set(n =>
    {
      n.BackBufferColor = new Color(44, 41, 38);
    });
  }

  public void BuildUI()
  {
    Root = new Grid();
    Root.AddToRoot();

    Root.Width = 640;
    Root.Height = 360;

    Root.X = 180;
    Root.Y = 180;
    
    Settings = new ListBox();
    Root.AddChild(Settings, 1, 1);

    Settings.Width = 640;
    Settings.Height = 360;
    Settings.X = 20;

    Settings.InnerPanel.StackSpacing = 2;

    for (int i = 0; i < 30; i++)
    {
        var button = new CustomButton();
        Settings.AddChild(button);
        button.Text = "Button " + i;
        button.Click += (_, _) =>
            button.Text = DateTime.Now.ToString();
    }
    
    MenuChooser = new StackPanel();
    Root.AddChild(MenuChooser, 2, 1);
  }

  public void Back()
  {
  }


  public override void ExitTree()
  {
    base.ExitTree();
  }

  public override void PhysicsUpdate(float delta)
  {
    base.PhysicsUpdate(delta);
  }

  public override void Process(float delta)
  {
    base.Process(delta);

    if (Core.Input.Keyboard.WasKeyJustPressed(Keys.X))
      Back();

  }

  public override void Submit(Canvas2D canvas)
  {
    base.Submit(canvas);
  }
}
