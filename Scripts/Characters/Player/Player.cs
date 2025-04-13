using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Player;

public partial class Player : Character
{
    private const float GRAVITY = 1000f;

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        if (!IsOnFloor())
        {
            velocity.Y -= GRAVITY * (float)delta;
        }
        Velocity = velocity;
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }
}
