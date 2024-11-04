using UnityEngine;

public class EnemyUnit : Unit
{
    [Header("EnemyUnit prefs")]
    [SerializeField] protected Unit _target;
    [SerializeField] protected float _pursuitDistance;

    private void Awake()
    {
        Health = new UnitHealth(this);
        Attack = new UnitAttack(this);
        Damageable = new UnitDamageable(Health);
    }

    private void Start()
    {
        Movable = new TransformUnitMovable(transform);
    }
}