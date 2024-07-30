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
    private float attackTimer = 1f;
    private float initAttackTimer;
    public GameObject projectile;
    public Transform originPosition;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        initAttackTimer = attackTimer;
    }

    private void Update()
    {
        moveInput = movingAction.action.ReadValue<Vector2>();

        if (attackTimer > 0f )
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
        Instantiate(projectile, originPosition.position, Quaternion.identity);
    }
}
