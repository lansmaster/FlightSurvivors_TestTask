using System.Collections;
using UnityEngine;

public class BossFattyUnit : EnemyUnit
{
    [Header("Fatty prefs")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform[] _firePoints;
    [SerializeField][Range(0.35f, 0.8f)] private float _initialFireCooldown = 0.55f;
    [SerializeField] private float _initialrotationSpeed = 40f;
    [SerializeField] private GameObject _kamikazePrefab;
    [SerializeField][Range(0, 100)] private int _initialKamikazeSpawnChanceInPercent = 1;

    private float _initialKamikazeSpawnChanceNormalized => _initialKamikazeSpawnChanceInPercent / 100f;
    private float _fireCooldown;
    private float _rotationSpeed;

    private void Awake()
    {
        Health = new UnitHealth(this);
        Attack = new UnitAttack(this);
        Damageable = new UnitDamageable(Health);
    }

    private void OnEnable()
    {
        Health.OnHealthValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        Health.OnHealthValueChanged -= OnHealthValueChanged;
    }

    private void Start()
    {
        Movable = new TransformUnitMovable(transform);

        _fireCooldown = _initialFireCooldown;
        _rotationSpeed = _initialrotationSpeed;

        StartCoroutine(BulletSpawnRoutine());
    }

    private void Update()
    {
        Movable.Follow(_target.transform.position, Config.Speed, _pursuitDistance);
        Movable.Rotate(Vector3.back, _rotationSpeed);
    }

    private void OnHealthValueChanged(float valueNormalized)
    {
        if (valueNormalized == 0.05f)
        {
            StartCoroutine(EnableInvulnerabilityRoutine(30f));
        }

        if (valueNormalized <= 0.05f)
        {
            _rotationSpeed = _initialrotationSpeed * 5;
            _fireCooldown = _initialFireCooldown / 1.3f;

            return;
        }
        
        _rotationSpeed = _initialrotationSpeed + ((_initialrotationSpeed - _initialrotationSpeed * valueNormalized) * 2);
    }

    private IEnumerator EnableInvulnerabilityRoutine(float seconds)
    {
        Damageable.IsInvulnerable = true;

        yield return new WaitForSeconds(seconds);

        Damageable.IsInvulnerable = false;
    }

    private IEnumerator BulletSpawnRoutine()
    {
        foreach (Transform firePoint in _firePoints)
        {
            if (Health.HealthPointsNormalized < 0.5f)
            {
                if (Health.HealthPointsNormalized < 0.25f)
                {
                    if (Random.value < _initialKamikazeSpawnChanceNormalized * 2)
                    {
                        KamikazeSpawn(firePoint);

                        continue;
                    }
                }

                if (Random.value < _initialKamikazeSpawnChanceNormalized)
                {
                    KamikazeSpawn(firePoint);

                    continue;
                }
            }

            BulletSpawn(firePoint);
        }

        yield return new WaitForSeconds(_fireCooldown);
        yield return BulletSpawnRoutine();
    }

    private void BulletSpawn(Transform firePoint)
    {
        GameObject bulletGameObject = Instantiate(_bulletPrefab, firePoint.position, Quaternion.LookRotation(Vector3.forward, firePoint.up));

        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.Init(this);

        bulletGameObject.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.Speed, ForceMode2D.Impulse);
    }

    private void KamikazeSpawn(Transform firePoint)
    {
        Instantiate(_kamikazePrefab, firePoint.position, Quaternion.identity).
            GetComponent<KamikadzeUnit>().Init(_target);
    }
}
