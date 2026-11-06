namespace Slumber;

public class Binds
{
  public Keys LeftKey { get; set; }
  public Buttons LeftButton { get; set; }
  public Keys RightKey { get; set; }
  public Buttons RightButton { get; set; }
  public Keys DownKey { get; set; }
  public Buttons DownButton { get; set; }
  public Keys UpKey { get; set; }
  public Buttons UpButton { get; set; }

  public Keys JumpKey { get; set { field = value; if (JumpMouse is not MouseButton.None && value != Keys.None) JumpMouse = MouseButton.None; } }
  public Buttons JumpButton { get; set; }
  public MouseButton JumpMouse { get; set { field = value; if (JumpKey is not Keys.None && value != MouseButton.None) JumpKey = Keys.None; } }

  public Keys DashKey { get; set { field = value; if (DashMouse is not MouseButton.None && value != Keys.None) DashMouse = MouseButton.None; } }
  public Buttons DashButton { get; set; } 
  public MouseButton DashMouse { get; set { field = value; if (DashKey is not Keys.None && value != MouseButton.None) DashKey = Keys.None; } }

  public Keys InteractKey { get; set { field = value; if (InteractMouse is not MouseButton.None && value != Keys.None) InteractMouse = MouseButton.None; } }
  public Buttons InteractButton { get; set; }
  public MouseButton InteractMouse { get; set { field = value; if (InteractKey is not Keys.None && value != MouseButton.None) InteractKey = Keys.None; } }

  public Keys AttackKey { get; set { field = value; if (AttackMouse is not MouseButton.None && value != Keys.None) AttackMouse = MouseButton.None; } }
  public Buttons AttackButton { get; set; }
  public MouseButton AttackMouse { get; set { field = value; if (AttackKey is not Keys.None && value != MouseButton.None) AttackKey = Keys.None; } }

  public Keys PauseKey { get; set { field = value; if (PauseMouse is not MouseButton.None && value != Keys.None) PauseMouse = MouseButton.None; } }
  public Buttons PauseButton { get; set; }
  public MouseButton PauseMouse { get; set { field = value; if (PauseKey is not Keys.None && value != MouseButton.None) PauseKey = Keys.None; } }

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
      jumpMouse: MouseButton.None,
      dashKey: Keys.E,
      dashButton: Buttons.RightTrigger,
      dashMouse: MouseButton.None,
      interactKey: Keys.W,
      interactButton: Buttons.DPadUp,
      interactMouse: MouseButton.None,
      attackKey: Keys.K,
      attackButton: Buttons.Y,
      attackMouse: MouseButton.None,
      pauseKey: Keys.Escape,
      pauseButton: Buttons.Start,
      pauseMouse: MouseButton.None
  );

  public Binds(
      Keys leftKey, Buttons leftButton,
      Keys rightKey, Buttons rightButton,
      Keys downKey, Buttons downButton,
      Keys upKey, Buttons upButton,
      Keys jumpKey, Buttons jumpButton, MouseButton jumpMouse,
      Keys dashKey, Buttons dashButton, MouseButton dashMouse,
      Keys interactKey, Buttons interactButton, MouseButton interactMouse,
      Keys attackKey, Buttons attackButton, MouseButton attackMouse,
      Keys pauseKey, Buttons pauseButton, MouseButton pauseMouse)
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
