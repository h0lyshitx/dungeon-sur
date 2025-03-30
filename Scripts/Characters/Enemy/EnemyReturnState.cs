using Godot;
using RPGDEMO.Scripts.Characters.Player;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();
        Destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        CharacterNode.AgentNode.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.AgentNode.IsNavigationFinished())
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }
        Move();
    }
}
