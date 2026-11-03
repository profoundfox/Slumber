using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using RenderingLibrary;
using RenderingLibrary.Graphics;

namespace Slumber;

public class PauseMenu : Node2D
{
  public bool Open = false;
  
  public Grid Root;

  public StackPanel MainPanel;

  public CustomButton ResumeBtn;
  public CustomButton SettingsBtn;
  public CustomButton QuitBtn;

  public bool NavigationLock;

  public override void EnterTree()
  {
    Visible = false;

    Root = new Grid();
    Root.Visual.AttachManagersOnly(SystemManagers.Default);

    Root.Width = 640;
    Root.Height = 360;

    MainPanel = new StackPanel();
    Root.AddChild(MainPanel, 0, 0);

    MainPanel.XOrigin = HorizontalAlignment.Center;

    MainPanel.WidthUnits = DimensionUnitType.Absolute;
    MainPanel.Width = 100;

    ResumeBtn = new CustomButton();
    MainPanel.AddChild(ResumeBtn);

    ResumeBtn.Text = "Resume";

    ResumeBtn.Click += (sender, args) =>
    {
      CloseMenu();
    };

    SettingsBtn = new CustomButton();
    MainPanel.AddChild(SettingsBtn);

    SettingsBtn.Text = "Settings";

    QuitBtn = new CustomButton();
    MainPanel.AddChild(QuitBtn);

    QuitBtn.Text = "To Menu";
  }

  public void OpenMenu()
  {
    Open = true;
    Visible = true;
    ResumeBtn.IsFocused = true;
    Core.Time.TimeScale = 0f;
  }

  public void CloseMenu()
  {
    Open = false;
    Visible = false;
    Core.Time.TimeScale = 1f;
  }

  public override void Submit(Canvas2D canvas)
  {
    Main.GumManager.Submit(Root.Visual); 
  }

  public void Move(bool upPressed, bool downPressed, bool rightPressed, bool leftPressed)
  {
    if (!upPressed && !downPressed && !rightPressed && !leftPressed)
      return;

    switch (true)
    {
      case true when ResumeBtn.IsFocused:
        if (downPressed)
          SettingsBtn.IsFocused = true;
        break;
      case true when SettingsBtn.IsFocused:
        if (upPressed)
          ResumeBtn.IsFocused = true;
        if (downPressed)
          QuitBtn.IsFocused = true;
        break;
      case true when QuitBtn.IsFocused:
        if (upPressed)
          SettingsBtn.IsFocused = true;
        break;
    }
  }

  public override void Process(float delta)
  {
    Root.X = Transform.Global.Position.X;
    Root.Y = Transform.Global.Position.Y;
    
    Root.IsVisible = Material.Global.Visible;

    if (Main.Binds.PauseKey != null && Core.Input.Keyboard.WasJustPressed(Main.Binds.PauseKey.Value)
        || Main.Binds.PauseButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.PauseButton.Value)
        || Main.Binds.PauseMouse != null && Core.Input.Mouse.WasJustPressed(Main.Binds.PauseMouse.Value))
    {
      if (!Open)
      {
        OpenMenu();
      }
      else
      {
        CloseMenu();
      }
    }

    if (NavigationLock)
      return;

    bool upPressed = Core.Input.Keyboard.WasJustPressed(Keys.Up);
    bool downPressed = Core.Input.Keyboard.WasJustPressed(Keys.Down);
    bool rightPressed = Core.Input.Keyboard.WasJustPressed(Keys.Right);
    bool leftPressed = Core.Input.Keyboard.WasJustPressed(Keys.Left);

    Move(upPressed, downPressed, rightPressed, leftPressed);
  }
}
