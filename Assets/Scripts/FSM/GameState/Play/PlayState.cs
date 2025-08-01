using Gehenna;

public class PlayState : BaseGameState
{
    private PlayStateConfig config;
    
    public PlayState(GameFlowManager owner, GameFlowParam param) : base(owner, param) { }

    public override void Enter()
    {
        base.Enter();
        
        if (!param.GameConfig.TryGetGameStateConfig<PlayStateConfig>(out config))
        {
            GehennaLogger.Log(this, LogType.Error, $"Failed to load {nameof(PlayStateConfig)}");
            return;
        }
    }

    public override void Update() { }
    public override void FixedUpdate() { }
}