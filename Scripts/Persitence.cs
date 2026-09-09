namespace Slumber;

public class Persistence : Object
{
  public Vector2 CurrentSpawnPoint { get; set; }
  public string CurrentSpawnScene { get; set; }

  public int MaxHealthPoints { get; set; } = 5;
  
  public string CurrentBonfireId { get; set; }
}
