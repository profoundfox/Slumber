namespace Slumber;

public record struct SpawnPointInfo
{
  public Vector2 Position { get; set; }
  public Vector2 Direction { get; set; }
  public bool? Contingent { get; set; } = null;

  public SpawnPointInfo()
  {
    Contingent = null;
  }
}
