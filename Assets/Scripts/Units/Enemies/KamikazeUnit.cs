using System.Collections;
using UnityEngine;

public class KamikadzeUnit : EnemyUnit
{
    [Header("Kamikadze prefs")]
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Sprite _defaultKamikadzeSprite;
    [SerializeField] private Sprite _redKamikadzeSprite;
    [SerializeField] private float _interactionDistance = 0.8f;
    [SerializeField] private float _rotationSpeed = 30f;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Health = new UnitHealth(this);
        Attack = new UnitAttack(this);
        Damageable = new UnitDamageable(Health);
    }

    private void OnEnable()
    {
        Health.OnUnitDefeated += OnKamikadzeDefeated;
    }

    private void OnDisable()
    {
        Health.OnUnitDefeated -= OnKamikadzeDefeated;
    }

    private void Start()
    {
        Movable = new TransformUnitMovable(transform);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(SpriteChangeRoutine());
    }

    private void Update()
    {
        Movable.Follow(_target.transform.position, Config.Speed, _pursuitDistance);
        Movable.Rotate(Vector3.back, _rotationSpeed);

        DetectPlayer();
    }

    public void Init(Unit target) // for Instantiate (rough variant)
    {
        _target = target;
    }

    private void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, _interactionDistance);

        if (playerCollider.CompareTag("Player") && playerCollider.TryGetComponent(out PlayerUnit playerUnit))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _interactionDistance);

            foreach(Collider2D collider in colliders)
            {
                // for area damage
            }

            Attack.PerformAttack(playerUnit.Damageable);

            OnKamikadzeDefeated();
        }
    }

    private void OnKamikadzeDefeated()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity).
            GetComponent<ParticleSystem>().Play();

        Destroy(gameObject);
    }

    private IEnumerator SpriteChangeRoutine()
    {
        _spriteRenderer.sprite = _defaultKamikadzeSprite;

        yield return new WaitForSeconds(0.5f);

        _spriteRenderer.sprite = _redKamikadzeSprite;

        yield return new WaitForSeconds(0.5f);

        yield return SpriteChangeRoutine();
    }
}
