using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitConfig _unitConfig;

    public UnitConfig Config => _unitConfig;
    public UnitHealth Health { get; set; }
    public UnitAttack Attack { get; set; }
    public UnitDamageable Damageable { get; set; }
    public UnitMovable Movable { get; set; }
}
