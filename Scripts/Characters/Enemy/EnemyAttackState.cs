using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Enemy;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        CharacterNode.AttackAreaNode.BodyExited += HandleAttackAreaBodyExited;
    }

    protected override void ExitState()
    {
        CharacterNode.AttackAreaNode.BodyExited -= HandleAttackAreaBodyExited;
    }
    private void HandleAttackAreaBodyExited(Node3D body)
    {
        
        CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }
}