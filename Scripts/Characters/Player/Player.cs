using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Player;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer AnimPlayerNode;

    [Export]
    public Sprite3D PlayerSpriteNode;

    [Export]
    public StateMachine StateMachineNode;

    [Export]
    public float Movespeed = 8;

    public Vector2 Direction = new();

    // public override void _Ready() { }


    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;
        if (isNotMovingHorizontally)
        {
            return;
        }

        bool isMovingLeft = Velocity.X < 0;
        PlayerSpriteNode.FlipH = isMovingLeft;
    }
}
