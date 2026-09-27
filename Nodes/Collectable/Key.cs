namespace Slumber;

public class Key : Node2D
{
  public Area2D Area { get; set; }
  public Sprite2D Sprite { get; set; }

  bool pickedUp = false;

  public override void EnterTree()
  {
    AddChild(Area);
    AddChild(Sprite);
  }

  public override void Process(float delta)
  {
    if (Area.GetAnyBody() is Player p && !pickedUp)
    {
      pickedUp = true;

      if (Main.GameManager.Persistence.Items.TryGetValue("key", out int count))
        count += 1;
      else
        Main.GameManager.Persistence.Items.Add("key", 1);

      QueueFree();
    }
  }
}
