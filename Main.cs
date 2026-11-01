using System;

using ImGuiNET;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameGum;
using Gum.Converters;
using RenderingLibrary.Graphics;

namespace Slumber
{
  public enum InputType
  {
    Keyboard,
    Mouse,
    Controller
  }

  public class Main : Core
  {
    public Main() { }

    public static GameManager GameManager;

    public static Gum.GumService GumUI => Gum.GumService.Default;

    public static AudioManager AudioManager;
    public static GumManager GumManager;

    public static Keys? LeftKey = Keys.A;
    public static Buttons? LeftButton = Buttons.LeftThumbstickLeft;
    public static Keys? RightKey = Keys.D;
    public static Buttons? RightButton = Buttons.LeftThumbstickRight;
    public static Keys? DownKey = Keys.S;
    public static Buttons? DownButton = Buttons.LeftThumbstickDown;
    public static Keys? UpKey = Keys.W;
    public static Buttons? UpButton = Buttons.LeftThumbstickUp;
    public static Keys? JumpKey = Keys.Space;
    public static Buttons? JumpButton = Buttons.X;
    public static MouseButton? JumpMouse = null;
    public static Keys? DashKey = Keys.E;
    public static Buttons? DashButton = Buttons.RightTrigger;
    public static MouseButton? DashMouse = null;
    public static Keys? InteractKey = Keys.W;
    public static Buttons? InteractButton = Buttons.DPadUp;
    public static MouseButton? InteractMouse = null;
    public static Keys? AttackKey = Keys.K;
    public static Buttons? AttackButton = Buttons.Y;
    public static MouseButton? AttackMouse = MouseButton.Left;
    public static Keys? PauseKey = Keys.Escape;
    public static Buttons? PauseButton = Buttons.Start;
    public static MouseButton? PauseMouse = null;

    public static InputType CurrentInputType = InputType.Keyboard;
    public static InputType PreviousInputType;

    protected override void Initialize()
    {
      base.Initialize();

      GumUI.Initialize(this);

      GumUI.UseKeyboardDefaults();
      GumUI.UseGamepadDefaults();

      ClassDB.Initialize(typeof(Main).Assembly);

      GameManager = new GameManager();
      AudioManager = new AudioManager();
      GumManager = new GumManager();
      GumManager.Initialize();

      Token.Anchor.SetAnchor<TitleScreen>();

      Prefs.Graphics.Fullscreen = false;
      Graphics.SynchronizeWithVerticalRetrace = true;
      Graphics.HardwareModeSwitch = true;
      IsFixedTimeStep = true;

      #if DEBUG
      IsFixedTimeStep = true;
      Graphics.SynchronizeWithVerticalRetrace = false;
      Prefs.Graphics.Fullscreen = false;
      #endif

      Prefs.General.ShowCollision = false;

      ExitOnEsc = false;

      Prefs.Apply();
    
      Core.Instance.Graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
      base.LoadContent();
    }

    protected override void UnloadContent()
    {
      base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      GumUI.Update(gameTime);
      GumManager.Update(gameTime);

      #if !DEBUG 
      Mouse.SetPosition(0, 0);
      #endif

      AudioManager.Update(gameTime);

      PreviousInputType = CurrentInputType;

      if (Core.Input.Keyboard.GetDown() != Keys.None)
        CurrentInputType = InputType.Keyboard;
      if (Core.Input.CurrentGamePad.GetDown() != Buttons.None)
        CurrentInputType = InputType.Controller;
      if (Core.Input.Mouse.GetDown() != MouseButton.None || Core.Input.Mouse.PositionDelta != Point.Zero)
        CurrentInputType = InputType.Mouse;
    }

    bool showCollision;
    float gravity;
    float iniTermVel;
    float secTermVel;
    float fallGrav;
    float movSpe;
    float jmpFrc;

    float xImpulse;
    float yImpulse;

    bool init;

    protected override void Draw(GameTime gameTime)
    {
      base.Draw(gameTime);
      
      //GumUI.Draw();
      GumManager.Draw(gameTime);

      #if DEBUG

      var player = Core.Token.Get<Player>();

      var camera = Core.Token.Get<PixelCamera>();

      var sumBool = false;

      if (player == null || Graphics.IsFullScreen || sumBool)
        return;

      if (!init)
      {
        gravity = player.Properties.BaseGravity;
        iniTermVel = player.Properties.InitialTerminalVelocity;
        secTermVel = player.Properties.SecondaryTerminalVelocity;
        fallGrav = player.Properties.FallGravity;
        movSpe = player.Properties.MoveSpeed;
        jmpFrc = player.Properties.JumpForce;

        init = true;
      }

      Core.Prefs.General.ShowCollision = showCollision;
      Core.Prefs.Apply();

      Core.ImGuiRenderer.BeforeLayout(gameTime);  
      ImGui.Begin("Player");  
      ImGui.Text($"Velocity: {player.Velocity.ToString()}");
      ImGui.Text($"Position: {player.Transform.Global.Position.ToString()}");
      ImGui.Text($"Visibility: {player.Visible}");
      ImGui.Text($"Term: {MathF.Round(player.Properties.CurrentTerminalVelocity / 100f) * 100f}");
      ImGui.Text($"State: {player.Get<StateMachine>()?.Current}");
      ImGui.Text($"Count: {Core.Token.GetAll<Player>().Count}");

      ImGui.InputFloat("XImpulse", ref xImpulse);
      ImGui.InputFloat("YImpulse", ref yImpulse);
      if (ImGui.Button("Impulse!")) 
      {
          player.Velocity = new Vector2(xImpulse, yImpulse);
      }

      ImGui.PushItemWidth(150);

      ImGui.InputFloat("BaseGravity", ref gravity);
      ImGui.SameLine();
      if (ImGui.Button("Reset##BaseGravity")) 
      {
          gravity = 950f;
      }


      ImGui.InputFloat("InitialTerminal", ref iniTermVel);
      ImGui.SameLine();
      if (ImGui.Button("Reset##InitialTerminal")) 
      {
          iniTermVel = 250f;
      }


      ImGui.InputFloat("SecondaryTerminal", ref secTermVel);
      ImGui.SameLine();
      if (ImGui.Button("Reset##SecondaryTerminal")) 
      {
          secTermVel = 6400f;
      }

      ImGui.InputFloat("FallGravity", ref fallGrav);
      ImGui.SameLine();
      if (ImGui.Button("Reset##FallGravity")) 
      {
          fallGrav = 1500f;
      }

      ImGui.InputFloat("MoveSpeed", ref movSpe);
      ImGui.SameLine();
      if (ImGui.Button("Reset##MoveSpeed")) 
      {
        movSpe = 130f;
      }

      ImGui.InputFloat("JumpForce", ref jmpFrc);
      ImGui.SameLine();
      if (ImGui.Button("Reset##JumpForce")) 
      {
        jmpFrc = -360;
      }

      ImGui.PopItemWidth();
      ImGui.End();

      ImGui.Begin("Debug");

      ImGui.Checkbox("Show-Collision", ref showCollision);

      ImGui.End();

      Core.ImGuiRenderer.AfterLayout();

      player.Properties = player.Properties with
      {
        BaseGravity = gravity,
        InitialTerminalVelocity = iniTermVel,
        SecondaryTerminalVelocity = secTermVel,
        FallGravity = fallGrav,
        MoveSpeed = movSpe,
        JumpForce = jmpFrc
      };  

      #endif
    }
  }
}
