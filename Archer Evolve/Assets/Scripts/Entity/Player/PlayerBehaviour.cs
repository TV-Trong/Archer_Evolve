using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IEntity
{
    [SerializeField] private InputActionReference movingAction;
    [SerializeField] private Transform shooterTransform;

    [field: Header("My Stat")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public int strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }

    private Rigidbody2D myRigidbody;
    private int maxHP;
    private int baseStrength;
    private float baseAttackSpeed;
    private float attackTimer;
    private Vector2 moveInput;

    [HideInInspector] public float baseMoveSpeed;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        baseMoveSpeed = moveSpeed;
        maxHP = healthPoint;
        baseStrength = strength;
        baseAttackSpeed = attackSpeed;
        attackTimer = attackSpeed;
    }

    private void Update()
    {
        moveInput = movingAction.action.ReadValue<Vector2>();
        InitiateAttack();
    }

    private void FixedUpdate()
    {
        Moving(moveInput);
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    private void InitiateAttack()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            attackTimer = attackSpeed;
            Attack();
        }
    }


    public void Moving(Vector2 moveInput)
    {
        myRigidbody.velocity = moveInput * moveSpeed * Time.deltaTime;
    }

    public void Attack()
    {
        GameObject projectile = PlayerProjectilePool.instance.PlayerGetPooledProjectile();
        projectile.transform.position = shooterTransform.position;
        projectile.SetActive(true);
    }

    public Vector2 GetMousePositionOnScreen()
    {
        Vector2 getMousePosition = Input.mousePosition;
        Vector2 getMouseWorldPosition = Camera.main.ScreenToWorldPoint(getMousePosition);
        return getMouseWorldPosition;
    }

    public void TakeDamge(int strength)
    {
        healthPoint -= strength;
    }
}
