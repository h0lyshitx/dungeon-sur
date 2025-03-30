using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Enemy;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer _idleTimerNode;

    [Export(PropertyHint.Range, "0, 20, 0.1")] private float _maxIdleTime = 4; 
    private int _pointIndex = 0;

    protected override void EnterState()
    {
        _pointIndex = 1;
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        Destination = GetPointGlobalPosition(_pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;

        CharacterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        _idleTimerNode.Timeout += HandleTimeout;
        CharacterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        CharacterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        _idleTimerNode.Timeout -= HandleTimeout;
        CharacterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_idleTimerNode.IsStopped())
        {
            return;
        }
        Move();
    }

    private void HandleNavigationFinished()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        _idleTimerNode.WaitTime = rng.RandfRange(2, _maxIdleTime);
        
        _idleTimerNode.Start();
    }
    
    private void HandleTimeout()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        _pointIndex = Mathf.Wrap(_pointIndex + 1, 0, CharacterNode.PathNode.Curve.GetPointCount());
        Destination = GetPointGlobalPosition(_pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;
    }
}
