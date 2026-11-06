using System.Linq;

namespace Slumber;

public class SceneChange : Area2D
{
  public string TargetSceneName { get; set; }
  public string TargetGateID { get; set; }

  public Sprite2D EnterText { get; set; }

  public bool Trigger;

  public override void _EnterTree()
  {
    base._EnterTree();

    EnterText = new Sprite2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Interact"), new Rectangle(0, 0, 48, 16));
      n.Visible = false;
      n.SetParent(this);
    });
  }

  public override void _Process(float delta)
  {
    base._Process(delta);

    if (GetAnyBody() is Player p)
    {
      if (Trigger)
        EnterText.Visible = false;
      if (Trigger && !(Main.Binds.InteractKey != Keys.None && Core.Input.Keyboard.WasJustPressed(Main.Binds.InteractKey)
            || Main.Binds.InteractButton != Buttons.None && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.InteractButton)
            || Main.Binds.InteractMouse != MouseButton.None && Core.Input.Mouse.WasJustPressed(Main.Binds.InteractMouse)))
        return;
      
      OnSceneChange(p);
    }

    EnterText.Visible = false;

  }

  public virtual void OnSceneChange(Player p)
  {
    p.QueueFree();
    Main.GameManager.Change(TargetSceneName, TargetGateID);
  }
}

