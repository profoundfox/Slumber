


using System.IO;
using System.Linq;
using DotTiled.Serialization;
using Gum.Converters;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals;
using Gum.GueDeriving;
using Gum.Wireframe;
using MonoTile;
using RenderingLibrary.Graphics;

namespace Slumber;

public class TitleScreen : Scene
{
  Grid Root;
  StackPanel MenuChooser;

  Panel Settings;
  Panel SetButCont;
  Panel Controls;

  AnimatedSprite2D BG;

  CustomButton PlayBtn;
  CustomButton SetBtn;
  CustomButton QuitBtn;

  CustomButtonSquare KeyBtn;

  DubTextButton UpButton;
  DubTextButton DownButton;
  DubTextButton LeftButton;
  DubTextButton RightButton;
  DubTextButton JumpButton;
  DubTextButton DashButton;
  DubTextButton AttackButton;
  DubTextButton InteractButton;

  Keys KeyToRemap;
  MouseButton MouseButtonToRemap;

  bool NavigationLock;

  Point beforeLockPos;

  public override void EnterTree()
  {
    base.EnterTree();

    Core.Time.TimeScale = 1f; 

    Main.AudioManager.Play(Main.AudioManager.TitleScreen);

    BuildUI();

    var animations = AsepriteLoader.LoadAnimation(
        new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Atlas/Comet"), new Rectangle(0, 0, 7040, 180)),
        new Extent(320, 180)
    );

    BG = new AnimatedSprite2D().Set(n =>
    {
      n.Atlas = animations;
      n.Origin = Vector2.Zero;
      n.Scale = new Vector2(2);
    });

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

    MenuChooser = new StackPanel();
    Root.AddChild(MenuChooser, 0, 0);
    
    MenuChooser.XUnits = GeneralUnitType.PixelsFromSmall;
    MenuChooser.XOrigin = HorizontalAlignment.Left;
    
    MenuChooser.YUnits = GeneralUnitType.PixelsFromSmall;
    MenuChooser.YOrigin = VerticalAlignment.Top;

    MenuChooser.X = 407;
    MenuChooser.Y = 60;

    var slumLogo = new SpriteRuntime();
    slumLogo.FromRegion(new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/SlumberLogo"), new Rectangle(0, 0, 96, 22)));
    MenuChooser.AddChild(slumLogo);
    slumLogo.WidthUnits = DimensionUnitType.Absolute;
    slumLogo.Width = slumLogo.SourceRectangle.Width * 4; 
    slumLogo.HeightUnits = DimensionUnitType.Absolute;
    slumLogo.Height = slumLogo.SourceRectangle.Height * 4;

    slumLogo.XOrigin = HorizontalAlignment.Center;

    slumLogo.X = 113;

    PlayBtn = new CustomButton();
    MenuChooser.AddChild(PlayBtn);

    Check();
    
    void Check()
    {
      
      string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      string myGameFolder = System.IO.Path.Combine(saveFolder, "Slumber");
      System.IO.Directory.CreateDirectory(myGameFolder);

      if (!File.Exists(Path.Combine(myGameFolder, "Persistence")))
      {
        PlayBtn.Text = "Start";
        PlayBtn.Click += (sender, args) =>
        {
          Main.GameManager.Change("Caverns1", "door_1");
          Main.AudioManager.FadeOut();
          Root.RemoveFromRoot();
        };
      }
      else
      {
        PlayBtn.Text = "Continue";
        PlayBtn.Click += (sender, args) =>
        {
          Main.GameManager.Load();
          Main.AudioManager.FadeOut();
          Root.RemoveFromRoot();
        };
      }
    }

    PlayBtn.IsFocused = true;
    //PlayBtn.X = -113;

    SetBtn = new CustomButton();
    MenuChooser.AddChild(SetBtn);
    
    SetBtn.Text = "Settings";
    //SetBtn.X = -113;

    SetBtn.Click += (sender, args) =>
    {
      MenuChooser?.IsVisible = false;
      MenuChooser?.Disable();
      Settings?.IsVisible = true;
      Settings?.Enable();
      KeyBtn.IsFocused = true;
    };

    QuitBtn = new CustomButton();
    MenuChooser.AddChild(QuitBtn);

    QuitBtn.Text = "Quit";
    //QuitBtn.X = -113;

    QuitBtn.Click += (sender, args) =>
    {
      Root.RemoveFromRoot();
      Main.GameManager.ScreenEffects.In();
      Main.AudioManager.FadeOut();
      Await.Until(() => Main.GameManager.ScreenEffects.Transition.IsFinished, () => Core.Quit());
    };

    Settings = new Panel();
    Root.AddChild(Settings, 0, 1);

    Settings.IsVisible = false;

    Settings.X = 50;
    Settings.Y = -50;

    Settings.Width = 880;
    Settings.Height = 520;

    Settings.WidthUnits = DimensionUnitType.Absolute;
    Settings.HeightUnits = DimensionUnitType.Absolute;
    
    var nineslice = new NineSliceRuntime();
    Settings.AddChild(nineslice);

    nineslice.Texture = Core.Resource.Load<Texture2D>("Graphics/Settings-9-Slice");
    nineslice.WidthUnits = DimensionUnitType.RelativeToParent;
    nineslice.HeightUnits = DimensionUnitType.RelativeToParent;

    SetButCont = new StackPanel();
    Settings.AddChild(SetButCont);

    SetButCont.X = 31;
    SetButCont.Y = 30;

    SetButCont.WidthUnits = DimensionUnitType.RelativeToParent;
    SetButCont.HeightUnits = DimensionUnitType.RelativeToParent;

    SetButCont.Width = -91;
    SetButCont.Height = -60;

    KeyBtn = new CustomButtonSquare();
    SetButCont.AddChild(KeyBtn);

    KeyBtn.Click += (sender, args) =>
    {
      Controls?.IsVisible = true;
      Controls?.Enable();
      UpButton.IsFocused = true;
      SetButCont?.IsVisible = false;
      SetButCont?.Disable();
    };

    KeyBtn.WidthUnits = DimensionUnitType.RelativeToParent;

    KeyBtn.Text = "Keyboard";

    Controls = new StackPanel();
    Settings.AddChild(Controls);

    Controls.IsVisible = false;
    Controls.Disable();

    Controls.X = 31;
    Controls.Y = 30;

    Controls.WidthUnits = DimensionUnitType.RelativeToParent;
    Controls.HeightUnits = DimensionUnitType.RelativeToParent;

    Controls.Width = -91;
    Controls.Height = -60;

    var row1 = new StackPanel();
    Controls.AddChild(row1);
    row1.Orientation = Orientation.Horizontal;
    
    row1.Spacing = 100;
    
    UpButton = new DubTextButton("Up",$"{Main.UpKey}");
    row1.AddChild(UpButton);

    UpButton.Click += (sender, args) =>
    {
      Rebind(UpButton, k => Main.UpKey = k, null);
    };

    DownButton = new DubTextButton("Down", $"{Main.DownKey}");
    row1.AddChild(DownButton);

    DownButton.Click += (sender, args) =>
    {
      Rebind(DownButton, k => Main.DownKey = k, null);
    };

    DownButton.XUnits = GeneralUnitType.PixelsFromLarge;

    var row2 = new StackPanel();
    Controls.AddChild(row2);
    row2.Orientation = Orientation.Horizontal;

    row2.Spacing = 100;

    LeftButton = new DubTextButton("Left", $"{Main.LeftKey}");
    row2.AddChild(LeftButton);
   
    LeftButton.Click += (sender, args) =>
    {
      Rebind(LeftButton, k => Main.LeftKey = k, null);
    };
    
    RightButton = new DubTextButton("Right", $"{Main.RightKey}");
    row2.AddChild(RightButton);
    
    RightButton.Click += (sender, args) =>
    {
      Rebind(RightButton, k => Main.RightKey = k, null);
    };
    
    var row3 = new StackPanel();
    Controls.AddChild(row3);
    row3.Orientation = Orientation.Horizontal;

    row3.Spacing = 100;
    
    JumpButton = new DubTextButton("Jump", $"{Main.JumpKey.ToString()}");
    row3.AddChild(JumpButton);
    
    JumpButton.Click += (sender, args) =>
    {
      Rebind(JumpButton, k => Main.JumpKey = k, k => Main.JumpMouse = k);
    };
    
    DashButton = new DubTextButton("Dash", $"{Main.DashKey.ToString()}");
    row3.AddChild(DashButton);
    
    DashButton.Click += (sender, args) =>
    {
      Rebind(DashButton, k => Main.DashKey = k, k => Main.DashMouse = k);
    };
    
    var row4 = new StackPanel();
    Controls.AddChild(row4);
    row4.Orientation = Orientation.Horizontal;

    row4.Spacing = 100;

    AttackButton = new DubTextButton("Attack", $"{Main.AttackKey.ToString()}");
    row4.AddChild(AttackButton);
   
    AttackButton.Click += (sender, args) =>
    {
      Rebind(AttackButton, k => Main.AttackKey = k, k => Main.AttackMouse = k);
    };
    
    InteractButton = new DubTextButton("Interact", $"{Main.InteractKey.ToString()}");
    row4.AddChild(InteractButton);

    InteractButton.Click += (sender, args) =>
    {
      Rebind(InteractButton, k => Main.InteractKey = k, k => Main.InteractMouse = k);
    };
  }

  public void Rebind(DubTextButton button, Action<Keys> setKey, Action<MouseButton> setMouseButton)
  {
    Await.Until(() => 
    {
      KeyToRemap = Core.Input.Keyboard.GetDown();
      MouseButtonToRemap = Core.Input.Mouse.GetDown();
      return KeyToRemap != Keys.None || MouseButtonToRemap != MouseButton.None;
    }, () =>
    {
      NavigationLock = true;
      if (KeyToRemap != Keys.None && setKey != null)
      {
        setKey(KeyToRemap);
        button.RightLabel.Text = KeyToRemap.ToString();
      }
      else if (MouseButtonToRemap != MouseButton.None && setMouseButton != null)
      {
        setMouseButton(MouseButtonToRemap);
        button.RightLabel.Text = MouseButtonToRemap.ToString();
      }
      NavigationLock = false;
      KeyToRemap = Keys.None;
      MouseButtonToRemap = MouseButton.None;
    });
  }

  public void Back()
  {
    if (MenuChooser.IsVisible)
      return;

    if (Controls.IsVisible)
    {
      Controls.IsVisible = false;
      Controls.Disable();
      SetButCont.IsVisible = true;
      SetButCont.Enable();
      KeyBtn.IsFocused = true;
      return;
    }
    if (Settings.IsVisible)
    {
      Settings.IsVisible = false;
      Settings.Disable();
      MenuChooser.IsVisible = true;
      MenuChooser.Enable();
      SetBtn.IsFocused = true;
      return; 
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

  public void Move(bool upPressed, bool downPressed, bool leftPressed, bool rightPressed)
  {
    if (!upPressed && !downPressed && !leftPressed && !rightPressed)
      return;

    switch (true)
    {
      case true when PlayBtn.IsFocused:
        if (downPressed) 
          SetBtn.IsFocused = true;
        break;
      case true when SetBtn.IsFocused:
        if (upPressed)
          PlayBtn.IsFocused = true;
        if (downPressed)
          QuitBtn.IsFocused = true;
        break;
      case true when QuitBtn.IsFocused:
        if (upPressed)
          SetBtn.IsFocused = true;
        break;
      case true when UpButton.IsFocused:
        if (downPressed)
          LeftButton.IsFocused = true;
        if (rightPressed)
          DownButton.IsFocused = true;
        break;
      case true when DownButton.IsFocused:
        if (downPressed)
          RightButton.IsFocused = true;
        if (leftPressed)
          LeftButton.IsFocused = true;
        break;
      case true when LeftButton.IsFocused:
        if (upPressed)
          UpButton.IsFocused = true;
        if (downPressed)
          JumpButton.IsFocused = true;
        if (rightPressed)
          RightButton.IsFocused = true;
        break;
      case true when RightButton.IsFocused:
        if (upPressed)
          DownButton.IsFocused = true;
        if (downPressed)
          DashButton.IsFocused = true;
        if (leftPressed)
          LeftButton.IsFocused = true;
        break;
      case true when JumpButton.IsFocused:
        if (upPressed)
          LeftButton.IsFocused = true;
        if (downPressed)
          AttackButton.IsFocused = true;
        if (rightPressed)
          DashButton.IsFocused = true;
        break;
      case true when DashButton.IsFocused:
        if (upPressed)
          RightButton.IsFocused = true;
        if (downPressed)
          InteractButton.IsFocused = true;
        if (leftPressed)
          JumpButton.IsFocused = true;
        break;
      case true when AttackButton.IsFocused:
        if (upPressed)
          JumpButton.IsFocused = true;
        if (rightPressed)
          InteractButton.IsFocused = true;
        break;
      case true when InteractButton.IsFocused:
        if (upPressed)
          DashButton.IsFocused = true;
        if (leftPressed)
          AttackButton.IsFocused = true;
        break;
    }
  }

  public override void Process(float delta)
  {
    base.Process(delta);

    BG.PlayAnimation("Main");

    if (Main.CurrentInputType != InputType.Mouse)
    {
      Core.Instance.IsMouseVisible = false;
      Core.Input.Mouse.SetPosition(0, 0);
    }
    if (Main.CurrentInputType == InputType.Mouse)
    {
      Core.Instance.IsMouseVisible = true;
    }
      
    if (NavigationLock)
      return;

    if (Core.Input.Keyboard.WasJustPressed(Keys.X))
      Back();

    bool upPressed = Core.Input.Keyboard.WasJustPressed(Keys.Up);
    bool downPressed = Core.Input.Keyboard.WasJustPressed(Keys.Down);
    bool leftPressed = Core.Input.Keyboard.WasJustPressed(Keys.Left);
    bool rightPressed = Core.Input.Keyboard.WasJustPressed(Keys.Right);

    if (Main.CurrentInputType == InputType.Keyboard && Main.PreviousInputType == InputType.Keyboard)
      Move(upPressed, downPressed, leftPressed, rightPressed);
  }

  public override void Submit(Canvas2D canvas)
  {
    base.Submit(canvas);
  }
}
