using System;

public class UnitAttack
{
    private Unit _unit;

    private float _damage;

    public event Action<float> OnDamageValueChanged;

    public float Damage
    {
        get => _damage;
        set
        {
            _damage = Math.Clamp(value, 0f, float.MaxValue);
            OnDamageValueChanged?.Invoke(_damage);
        }
    }

    public UnitAttack(Unit unit)
    {
        _unit = unit;
        Init();
    }

    private void Init()
    {
        Damage = _unit.Config.Damage;
    }

    public void PerformAttack(UnitDamageable recipientUnitDamageable)
    {
        recipientUnitDamageable.ApplyDamage(Damage);
    }
}
