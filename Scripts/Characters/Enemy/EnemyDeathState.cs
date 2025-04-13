using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Enemy;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_DEATH);
        CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animname)
    {
        CharacterNode.PathNode.QueueFree();
    }
}
