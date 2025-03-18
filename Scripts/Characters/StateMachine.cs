using Godot;

namespace RPGDEMO.Scripts.Characters;

public partial class StateMachine : Node
{
    [Export]
    private Node _currentState;

    [Export]
    private Node[] _states;

    public override void _Ready()
    {
        _currentState.Notification(5001);
    }

    public void SwitchState<T>()
    {
        Node newState = null;

        foreach (Node state in _states)
        {
            if (state is T)
            {
                newState = state;
            }
        }
        if (newState == null)
            return;

        _currentState.Notification(5002); // send notification to disable PhysicsProcess
        _currentState = newState;
        _currentState.Notification(5001); // send notification to enable PhysicsProcess
    }
}
