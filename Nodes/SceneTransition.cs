using Slumber;

namespace Polybit;

public record class SceneTransitionConfig : SpatialNodeConfig
{
    public CollisionShape2D CollisionShape2D { get; set; }
    public string TargetScene { get; set; }
    public bool ResetScene { get; set; }
}

public class SceneAreaTransition : Node2D
{
    public CollisionShape2D CollisionShape2D;
    public string TargetScene;
    public Area2D TransitionZone;
    public bool ResetScene;

    public SceneAreaTransition(SceneTransitionConfig cfg) : base(cfg)
    {
        CollisionShape2D = cfg.CollisionShape2D;
        TargetScene = cfg.TargetScene;
        ResetScene = cfg.ResetScene;
    }

    public override void Load()
    {
        base.Load();

        TransitionZone = new Area2D(new AreaConfig
        {
            CollisionShape2D = this.CollisionShape2D,
            Parent = this
        });
    }

    public override void ProcessUpdate(float delta)
    {
        base.ProcessUpdate(delta);

        if (TransitionZone.AreaEntered(out Node2D overlapping))
        {
            if (overlapping is Player)
            {
                if (ResetScene)
                    Engine.Stage.ReloadCurrentStage();
                else
                    Engine.Stage.AddStageFromString(TargetScene);
            }
        }
    }

}