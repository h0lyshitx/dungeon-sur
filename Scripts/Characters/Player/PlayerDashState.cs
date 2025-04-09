using DUNSUR.Scripts.General;
using Godot;

namespace DUNSUR.Scripts.Characters.Player;

public partial class PlayerDashState : PlayerState
{
    [Export]
    private Timer _dashTimerNode; // determine how long the player will dash

    [Export(PropertyHint.Range, "0, 30, 1")]
    private float _speed = 12;

    public override void _Ready()
    {
        base._Ready();
        _dashTimerNode.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        CharacterNode.Flip();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
        CharacterNode.Velocity = new(CharacterNode.Direction.X, 0, CharacterNode.Direction.Y);

        if (CharacterNode.Velocity == Vector3.Zero)
        {
            CharacterNode.Velocity = CharacterNode.SpriteNode.FlipH
                ? Vector3.Left
                : Vector3.Right;
        }

        CharacterNode.Velocity *= _speed;
        _dashTimerNode.Start();
    }

    private void HandleDashTimeout()
    {
        CharacterNode.Velocity = Vector3.Zero;
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        CharacterNode.HurtboxCollisionNode.SetDeferred("disabled", false);
    }
}
