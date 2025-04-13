using DungeonSurvival.Scripts.Resources;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Enemy;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 Destination;

    public override void _Ready()
    {
        base._Ready();
        CharacterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 localPos = CharacterNode.PathNode.Curve.GetPointPosition(index);
        Vector3 globalPos = CharacterNode.PathNode.GlobalPosition;
        return globalPos + localPos;
    }

    protected void Move()
    {
        CharacterNode.AgentNode.GetNextPathPosition();
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(Destination);
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }
    protected void HandleChaseAreaBodyExited(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }
}
