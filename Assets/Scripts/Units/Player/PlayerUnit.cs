using System.Collections;
using UnityEngine;

public class PlayerUnit : Unit
{
    [Header("Player prefs")] 
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField][Range(0.02f, 0.2f)] private float _fireCooldown = 0.1f;

    private void Awake()
    {
        Health = new UnitHealth(this);
        Attack = new UnitAttack(this);
        Damageable = new UnitDamageable(Health);
    }

    private void Start()
    {
        Movable = new TransformUnitMovable(transform);

        StartCoroutine(BulletSpawnRoutine());
    }

    private void OnEnable()
    {
        Health.OnUnitDefeated += OnPlayerDefeated;
    }

    private void OnDisable()
    {
        Health.OnUnitDefeated -= OnPlayerDefeated;
    }

    private void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Movable.Move(direction, Config.Speed);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Movable.RotateByMouse(mousePos);
    }

    private void OnPlayerDefeated()
    {
        //Calling an explosion animation, for example
    }

    private IEnumerator BulletSpawnRoutine()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 starshipPos = transform.position;
        Vector2 direction = mousePos - starshipPos;

        transform.up = direction;


        GameObject bulletGameObject = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.LookRotation(Vector3.forward, direction));

        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.Init(this);

        bulletGameObject.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * bullet.Speed, ForceMode2D.Impulse);


        yield return new WaitForSeconds(_fireCooldown);
        yield return BulletSpawnRoutine();
    }
}