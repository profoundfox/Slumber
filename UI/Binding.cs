using Gum.Forms.Controls;

namespace Slumber;

public class Binding : StackPanel
{
  public DubTextButton UpButton;
  public DubTextButton DownButton;
  public DubTextButton LeftButton;
  public DubTextButton RightButton;
  public DubTextButton JumpButton;
  public DubTextButton DashButton;
  public DubTextButton AttackButton;
  public DubTextButton InteractButton;

  public Keys KeyToRemap;

  public Binding()
  {
    UpButton = new DubTextButton("Up", "D");
    AddChild(UpButton);

    UpButton.Click += (sender, args) =>
    {
      Await.Until(() => 
      {
        KeyToRemap = Core.Input.Keyboard.GetDown();
        return KeyToRemap != Keys.None;
      }, () =>
      {
        Main.Binds.UpKey = KeyToRemap;
        UpButton.RightLabel.Text = Main.Binds.UpKey.ToString();
      });
    };

    DownButton = new DubTextButton("Down", "D");
    AddChild(DownButton);

    LeftButton = new DubTextButton("Left", "D");
    AddChild(LeftButton);
    
    RightButton = new DubTextButton("Right", "D");
    AddChild(RightButton);
    
    JumpButton = new DubTextButton("Jump", "D");
    AddChild(JumpButton);
    
    DashButton = new DubTextButton("Dash", "D");
    AddChild(DashButton);
   
    AttackButton = new DubTextButton("Attack", "D");
    AddChild(AttackButton);
   
    InteractButton = new DubTextButton("Interact", "D");
    AddChild(InteractButton);
  }
}
