using DUNSUR.Scripts.General;
using Godot;

namespace DUNSUR.Scripts.Characters.Enemy;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_DEATH);
        CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animname)
    {
        CharacterNode.QueueFree();
        // solve the attack timeout problem where the enemy will attack the player even after death, right before they disappear
    }
}