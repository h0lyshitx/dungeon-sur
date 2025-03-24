using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Enemy;

public partial class EnemyReturnState : CharacterState
{
    private Vector3 _destination;

    public override void _Ready()
    {
        base._Ready();
        Vector3 localPos = CharacterNode.PathNode.Curve.GetPointPosition(0);
        Vector3 globalPos = CharacterNode.PathNode.GlobalPosition;
        _destination = globalPos + localPos;
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        CharacterNode.GlobalPosition = _destination;
    }
}
