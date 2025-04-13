using DungeonSurvival.Scripts.General;
using DungeonSurvival.Scripts.Resources;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        CharacterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachineNode.SwitchState<DungeonSurvival.Scripts.Characters.Player.PlayerDeathState>(); 
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}
