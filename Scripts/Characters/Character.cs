using Godot;
using RPGDEMO.Scripts.Resources;
using System.Linq;

namespace RPGDEMO.Scripts.Characters;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] _stats;
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D PlayerSpriteNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public CollisionShape3D HurtboxCollisionNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }
    [Export] public CollisionShape3D HitboxCollisionNode { get; private set; }

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

    protected void HandleHurtboxAreaEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        Character character = area.GetOwner<Character>();
        
        health.StatValue -= character.GetStatResource(Stat.Strength).StatValue;
        GD.Print($"{health.StatValue} HP");
    }

    private StatResource GetStatResource(Stat stat)
    {
        return _stats.Where((element) => element.StatType == stat).FirstOrDefault();

        // Does the same thing as the below code
        // foreach (StatResource element in _stats)
        // {
        //     if (element.StatType == stat)
        //     {
        //         result = element;
        //         break;
        //     }
        // }
        // return result;
    }
}
