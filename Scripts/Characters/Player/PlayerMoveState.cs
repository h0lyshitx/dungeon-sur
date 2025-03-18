using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Player;

public partial class PlayerMoveState : Node
{
    private Player _characterNode;

    public override void _Ready()
    {
        _characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_characterNode.Direction == Vector2.Zero)
        {
            _characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        _characterNode.Velocity = new(_characterNode.Direction.X, 0, _characterNode.Direction.Y);
        _characterNode.Velocity *= _characterNode.Movespeed;
        _characterNode.MoveAndSlide();
        _characterNode.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            _characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            _characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }
}
