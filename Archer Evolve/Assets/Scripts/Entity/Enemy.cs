using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    [field: Header("My Stat")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    [field: SerializeField] public GameObject popupDamage { get; set; }
    [field: SerializeField] public Transform popupDamagePosition { get; set; }

    private Rigidbody2D myRigidbody;
    private PlayerBehaviour playerBehaviour;
    private CircleCollider2D playerHitbox;
    private float baseMoveSpeed;
    private int maxHP;
    private float baseStrength;
    private float baseAttackSpeed;
    private float attackTimer;
    private bool isPlayerInAttackRange = false;
    private Coroutine myCoroutine;
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

    private void KnockbackCalculation()
    {
        Vector2 playerPosition = playerBehaviour.transform.position;
        Vector2 myPosition = transform.position;
        Vector2 knockbackDirection = (myPosition - playerPosition).normalized;
        myRigidbody.AddForce(knockbackDirection * 2f, ForceMode2D.Impulse);
        myCoroutine = StartCoroutine(KnockbackTime(knockbackValue));
    }

    private IEnumerator KnockbackTime(float time)
    {
        yield return new WaitForSeconds(time);
        myRigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerHitbox)
        {
            isPlayerInAttackRange = false;
        }
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
        GameObject damageText = Instantiate(popupDamage, popupDamagePosition.position, Quaternion.identity);
        DamagePopup damagePopup = damageText.GetComponent<DamagePopup>();
        damagePopup.Setup((int)strength);
    }
}
