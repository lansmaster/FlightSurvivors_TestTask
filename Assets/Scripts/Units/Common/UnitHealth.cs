using System;

public class UnitHealth
{
    private Unit _unit;

    private float _health;

    public event Action<float> OnHealthValueChanged;
    public event Action OnUnitDefeated;

    public float MaxHealthPoints { get; private set; }
    public float HealthPointsNormalized => _health / MaxHealthPoints;
    public float HealthPoints
    {
        get => _health;
        set
        {
            _health = Math.Clamp(value, 0f, MaxHealthPoints);
            OnHealthValueChanged?.Invoke(HealthPointsNormalized);

            if(_health == 0f)
            {
                OnUnitDefeated?.Invoke();
            }
        }
    }

    public UnitHealth(Unit unit)
    {
        _unit = unit;
        Init();
    }

    private void Init()
    {
        MaxHealthPoints = _unit.Config.Health;
        HealthPoints = MaxHealthPoints;
        OnHealthValueChanged?.Invoke(HealthPointsNormalized);
    }
}
