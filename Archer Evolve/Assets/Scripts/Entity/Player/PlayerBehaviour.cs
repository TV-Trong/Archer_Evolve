using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IEntity
{
    [SerializeField] 
    private InputActionReference movingAction;
    private Rigidbody2D myRigidbody;

    [SerializeField] 
    private float moveSpeed = 300f;
    private Vector2 moveInput;

    [SerializeField]
    private float initAttackTimer;
    private float attackTimer = 1f;
    public Transform shooterTransform;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        attackTimer = initAttackTimer;
    }

    private void Update()
    {
        moveInput = movingAction.action.ReadValue<Vector2>();

        InitiateAttack();
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
            attackTimer = initAttackTimer;
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Moving(moveInput);
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

    public static Vector2 GetMousePositionOnScreen()
    {
        Vector2 getMousePosition = Input.mousePosition;
        Vector2 getMouseWorldPosition = Camera.main.ScreenToWorldPoint(getMousePosition);
        return getMouseWorldPosition;
    }
}
