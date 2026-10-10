

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

public class MainMenu : Scene
{
  public Grid Root;
  public StackPanel MainPanel;
  public StackPanel Settings;
  public Keyboard Keyboard;

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

    Root.Height = 640;
    Root.Width = 360;

    Root.Y = -360;

    MainPanel = new StackPanel();
    Root.AddChild(MainPanel, 1, 1);
    
    MainPanel.XUnits = GeneralUnitType.PixelsFromSmall;
    MainPanel.XOrigin = HorizontalAlignment.Left;
    
    MainPanel.YUnits = GeneralUnitType.PixelsFromMiddle;
    MainPanel.YOrigin = VerticalAlignment.Center;

    MainPanel.X = 200;

    var startBtn = new CustomButton();
    MainPanel.AddChild(startBtn);

    startBtn.Y = 5;

    Check();
    
    void Check()
    {
      
      string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      string myGameFolder = System.IO.Path.Combine(saveFolder, "Slumber");
      System.IO.Directory.CreateDirectory(myGameFolder);

      if (!File.Exists(Path.Combine(myGameFolder, "Persistence")))
      {
        startBtn.Text = "Start";
        startBtn.Click += (sender, args) =>
        {
          Main.GameManager.Change("Caverns1", "door_1");
          MainPanel.RemoveFromRoot();
        };
      }
      else
      {
        startBtn.Text = "Continue";
        startBtn.Click += (sender, args) =>
        {
          Main.GameManager.Load();
          MainPanel.RemoveFromRoot();
        };
      }
    }


    startBtn.IsFocused = true;

    var setBtn = new CustomButton();
    MainPanel.AddChild(setBtn);

    setBtn.Y = 5;

    setBtn.Text = "Settings";
    setBtn.Click += (sender, args) =>
    {
      MainPanel.Disable();
      Settings.Enable();
      Settings.IsVisible = true;
      Settings.Children.FirstOrDefault()?.IsFocused = true; 
    };

    //setBtn.IsEnabled = false;
    
    var exitBtn = new CustomButton();
    MainPanel.AddChild(exitBtn);

    exitBtn.Y = 5;

    exitBtn.Text = "Quit";
    exitBtn.Click += (sender, args) =>
    {
      MainPanel.IsVisible = false;
      Main.GameManager.ScreenEffects.In();
      Await.Until(() => Main.GameManager.ScreenEffects.Transition.IsFinished, () => Core.Quit());
    };

    #region Settings

    Settings = new StackPanel();
    Root.AddChild(Settings, 1, 2);

    Settings.XUnits = GeneralUnitType.PixelsFromSmall;
    Settings.XOrigin = HorizontalAlignment.Left;
    
    Settings.YUnits = GeneralUnitType.PixelsFromMiddle;
    Settings.YOrigin = VerticalAlignment.Center;

    Settings.X = 400;

    Settings.IsVisible = false;

    var contrBtn = new CustomButton();
    Settings.AddChild(contrBtn);

    contrBtn.Y = 5;

    contrBtn.Text = "Controls";
    contrBtn.Click += (sender, args) =>
    {
    };

    var keybBtn = new CustomButton();
    Settings.AddChild(keybBtn);

    keybBtn.Y = 5;

    keybBtn.Text = "Keyboard";
    keybBtn.Click += (sender, args) =>
    {
      Keyboard.IsVisible = true;
      Settings.Disable();
      Keyboard.Enable();
      var firstChild = Keyboard.Children.OfType<CustomButton>().FirstOrDefault().IsFocused = true;
    };

    var audBtn = new CustomButton();
    Settings.AddChild(audBtn);

    audBtn.Y = 5;

    audBtn.Text = "Audio";
    audBtn.Click += (sender, args) =>
    {
    };

    var dltBtn = new CustomButton();
    Settings.AddChild(dltBtn);

    dltBtn.Y = 5;

    dltBtn.Text = "Delete Save";
    dltBtn.Click += (sender, args) =>
    {

      string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      string myGameFolder = System.IO.Path.Combine(saveFolder, "Slumber");
      System.IO.Directory.CreateDirectory(myGameFolder);

      File.Delete(Path.Combine(myGameFolder, "Persistence"));
      File.Delete(Path.Combine(myGameFolder, "JangoPersistence"));

      MainPanel.RemoveFromRoot();
      Settings.RemoveFromRoot();
      Main.GameManager.ScreenEffects.In();
      Main.GameManager.Transition("MainMenu", () => Main.GameManager.ScreenEffects.Out());
    };


    Keyboard = new Keyboard();
    Root.AddChild(Keyboard, 1, 3);

    Keyboard.X = -100;
    Keyboard.IsVisible = false;
    #endregion
  }

  public void Back()
  {
    if (MainPanel.IsEnabled)
      return;

    else if (Keyboard.IsEnabled)
    {
      Keyboard.IsVisible = false;
      Keyboard.Disable();
      Settings.Enable();
      Settings.Children.OfType<CustomButton>().Where(n => n.Text == "Keyboard").FirstOrDefault().IsFocused = true;
    }

    else if (Settings.IsEnabled)
    {
      Settings.IsVisible = false;
      Settings.Disable();
      MainPanel.Enable();
      MainPanel.Children.OfType<CustomButton>().Where(n => n.Text == "Settings").FirstOrDefault().IsFocused = true;
    }
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
