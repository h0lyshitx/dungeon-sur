using Godot;
using RPGDEMO.Scripts.General;

namespace RPGDEMO.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}
