using System.Linq;
using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters;

public partial class StateMachine : Node
{
    [Export]
    private Node _currentState;

    [Export]
    private Node[] _states;

    public override void _Ready()
    {
        _currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        Node newState = _states.Where((state) => state is T).FirstOrDefault();

        if (newState == null)
        { return;}

        if (_currentState is T)  { return;}
        
        _currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE); // send notification to disable something
        _currentState = newState;
        _currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE); // send notification to enable something
    }
}
