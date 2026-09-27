namespace Slumber;

public class Gate : SceneChange
{
  public Sprite2D Sprite;
  public bool Open { get; set; }

  public override void _EnterTree()
  {
    base._EnterTree();
    
    Sprite = new Sprite2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Maps/Atlas"), new Rectangle(448, 496, 288, 112));
      n.HFrames = 2;
      n.Origin = Vector2.Zero;
      n.SetParent(this);
    });

    AddChild(new CollisionShape2D().Set(n =>
    {
      n.Position = new Vector2(45, 12);
      n.Shape = new RectangleShape2D(63, 100);
    }));
    
    if (Main.GameManager.JangoPersistence.GatesOpen.TryGetValue(this.Name, out bool open))
      Open = open;
    else
      Main.GameManager.JangoPersistence.GatesOpen.Add(this.Name, Open);

    Sprite.Frame = Open ? 1 : 0;

    Trigger = true;
  }

  public override void _Process(float delta)
  {
    base._Process(delta);

    if (Main.GameManager.Persistence.Items.TryGetValue("key", out int count) && count > 0)
    {
      if (GetAnyBody() is Player p)
      {

        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
          Open = true;
          Main.GameManager.JangoPersistence.GatesOpen[this.Name] = Open;
        }
      }
    }

    Sprite.Frame = Open ? 1 : 0;
  }

  public override void OnSceneChange(Player p)
  {
    if (!Open)
      return;

    base.OnSceneChange(p);
  }
}
