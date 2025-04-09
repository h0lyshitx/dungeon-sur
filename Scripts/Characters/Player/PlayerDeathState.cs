using DUNSUR.Scripts.General;
using Godot;

namespace DUNSUR.Scripts.Characters.Player;

public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_DEATH, -1d, 1.5f);
        CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animname)
    {
        CharacterNode.QueueFree();
    }

}