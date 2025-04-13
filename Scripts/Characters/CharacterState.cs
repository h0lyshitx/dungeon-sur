using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters;

public abstract partial class CharacterState : Node
{
    protected Character CharacterNode;

    public override void _Ready()
    {
        CharacterNode = GetOwner<Character>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            ExitState();
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    protected virtual void EnterState() { }

    protected virtual void ExitState()
    {
    }
    
}
