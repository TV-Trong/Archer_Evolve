using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
   
    [field: SerializeField] public Transform popupDamagePosition { get; set; }
    [field: SerializeField] public Transform weaponPosition { get; set; }

    private Rigidbody2D myRigidbody;
    private PlayerBehaviour playerInstance;
    private CircleCollider2D playerHitbox;
    private Coroutine myCoroutine;
    private float baseMoveSpeed;
    private float baseStrength;
    private float baseAttackSpeed;
    private float attackTimer;
    private int baseHealth;
    private bool isPlayerInAttackRange = false;

    [field: Header("Base Stats Variable")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }

    [SerializeField] private float knockbackValue;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float baseExpYield;
    [SerializeField] private int enemyLevel;
    private float extraRadius = 2f;

    private void Awake()
    {
        playerInstance = PlayerBehaviour.instance;
        playerHitbox = GameObject.FindWithTag("PlayerHitbox").GetComponent<CircleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        baseMoveSpeed = moveSpeed;
        baseHealth = healthPoint;
        baseStrength = strength;
        baseAttackSpeed = attackSpeed;
        attackTimer = 0;
    }

    private void Update()
    {
        if (isPlayerInAttackRange) InitiateAttack();
        else if (!isPlayerInAttackRange && attackTimer > 0.1f) attackTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void OnEnable()
    {
        RepositionEnemyWhenEnable();
        healthPoint = baseHealth;
        moveSpeed = baseMoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerHitbox)
        {
            isPlayerInAttackRange = true;
        }

        if (collision.CompareTag("PlayerProjectile"))
        {
            if (myCoroutine != null) StopCoroutine(myCoroutine);
            KnockbackCalculation();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerHitbox)
        {
            isPlayerInAttackRange = false;
        }
    }

    #region Methods

    private void RepositionEnemyWhenEnable()
    {
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);
        float randomDistance = Random.Range(spawnRadius, spawnRadius + extraRadius);
        transform.position = new Vector3(
            playerInstance.transform.position.x + Mathf.Cos(randomAngle) * randomDistance,
            playerInstance.transform.position.y + Mathf.Sin(randomAngle) * randomDistance,
            0f);
    }

    public void FlipSprite()
    {
        if (playerInstance != null)
            if (playerInstance.transform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
    }
    private void KnockbackCalculation()
    {
        if (knockbackValue != 0) myCoroutine = StartCoroutine(InitiateKnockback(knockbackValue));
    }

    private IEnumerator InitiateKnockback(float time)
    {
        SetupKnockback();
        yield return new WaitForSeconds(time);
        ResetVelocity();
    }

    private void SetupKnockback()
    {
        Vector2 playerPosition = playerInstance.transform.position;
        Vector2 myPosition = transform.position;
        Vector2 knockbackDirection = (myPosition - playerPosition).normalized;
        myRigidbody.AddForce(knockbackDirection * 2f, ForceMode2D.Impulse);
    }

    private void InitiateAttack()
    {
        if (attackTimer > 0.1f)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            attackTimer = attackSpeed;
            Attack();
        }
    }

    private void FollowPlayer()
    {
        Vector2 playerPosition = playerInstance.transform.position;
        Vector2 moveInput = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
        Moving(moveInput);
    }

    public void Attack()
    {
        playerInstance.TakeDamge(strength);
    }

    public void Moving(Vector2 moveInput)
    {
        transform.position = moveInput;
    }
    public void EnemyTakeDamage(float strength, bool isCrit)
    {
        healthPoint -= (int)strength;
        ShowPopupDamage(strength, isCrit);
        if (healthPoint <= 0)
        {
            moveSpeed = 0f;
            Die();
        }
    }

    public void ShowPopupDamage(float strength, bool isCrit)
    {
        GameObject popupDamage = ObjectsPooler.instance.GetPooledObjects(1);
        if (popupDamage != null)
        {
            popupDamage.transform.position = popupDamagePosition.position;
            PopupDamage damageText = popupDamage.GetComponent<PopupDamage>();
            damageText.Setup((int)strength);
            if (isCrit) 
                damageText.SetCriticalDamage();
            else
                damageText.SetDamageColor(Color.red);
            popupDamage.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Not enought damage popup text!");
        }
    }

    public void Die()
    {
        GameObject crystal = ObjectsPooler.instance.GetPooledObjects(4);
        if (crystal != null)
        {
            crystal.transform.position = gameObject.transform.position;
            ExpCrystal expCrystal = crystal.GetComponent<ExpCrystal>();
            expCrystal.StoreExp(baseExpYield);
            crystal.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Not enought exp crystal pool!");
        }
    }

    public void ResetVelocity()
    {
        myRigidbody.velocity = Vector2.zero;
    }
    #endregion
}
