using Godot;
using RPGDEMO.Scripts.General;
using System.Linq;

namespace RPGDEMO.Scripts.Characters.Enemy;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 _targetPosition;
    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        Node3D target = CharacterNode.AttackAreaNode.GetOverlappingBodies().First();
        _targetPosition = target.GlobalPosition; // player will be given time to dodge from this position. 
        
        CharacterNode.AttackAreaNode.BodyExited += HandleAttackAreaBodyExited;
    }

    protected override void ExitState()
    {
        CharacterNode.AttackAreaNode.BodyExited -= HandleAttackAreaBodyExited;
    }
    private void HandleAttackAreaBodyExited(Node3D body)
    {
        CharacterNode.HitboxCollisionNode.SetDeferred("disabled", true);
        CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void PerformAttack()
    {
        CharacterNode.HitboxNode.GlobalPosition = _targetPosition;
    }
}