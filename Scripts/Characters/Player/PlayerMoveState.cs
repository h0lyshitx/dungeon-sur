using System;
using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Player;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0, 20, 0.1")]
    protected float Movespeed = 8;

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.Direction == Vector2.Zero)
        {
            CharacterNode.Velocity = new Vector3(0, CharacterNode.Velocity.Y, 0); // leave y velocity for gravity
            CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        CharacterNode.Velocity = new(CharacterNode.Direction.X, 0, CharacterNode.Direction.Y);
        CharacterNode.Velocity *= Movespeed;

        // CharacterNode.MoveAndSlide(); -> this is done in Player class
        CharacterNode.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
