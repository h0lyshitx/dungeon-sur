using Godot;
using RPGDEMO.Scripts.Resources;

namespace RPGDEMO.Scripts.Characters;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] _stats;
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D PlayerSpriteNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }
    


    public Vector2 Direction = new();

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

    public override void _Ready()
    {
        HurtboxNode.AreaEntered += HandleHurtboxAreaEntered;
    }

    private void HandleHurtboxAreaEntered(Area3D area)
    {
        GD.Print("Hurt");
    }
}
