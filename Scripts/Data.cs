namespace Slumber;

public class Data : BaseObject
{
  public int PlayerCurrentHealth { get; set; }

  public int PlayerViewDirection { get; set; } = 1;

  public Vector2 LastSafePoint { get; set; }

  public Vector2 CurrentRespawnPoint { get; set; }
  public string CurrentRespawnScene { get; set; }

  public Data(Persistence p)
  {
    PlayerCurrentHealth = p.MaxHealthPoints;
  }
}
