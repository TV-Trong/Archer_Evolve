using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IEntity
{
    public static PlayerBehaviour instance;
    [field: SerializeField] public Transform shooterPosition { get; set; }
    [field: SerializeField] public Transform popupDamagePosition { get; set; }

    [SerializeField] private InputActionReference movingAction;
    [HideInInspector] public float baseMoveSpeed;
    [HideInInspector] public int baseHP;
    [HideInInspector] public float baseStrength;
    [HideInInspector] public float baseAttackSpeed;
    private Rigidbody2D myRigidbody;
    private float attackTimer;
    private Vector2 moveInput;

    [field: Header("My Stat")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;

        myRigidbody = GetComponent<Rigidbody2D>();

        baseMoveSpeed = moveSpeed;
        baseHP = healthPoint;
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

    #region Methods

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
        projectile.transform.position = shooterPosition.position;
        projectile.SetActive(true);
    }

    public Vector2 GetMousePositionOnScreen()
    {
        Vector2 getMousePosition = Input.mousePosition;
        Vector2 getMouseWorldPosition = Camera.main.ScreenToWorldPoint(getMousePosition);
        return getMouseWorldPosition;
    }

    public void TakeDamge(float strength)
    {
        healthPoint -= (int)strength;
        ShowPopupDamage(strength);
    }
    public void ShowPopupDamage(float strength)
    {
        GameObject popupDamage = PopupDamagePool.instance.GetPooledPopupDamageObjects();
        popupDamage.transform.position = popupDamagePosition.position;
        PopupDamage damageText = popupDamage.GetComponent<PopupDamage>();
        damageText.SetDamageColor(Color.blue);
        damageText.Setup((int)strength);

        popupDamage.SetActive(true);
    }
    #endregion
}
