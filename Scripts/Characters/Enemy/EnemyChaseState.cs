using Godot;
using System.Linq;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Enemy;

public partial class EnemyChaseState : EnemyState
{
    private CharacterBody3D _target;
    [Export] private Timer _updateDestinationNode;
    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        _target = CharacterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;
        _updateDestinationNode.Timeout += HandleTimeout;
        CharacterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        CharacterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }



    protected override void ExitState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        _updateDestinationNode.Timeout -= HandleTimeout;
        CharacterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        CharacterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited; 
    }
    
    public override void _PhysicsProcess(double delta)
    {
        
        Move();
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }
    
    private void HandleTimeout()
    {
        Destination = _target.GlobalPosition;
        CharacterNode.AgentNode.TargetPosition = Destination;
    }
}