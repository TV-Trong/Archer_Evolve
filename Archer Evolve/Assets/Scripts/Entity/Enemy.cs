using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    [field: Header("My Stat")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public int strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }

    private Rigidbody2D myRigidbody;
    private GameObject playerGameObject;
    private PlayerBehaviour playerBehaviour;
    private float baseMoveSpeed;
    private int maxHP;
    private int baseStrength;
    private float baseAttackSpeed;
    private float attackTimer;
    private bool isPlayerInAttackRange = false;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerBehaviour = playerGameObject.GetComponent<PlayerBehaviour>();

        baseMoveSpeed = moveSpeed;
        maxHP = healthPoint;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInAttackRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
        Vector2 playerPosition = playerGameObject.transform.position;
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

    public void TakeDamge(int strength)
    {
        healthPoint -= strength;
    }
}
