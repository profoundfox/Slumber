


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

  AnimatedSprite2D BG;

  CustomButton PlayBtn;
  CustomButton SetBtn;
  CustomButton QuitBtn;

  CustomButtonSquare KeyBtn;

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
    
    //MenuChooser.YUnits = GeneralUnitType.PixelsFromMiddle;
    MenuChooser.YOrigin = VerticalAlignment.Center;

    MenuChooser.X = 100;
    MenuChooser.Y = 209;

    var slumLogo = new SpriteRuntime();
    slumLogo.FromRegion(new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/SlumberLogo"), new Rectangle(0, 0, 96, 22)));
    MenuChooser.AddChild(slumLogo);
    slumLogo.WidthUnits = DimensionUnitType.Absolute;
    slumLogo.Width = slumLogo.SourceRectangle.Width * 4; 
    slumLogo.HeightUnits = DimensionUnitType.Absolute;
    slumLogo.Height = slumLogo.SourceRectangle.Height * 4;

    slumLogo.XOrigin = HorizontalAlignment.Center;

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
    PlayBtn.X = -113;

    SetBtn = new CustomButton();
    MenuChooser.AddChild(SetBtn);
    
    SetBtn.Text = "Settings";
    SetBtn.X = -113;

    SetBtn.NormalDisabledFocus = false;

    SetBtn.Click += (sender, args) =>
    {
      MenuChooser?.Disable();
      Settings?.Enable();
      Settings?.IsVisible = true;
      KeyBtn.IsFocused = true;
    };

    QuitBtn = new CustomButton();
    MenuChooser.AddChild(QuitBtn);

    QuitBtn.Text = "Quit";
    QuitBtn.X = -113;

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

    Settings.X = 300;
    Settings.Y = -50;

    Settings.Width = 700;
    Settings.Height = 400;

    Settings.WidthUnits = DimensionUnitType.Absolute;
    Settings.HeightUnits = DimensionUnitType.Absolute;
    
    var nineslice = new NineSliceRuntime();
    Settings.AddChild(nineslice);

    nineslice.Texture = Core.Resource.Load<Texture2D>("Graphics/Settings-9-Slice");
    nineslice.WidthUnits = DimensionUnitType.RelativeToParent;
    nineslice.HeightUnits = DimensionUnitType.RelativeToParent;

    var setButCont = new StackPanel();
    Settings.AddChild(setButCont);

    setButCont.X = 31;
    setButCont.Y = 30;

    setButCont.WidthUnits = DimensionUnitType.RelativeToParent;
    setButCont.HeightUnits = DimensionUnitType.RelativeToParent;

    setButCont.Width = -91;
    setButCont.Height = -60;

    KeyBtn = new CustomButtonSquare();
    setButCont.AddChild(KeyBtn);

    KeyBtn.WidthUnits = DimensionUnitType.RelativeToParent;

    KeyBtn.Text = "Keyboard";

    var controls = new StackPanel();
    Settings.AddChild(controls);

    controls.X = 31;
    controls.Y = 30;

    controls.WidthUnits = DimensionUnitType.RelativeToParent;
    controls.HeightUnits = DimensionUnitType.RelativeToParent;

    controls.Width = -91;
    controls.Height = -60;

    var row1 = new StackPanel();
    controls.AddChild(row1);
    row1.Orientation = Orientation.Horizontal;

  }

  public void Back()
  {
    if (MenuChooser.IsEnabled)
      return;
    if (Settings.IsEnabled)
    {
      Settings.Disable();
      Settings.IsVisible = false;
      MenuChooser.Enable();
      SetBtn.IsFocused = true;
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

    BG.PlayAnimation("Main");


    bool upPressed = Core.Input.Keyboard.WasKeyJustPressed(Keys.Up);
    bool downPressed = Core.Input.Keyboard.WasKeyJustPressed(Keys.Down);

    if (!upPressed && !downPressed)
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
    }
  }

  public override void Submit(Canvas2D canvas)
  {
    base.Submit(canvas);
  }
}
