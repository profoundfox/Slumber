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
using RenderingLibrary;
using RenderingLibrary.Graphics;

namespace Slumber;

public class TitleScreen : Scene
{
  Grid Root;
  StackPanel MenuChooser;
  StackPanel SettingsChooser;

  Panel Settings;
  ContainerRuntime Controls;

  AnimatedSprite2D BG;

  CustomButton PlayBtn;
  CustomButton SetBtn;
  CustomButton QuitBtn;

  CustomButton BindBtn;

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

  bool ReadingInput;
  DubTextButton CurrentInputButton;

  Action<Keys> SetKey;
  Action<MouseButton> SetMouseButton;

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
    Root.Visual.AttachManagersOnly(SystemManagers.Default);

    Root.Width = 640;
    Root.Height = 360;

    MenuChooser = new StackPanel();
    Root.AddChild(MenuChooser, 0, 0);
    
    MenuChooser.XUnits = GeneralUnitType.PixelsFromSmall;
    MenuChooser.XOrigin = HorizontalAlignment.Center;
    
    MenuChooser.YUnits = GeneralUnitType.PixelsFromSmall;
    MenuChooser.YOrigin = VerticalAlignment.Top;

    MenuChooser.X = -360;
    MenuChooser.Y = -180;

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
          Root.Visual.RemoveFromManagers();
        };
      }
      else
      {
        PlayBtn.Text = "Continue";
        PlayBtn.Click += (sender, args) =>
        {
          Main.GameManager.Load();
          Main.AudioManager.FadeOut();
          Root.Visual.RemoveFromManagers();
        };
      }
    }

    PlayBtn.IsFocused = true;

    SetBtn = new CustomButton();
    MenuChooser.AddChild(SetBtn);
    
    SetBtn.Text = "Settings";

    SetBtn.Click += (sender, args) =>
    {
      MenuChooser?.Disable();
      Settings?.IsVisible = true;
      Settings?.Enable();
      KeyBtn.IsFocused = true;
    };

    SetBtn.NormalDisabledFocus = false;

    QuitBtn = new CustomButton();
    MenuChooser.AddChild(QuitBtn);

    QuitBtn.Text = "Quit";

    QuitBtn.Click += (sender, args) =>
    {
      Root.RemoveFromRoot();
      Main.GameManager.ScreenEffects.In();
      Main.AudioManager.FadeOut();
      Await.Until(() => Main.GameManager.ScreenEffects.Transition.IsFinished, () => Core.Quit());
    };

    SettingsChooser = new StackPanel();
    Root.AddChild(SettingsChooser, 0, 1);

    SettingsChooser.XUnits = GeneralUnitType.PixelsFromSmall;
    SettingsChooser.XOrigin = HorizontalAlignment.Center;
    
    SettingsChooser.YUnits = GeneralUnitType.PixelsFromSmall;
    SettingsChooser.YOrigin = VerticalAlignment.Top;

    SettingsChooser.X = -90;
    SettingsChooser.Y = -180;

    SettingsChooser.IsVisible = false;

    BindBtn = new CustomButton();
    SettingsChooser.AddChild(BindBtn);

    BindBtn.Text = "Keybinds";

    Settings = new Panel();
    Root.AddChild(Settings, 0, 1);

    Settings.IsVisible = false;

    Settings.Width = 640;
    Settings.Height = 360;

    Settings.XOrigin = HorizontalAlignment.Center;
    Settings.XUnits = GeneralUnitType.PixelsFromSmall;

    Settings.WidthUnits = DimensionUnitType.Absolute;
    Settings.HeightUnits = DimensionUnitType.Absolute;
    
    var settingsBG = new NineSliceRuntime();
    Settings.AddChild(settingsBG);

    settingsBG.Texture = Core.Resource.Load<Texture2D>("Graphics/Settings-9-Slice");
    settingsBG.WidthUnits = DimensionUnitType.RelativeToParent;
    settingsBG.HeightUnits = DimensionUnitType.RelativeToParent;

    settingsBG.X = -31;
    settingsBG.Y = -31;
    settingsBG.Width = 190;

    var setBtns = new StackPanel();
    setBtns.WidthUnits = DimensionUnitType.RelativeToParent;
    setBtns.WidthUnits = DimensionUnitType.RelativeToParent;

    Settings.AddChild(setBtns);

    KeyBtn = new CustomButtonSquare();
    setBtns.AddChild(KeyBtn);

    KeyBtn.Click += (sender, args) =>
    {
      Controls?.Visible = true;
      Controls?.Enable();
      UpButton?.IsFocused = true;
      Settings?.Disable();
      Settings?.IsVisible = false;
    };

    KeyBtn.WidthUnits = DimensionUnitType.RelativeToParent;

    KeyBtn.Text = "Keyboard";

    Controls = new ContainerRuntime();
    Root.AddChild(Controls, 0, 2);

    Controls.Visible = false;
    Controls.Disable();

    Controls.WidthUnits = DimensionUnitType.Absolute;
    Controls.HeightUnits = DimensionUnitType.Absolute;

    Controls.Width = 370;
    Controls.Height = 570;

    Controls.XUnits = GeneralUnitType.PixelsFromSmall;
    Controls.XOrigin = HorizontalAlignment.Center;
    
    Controls.YUnits = GeneralUnitType.PixelsFromSmall;
    Controls.YOrigin = VerticalAlignment.Center;

    Controls.X = 36;
    Controls.Y = -36;
   
    var conteolsBG = new NineSliceRuntime();
    Controls.AddChild(conteolsBG);

    conteolsBG.Texture = Core.Resource.Load<Texture2D>("Graphics/Settings-9-Slice");
    conteolsBG.WidthUnits = DimensionUnitType.RelativeToParent;
    conteolsBG.HeightUnits = DimensionUnitType.RelativeToParent;

    conteolsBG.X = -31;
    conteolsBG.Y = -31;

    var contBtns = new StackPanel();
    Controls.AddChild(contBtns);
    contBtns.WidthUnits = DimensionUnitType.RelativeToParent;
    contBtns.WidthUnits = DimensionUnitType.RelativeToParent;


    UpButton = new DubTextButton("Up",$"{Main.UpKey}");
    contBtns.AddChild(UpButton);

    UpButton.Click += (sender, args) =>
    {
      Rebind(UpButton, k => Main.UpKey = k, null);
    };

    DownButton = new DubTextButton("Down", $"{Main.DownKey}");
    contBtns.AddChild(DownButton);

    DownButton.Click += (sender, args) =>
    {
      Rebind(DownButton, k => Main.DownKey = k, null);
    };

    LeftButton = new DubTextButton("Left", $"{Main.LeftKey}");
    contBtns.AddChild(LeftButton);
   
    LeftButton.Click += (sender, args) =>
    {
      Rebind(LeftButton, k => Main.LeftKey = k, null);
    };
    
    RightButton = new DubTextButton("Right", $"{Main.RightKey}");
    contBtns.AddChild(RightButton);
    
    RightButton.Click += (sender, args) =>
    {
      Rebind(RightButton, k => Main.RightKey = k, null);
    };
    
    JumpButton = new DubTextButton("Jump", $"{Main.JumpKey.ToString()}");
    contBtns.AddChild(JumpButton);
    
    JumpButton.Click += (sender, args) =>
    {
      Rebind(JumpButton, k => Main.JumpKey = k, k => Main.JumpMouse = k);
    };
    
    DashButton = new DubTextButton("Dash", $"{Main.DashKey.ToString()}");
    contBtns.AddChild(DashButton);
    
    DashButton.Click += (sender, args) =>
    {
      Rebind(DashButton, k => Main.DashKey = k, k => Main.DashMouse = k);
    };

    AttackButton = new DubTextButton("Attack", $"{Main.AttackKey.ToString()}");
    contBtns.AddChild(AttackButton);
   
    AttackButton.Click += (sender, args) =>
    {
      Rebind(AttackButton, k => Main.AttackKey = k, k => Main.AttackMouse = k);
    };
    
    InteractButton = new DubTextButton("Interact", $"{Main.InteractKey.ToString()}");
    contBtns.AddChild(InteractButton);

    InteractButton.Click += (sender, args) =>
    {
      Rebind(InteractButton, k => Main.InteractKey = k, k => Main.InteractMouse = k);
    };

  }

  public void Rebind(DubTextButton button, Action<Keys> setKey, Action<MouseButton> setMouseButton)
  {
    ReadingInput = true;
    CurrentInputButton = button;
    SetKey = setKey;
    SetMouseButton = setMouseButton;

    return;

    Await.Until(() => 
    {
      KeyToRemap = Core.Input.Keyboard.GetDown();
      MouseButtonToRemap = Core.Input.Mouse.GetDown();
      return (KeyToRemap != Keys.None || MouseButtonToRemap != MouseButton.None);
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
    if (MenuChooser.IsEnabled)
      return;

    if (Controls.Visible)
    {
      Controls.Visible = false;
      Controls.Disable();
      Settings.IsVisible = true;
      Settings.Enable();
      KeyBtn.IsFocused = true;
      return;
    }
    if (Settings.IsVisible)
    {
      Settings.IsVisible = false;
      Settings.Disable();
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
          DownButton.IsFocused = true;
        break;
      case true when DownButton.IsFocused:
        if (upPressed)
          UpButton.IsFocused = true;
        if (downPressed)
          LeftButton.IsFocused = true;
        break;
      case true when LeftButton.IsFocused:
        if (upPressed)
          DownButton.IsFocused = true;
        if (downPressed)
          RightButton.IsFocused = true;
        break;
      case true when RightButton.IsFocused:
        if (upPressed)
          LeftButton.IsFocused = true;
        if (downPressed)
          JumpButton.IsFocused = true;
        break;
      case true when JumpButton.IsFocused:
        if (upPressed)
          RightButton.IsFocused = true;
        if (downPressed)
          DashButton.IsFocused = true;
        break;
      case true when DashButton.IsFocused:
        if (upPressed)
          JumpButton.IsFocused = true;
        if (downPressed)
          AttackButton.IsFocused = true;
        break;
      case true when AttackButton.IsFocused:
        if (upPressed)
          DashButton.IsFocused = true;
        if (downPressed)
          InteractButton.IsFocused = true;
        break;
      case true when InteractButton.IsFocused:
        if (upPressed)
          AttackButton.IsFocused = true;
        break;
    }
  }

  public override void Process(float delta)
  {
    base.Process(delta);

    BG.PlayAnimation("Main");


    void ReadInput()
    {
      NavigationLock = true;

      KeyToRemap = Core.Input.Keyboard.GetDown();
      MouseButtonToRemap = Core.Input.Mouse.GetDown();

      if (KeyToRemap == Keys.None && MouseButtonToRemap == MouseButton.None)
        return;

      if (KeyToRemap != Keys.None && SetKey != null)
      {
        SetKey(KeyToRemap);
        CurrentInputButton.RightLabel.Text = KeyToRemap.ToString();
      }
      else if (MouseButtonToRemap != MouseButton.None && SetMouseButton != null)
      {
        SetMouseButton(MouseButtonToRemap);
        CurrentInputButton.RightLabel.Text = MouseButtonToRemap.ToString();
      }

      NavigationLock = false;
      
      KeyToRemap = Keys.None;
      MouseButtonToRemap = MouseButton.None;

      SetKey = null;
      SetMouseButton = null;
    }

    if (ReadingInput)
      ReadInput();
      
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

    Main.GumManager.Submit(Root.Visual);
  }
}
