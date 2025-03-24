using Godot;

namespace RPGDEMO.Scripts.Characters.Enemy;

public abstract partial class EnemyState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        CharacterNode = GetOwner<EnemyKnight>();
    }
}
