using System.Collections.Generic;

namespace Slumber;

public class JangoPersistence : BaseObject
{
  public Dictionary<string, bool> GatesOpen = new();
  public string CheckpointTriggered;
}
