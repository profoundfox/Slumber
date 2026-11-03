namespace Slumber;

public record struct Binds
{
  public Keys? LeftKey { get; set; }
  public Buttons? LeftButton { get; set; }
  public Keys? RightKey { get; set; }
  public Buttons? RightButton { get; set; }
  public Keys? DownKey { get; set; }
  public Buttons? DownButton { get; set; }
  public Keys? UpKey { get; set; }
  public Buttons? UpButton { get; set; }

  public Keys? JumpKey { get; set { field = value; if (JumpMouse is not null) JumpMouse = null; } }
  public Buttons? JumpButton { get; set; }
  public MouseButton? JumpMouse { get; set { field = value; if (JumpKey is not null) JumpKey = null; } }

  public Keys? DashKey { get; set { field = value; if (DashMouse is not null) DashMouse = null; } }
  public Buttons? DashButton { get; set; } 
  public MouseButton? DashMouse { get; set { field = value; if (DashKey is not null) DashKey = null; } }

  public Keys? InteractKey { get; set { field = value; if (InteractMouse is not null) InteractMouse = null; } }
  public Buttons? InteractButton { get; set; }
  public MouseButton? InteractMouse { get; set { field = value; if (InteractKey is not null) InteractKey = null; } }

  public Keys? AttackKey { get; set { field = value; if (AttackMouse is not null) AttackMouse = null; } }
  public Buttons? AttackButton { get; set; }
  public MouseButton? AttackMouse { get; set { field = value; if (AttackKey is not null) AttackKey = null; } }

  public Keys? PauseKey { get; set { field = value; if (PauseMouse is not null) PauseMouse = null; } }
  public Buttons? PauseButton { get; set; }
  public MouseButton? PauseMouse { get; set { field = value; if (PauseKey is not null) PauseKey = null; } }

  public static Binds Identity => new Binds(
      leftKey: Keys.A,
      leftButton: Buttons.LeftThumbstickLeft,
      rightKey: Keys.D,
      rightButton: Buttons.LeftThumbstickRight,
      downKey: Keys.S,
      downButton: Buttons.LeftThumbstickDown,
      upKey: Keys.W,
      upButton: Buttons.LeftThumbstickUp,
      jumpKey: Keys.Space,
      jumpButton: Buttons.X,
      jumpMouse: null,
      dashKey: Keys.E,
      dashButton: Buttons.RightTrigger,
      dashMouse: null,
      interactKey: Keys.W,
      interactButton: Buttons.DPadUp,
      interactMouse: null,
      attackKey: Keys.K,
      attackButton: Buttons.Y,
      attackMouse: null,
      pauseKey: Keys.Escape,
      pauseButton: Buttons.Start,
      pauseMouse: null
  );

  public Binds(
      Keys? leftKey, Buttons? leftButton,
      Keys? rightKey, Buttons? rightButton,
      Keys? downKey, Buttons? downButton,
      Keys? upKey, Buttons? upButton,
      Keys? jumpKey, Buttons? jumpButton, MouseButton? jumpMouse,
      Keys? dashKey, Buttons? dashButton, MouseButton? dashMouse,
      Keys? interactKey, Buttons? interactButton, MouseButton? interactMouse,
      Keys? attackKey, Buttons? attackButton, MouseButton? attackMouse,
      Keys? pauseKey, Buttons? pauseButton, MouseButton? pauseMouse)
  {
    LeftKey = leftKey;
    LeftButton = leftButton;
    RightKey = rightKey;
    RightButton = rightButton;
    DownKey = downKey;
    DownButton = downButton;
    UpKey = upKey;
    UpButton = upButton;
    JumpKey = jumpKey;
    JumpButton = jumpButton;
    JumpMouse = jumpMouse;
    DashKey = dashKey;
    DashButton = dashButton;
    DashMouse = dashMouse;
    InteractKey = interactKey;
    InteractButton = interactButton;
    InteractMouse = interactMouse;
    AttackKey = attackKey;
    AttackButton = attackButton;
    AttackMouse = attackMouse;
    PauseKey = pauseKey;
    PauseButton = pauseButton;
    PauseMouse = pauseMouse;
  }
}
