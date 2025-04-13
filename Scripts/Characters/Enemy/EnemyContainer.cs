using DungeonSurvival.Scripts.General;
using Godot;

namespace DungeonSurvival.Scripts.Characters.Enemy;

public partial class EnemyContainer : Node3D
{
    public override void _Ready()
    {
        int totalEnemies = GetChildCount();
        GameEvents.RaiseNewEnemyCount(totalEnemies);
        ChildExitingTree += HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        int totalEnemies = GetChildCount() - 1;
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        if (totalEnemies == 0)
        {
            GameEvents.RaiseVictory();
        }
    }
}
