using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Player;

public partial class PlayerAttackState : PlayerState
{
    [Export]
    private Timer _comboTimerNode;
    private int _comboCounter = 1;
    private int _maxComboCount = 2;

    protected override void EnterState()
    {
        CharacterNode.Velocity = new Vector3(0, CharacterNode.Velocity.Y, 0);
        CharacterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK + _comboCounter);
        CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        _comboTimerNode.Start();
    }

    public override void _Ready()
    {
        base._Ready();
        _comboTimerNode.Timeout += () => _comboCounter = 1;
    }

    private void HandleAnimationFinished(StringName animname)
    {
        _comboCounter++;
        _comboCounter = Mathf.Wrap(_comboCounter, 1, _maxComboCount + 1);
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void PerformAttack()
    {
        Vector3 newPosition = CharacterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        float distanceMultiplier = 0.75f;

        CharacterNode.HitboxNode.Position = newPosition * distanceMultiplier;
    }
}
