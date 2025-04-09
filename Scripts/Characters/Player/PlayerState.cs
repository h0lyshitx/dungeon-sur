using DUNSUR.Scripts.General;
using DUNSUR.Scripts.Resources;
using Godot;

namespace DUNSUR.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        CharacterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachineNode.SwitchState<PlayerDeathState>(); 
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}
