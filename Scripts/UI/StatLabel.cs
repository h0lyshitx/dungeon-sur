using DungeonSurvival.Scripts.Resources;
using Godot;

namespace DungeonSurvival.Scripts.UI;

public partial class StatLabel : Label
{
    [Export]
    public StatResource Stat { get; private set; }

    public override void _Ready()
    {
        StatResource.OnStatUpdate += HandleStatUpdate;
        Text = Stat.StatValue.ToString();
    }

    private void HandleStatUpdate()
    {
        Text = Stat.StatValue.ToString();
    }
}
