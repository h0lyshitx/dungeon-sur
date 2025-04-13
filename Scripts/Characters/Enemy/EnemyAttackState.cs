using System.Linq;
using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Enemy;

public partial class EnemyAttackState : EnemyState
{
    [Export]
    private Timer _attackTimerNode;
    private Vector3 _targetPosition;

    protected override void EnterState()
    {
        PerformAttack();
        CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    public override void _Ready()
    {
        base._Ready();
        _attackTimerNode.Timeout += HandleAttackTimeout;
    }

    private void HandleAnimationFinished(StringName animname)
    {
        CharacterNode.HitboxCollisionNode.SetDeferred("disabled", true);
        _attackTimerNode.Start();
    }

    private void HandleAttackTimeout()
    {
        PerformAttack();
    }

    private void PerformAttack()
    {
        Node3D target = CharacterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            Node3D chaseTarget = CharacterNode
                .ChaseAreaNode.GetOverlappingBodies()
                .FirstOrDefault();
            if (chaseTarget == null)
            {
                CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();
            }
            else
            {
                CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            }
        }
        else
        {
            Vector3 direction = CharacterNode.GlobalPosition.DirectionTo(target.GlobalPosition);
            CharacterNode.SpriteNode.FlipH = direction.X < 0;
            _targetPosition = target.GlobalPosition;
            CharacterNode.HitboxNode.GlobalPosition = _targetPosition;
            CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        }
    }
}
