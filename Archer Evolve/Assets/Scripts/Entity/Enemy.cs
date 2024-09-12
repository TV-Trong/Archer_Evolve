using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
   
    [field: SerializeField] public Transform popupDamagePosition { get; set; }
    [field: SerializeField] public Transform shooterPosition { get; set; }

    private Rigidbody2D myRigidbody;
    private PlayerBehaviour playerBehaviour;
    private CircleCollider2D playerHitbox;
    private Coroutine myCoroutine;
    private float baseMoveSpeed;
    private float baseStrength;
    private float baseAttackSpeed;
    private float attackTimer;
    private int maxHP;
    private bool isPlayerInAttackRange = false;

    [field: Header("My Stat")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }

    [SerializeField] private float knockbackValue;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        baseMoveSpeed = moveSpeed;
        maxHP = healthPoint;
        baseStrength = strength;
        baseAttackSpeed = attackSpeed;
        attackTimer = 0;
    }

    private void Start()
    {
        playerBehaviour = PlayerBehaviour.instance;
        playerHitbox = PlayerBehaviour.instance.GetComponentInChildren<CircleCollider2D>();
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

    public void FlipSprite()
    {
        if (playerBehaviour.transform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
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
        myRigidbody.velocity = Vector2.zero;
    }

    private void SetupKnockback()
    {
        Vector2 playerPosition = playerBehaviour.transform.position;
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
        Vector2 playerPosition = playerBehaviour.transform.position;
        Vector2 moveInput = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
        Moving(moveInput);
    }

    public void Attack()
    {
        playerBehaviour.TakeDamge(strength);
    }

    public void Moving(Vector2 moveInput)
    {
        transform.position = moveInput;
    }

    public void TakeDamge(float strength)
    {
        healthPoint -= (int)strength;
        ShowPopupDamage(strength);
    }

    public void ShowPopupDamage(float strength)
    {
        GameObject popupDamage = ObjectsPooler.instance.GetPooledObjects(1);
        popupDamage.transform.position = popupDamagePosition.position;
        PopupDamage damageText = popupDamage.GetComponent<PopupDamage>();
        damageText.Setup((int)strength);
        damageText.SetDamageColor(Color.red);

        popupDamage.SetActive(true);
    } 
    #endregion
}
