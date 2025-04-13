using System;
using Godot;

namespace DungeonSurvival.Scripts.Resources;

[GlobalClass]
public partial class StatResource : Resource
{
    public event Action OnZero;
    public static event Action OnStatUpdate;

    [Export]
    public Stat StatType { get; private set; }

    private float _statValue;

    [Export]
    public float StatValue
    {
        get => _statValue;
        set
        {
            _statValue = Mathf.Clamp(value, 0, Mathf.Inf);
            OnStatUpdate?.Invoke();

            if (_statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }
}
