

using System.Linq;
using System.Text.RegularExpressions;

namespace Slumber;

public class GameManager : Object
{
  public Persistence Persistence { get; set; }
  public JangoPersistence JangoPersistence { get; set; }

  public Data Data { get; set; }

  public Player Player { get; set; }

  public ScreenEffects ScreenEffects;

  public GameManager()
  {
    Persistence = new();
    JangoPersistence = new();
    Data = new (Persistence);

    ScreenEffects = new ScreenEffects().Set(n => n.Detach());
  }

  public void Save(Checkpoint c)
  {
    Data.CurrentRespawnScene = Core.Token.Anchor.GetCurrentAnchor().GetType().Name;
    Data.CurrentRespawnPoint = c.Transform.Global.Position;
  }


  public void Save(string scene, Vector2 pos)
  {
    Persistence.CurrentSpawnPoint = pos;
    Persistence.CurrentSpawnScene = scene;

    string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string myGameFolder = System.IO.Path.Combine(saveFolder, "Slumber");
    System.IO.Directory.CreateDirectory(myGameFolder);

    FileT.ToBinary(Persistence, System.IO.Path.Combine(myGameFolder, "Persistence"));
    FileT.ToBinary(JangoPersistence, System.IO.Path.Combine(myGameFolder, "JangoPersistence"));
  }

  public void Load()
  {
    string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string myGameFolder = System.IO.Path.Combine(saveFolder, "Slumber");
    System.IO.Directory.CreateDirectory(myGameFolder);

    FileT.FromBinary(Persistence, System.IO.Path.Combine(myGameFolder, "Persistence"));
    FileT.FromBinary(JangoPersistence, System.IO.Path.Combine(myGameFolder, "JangoPersistence"));
    
    Transition(Persistence.CurrentSpawnScene, () =>
    {
      Player.Position = Persistence.CurrentSpawnPoint;
    });
  }

  public void Change(string targetScene, string targetID)
  {
    Transition(targetScene, () =>
    {
      var s = Core.Token.Anchor.GetCurrentAnchor() as Scene;
      s.EntranceGateID = targetID;
      int playerDir = Data.PlayerViewDirection;
      if (s.SpawnPoints.TryGetValue($"{targetID}_right", out var rightInfo) && 
          s.SpawnPoints.TryGetValue($"{targetID}_left", out var leftInfo))
      {
        Console.WriteLine("Yes");
        if (playerDir == 1)
          Player.Position = rightInfo.Position;
        else if (playerDir == -1)
          Player.Position = leftInfo.Position;
      }
      else
        Player.Position = s.SpawnPoints[targetID].Position;
      Player.Properties.AllowControl = false;
    });
  }

  public void Transition(string targetScene, Action onNewScene)
  {
    ScreenEffects.In();
    Await.Until(() => ScreenEffects.Transition.IsFinished, () =>
    {
      Type t = null;
      
      t = Type.GetType(targetScene);

      if (t == null)
      {
        foreach (var asembly in AppDomain.CurrentDomain.GetAssemblies())
        {
          t = asembly.GetTypes().FirstOrDefault(x => x.Name == targetScene || x.FullName == targetScene);
          if (t != null) break; 
        }
      }

      if (t != null)
      {
        var n = Core.Token.Anchor.SetAnchor(t);
        
        ScreenEffects.Out();
      }
      else
      {
        Console.WriteLine($"Error: Could not find type matching '{targetScene}' in any asembly.");
      }

      ScreenEffects.Out();
      Await.Until(() => Core.Token.Anchor.GetCurrentAnchor() != null, onNewScene);
    });
  }

  private bool canBeHazard = true;

  public void HitHazard()
  {
    if (!canBeHazard)
      return;

    canBeHazard = false;

    Player.STM.ChangeState("TransitionState");

    Core.Token.Get<PixelCamera>().Shake(TimeSpan.FromSeconds(0.05), 15, 10);

    Player.Visible = false;
    Player.Properties.CanTakeDamage = false;

    Data.PlayerCurrentHealth -= 1;

    Player.HealthIcons.Where(n => n.Frame == 0).LastOrDefault().Frame = 1;
    Player.HealthIcons.RemoveAt(Player.HealthIcons.Count - 1);

    Player.Properties.AllowControl = false;

    ScreenEffects.In();
    Await.Until(() => ScreenEffects.Transition.IsFinished, () =>
    {
      Player.Position = Data.LastSafePoint;
      Player.Properties.CanTakeDamage = true;
      Player.Visible = true;
      canBeHazard = true;
      ScreenEffects.Out();
      Await.Span(TimeSpan.FromSeconds(0.35f), () =>
      {
        Player.STM.ChangeState("IdleState");
        Player.Properties.AllowControl = true;
        canBeHazard = true;
      });
    });
  }

  public void TriggerDeath()
  {
    var cam = Core.Token.Get<PixelCamera>();
    cam?.toggleShake = true;
    Await.Span(TimeSpan.FromSeconds(0.1f), () => cam?.toggleShake = false);
    Player.QueueFree();
    Data.PlayerCurrentHealth = 5;

    Transition(Data.CurrentRespawnScene, () =>
    {
      Player.Position = Data.CurrentRespawnPoint;
    });

  }
}
