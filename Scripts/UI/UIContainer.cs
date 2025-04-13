using System;
using DungeonSurvival.Scripts.UI;
using Godot;

public partial class UIContainer : Container
{
    [Export]
    public ContainerType Container { get; private set; }

    [Export]
    public Button ButtonNode { get; private set; }
}
