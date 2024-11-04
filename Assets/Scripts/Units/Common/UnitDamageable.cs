using System;

public class UnitDamageable
{
    private UnitHealth _unitHealth;
    private bool _isInvulnerable;

    public bool IsInvulnerable
    {
        get { return _isInvulnerable; } 
        set { _isInvulnerable = value;}
    }

    public UnitDamageable(UnitHealth unitHealth)
    {
        _unitHealth = unitHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }

        var totalDamage = ProcessDamage(damage);

        if (totalDamage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalDamage));
        }

        _unitHealth.HealthPoints -= totalDamage;
    }

    protected virtual float ProcessDamage(float damage)
    {
        if (_isInvulnerable)
        {
            return 0;
        }

        return damage;
    }
}
