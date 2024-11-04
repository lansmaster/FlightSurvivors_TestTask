using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _lifetime = 8f;

    private Unit _shooter;

    public float Speed
    {
        get => _speed;
        private set { _speed = value; }
    }

    public void Init(Unit shooter)
    {
        _shooter = shooter;

        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            Unit collisionUnit = collision.GetComponent<Unit>();

            if (_shooter == collisionUnit)
            {
                return;
            }

            if (_shooter.CompareTag("Enemy") && collision.CompareTag("Enemy"))
            {
                return;
            }

            _shooter.Attack.PerformAttack(collisionUnit.Damageable);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
