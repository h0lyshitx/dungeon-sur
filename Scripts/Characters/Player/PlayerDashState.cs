using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Player;

public partial class PlayerDashState : Node
{
    private Player _characterNode;

    [Export]
    private Timer _dashTimerNode;

    [Export]
    private float _speed = 12;

    public override void _Ready()
    {
        _characterNode = GetOwner<Player>();
        _dashTimerNode.Timeout += HandleDashTimeout;
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        _characterNode.MoveAndSlide();
        _characterNode.Flip();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            _characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
            _characterNode.Velocity = new(
                _characterNode.Direction.X,
                0,
                _characterNode.Direction.Y
            );

            _characterNode.Velocity = _characterNode.PlayerSpriteNode.FlipH
                ? Vector3.Left
                : Vector3.Right;

            _characterNode.Velocity *= _speed;
            _dashTimerNode.Start();
            SetPhysicsProcess(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
        }
    }

    private void HandleDashTimeout()
    {
        _characterNode.Velocity = Vector3.Zero;
        _characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}
