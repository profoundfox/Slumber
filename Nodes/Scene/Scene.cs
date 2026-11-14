using System.Collections.Generic;
using Opal.Managers;
using Opal.Runtime;

namespace Slumber;

public class Scene : Anchor,
  IReady,
  IEnterTree, 
  IPhysicsProcess, 
  IProcess, 
  ICall, 
  IExitTree
{
  public Dictionary<string, SpawnPointInfo> SpawnPoints { get; set; } = new();

  public string EntranceGateID { get; set; }

  public Rectangle CameraRect { get; set; }

  public Scene()
  {
    _Ready();
    Ready();
  }
  
  public virtual void _Ready() { }
  public virtual void Ready() { }

  public virtual void _EnterTree() { }
  public virtual void EnterTree() { }

  public virtual void _Process(float delta) { }
  public virtual void Process(float delta) { }

  public virtual void _PhysicsProcess(float delta) { }
  public virtual void PhysicsProcess(float delta) { }

  public virtual void _Submit(BatchLayer layer) { }
  public virtual void Submit(BatchLayer layer) { }

  public virtual void _ExitTree() { }
  public virtual void ExitTree()
  {
    SpawnPoints.Clear();
  }

  public void SetPhysicsProcess(bool value)
  {
    Core.Token.SetPhysicsProcess(this, value);
  }

  public void SetProcess(bool value)
  {
    Core.Token.SetProcess(this, value);
  }
}
