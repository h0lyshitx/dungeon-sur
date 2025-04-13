using Godot;

namespace DungeonSurvival.Scripts.General;

public partial class Camera : Camera3D
{
    [Export]
    public Node Target;

    [Export]
    public Vector3 PositionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }

    private void HandleStartGame()
    {
        Reparent(Target);
        Position = PositionFromTarget;
    }
}
