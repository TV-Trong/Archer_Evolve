using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IEntity
{
    public static PlayerBehaviour instance;
    [field: SerializeField] public Transform shooterPosition { get; set; }
    [field: SerializeField] public Transform popupDamagePosition { get; set; }

    [SerializeField] private InputActionReference movingAction;
    [SerializeField] private float baseInvincibilityFrame = 0.5f;
    [HideInInspector] public float baseMoveSpeed;
    [HideInInspector] public int baseHP;
    [HideInInspector] public float baseStrength;
    [HideInInspector] public float baseAttackSpeed;
    private Rigidbody2D myRigidbody;
    private float attackTimer;
    private Vector2 moveInput;
    private bool isInvincible;
    private float invincibilityFrame;

    [field: Header("My Stat")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        myRigidbody = GetComponent<Rigidbody2D>();
        SetupBaseStats();
    }

    private void Update()
    {
        moveInput = movingAction.action.ReadValue<Vector2>();
        InitiateAttack();

        if (isInvincible) invincibilityFrame -= Time.deltaTime;
        if (invincibilityFrame <= 0f)
        {
            invincibilityFrame = baseInvincibilityFrame;
            isInvincible = false;
        }
    }

    private void FixedUpdate()
    {
        Moving(moveInput);
    }

    #region Methods
    private void SetupBaseStats()
    {
        baseMoveSpeed = moveSpeed;
        baseHP = healthPoint;
        baseStrength = strength;
        baseAttackSpeed = attackSpeed;
        attackTimer = attackSpeed;
        invincibilityFrame = baseInvincibilityFrame;
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
        GameObject projectile = ObjectsPooler.instance.GetPooledObjects(0);
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
        if (!isInvincible)
        {
            healthPoint -= (int)strength;
            ShowPopupDamage(strength);
            isInvincible = true;
        }
    }
    public void ShowPopupDamage(float strength)
    {
        GameObject popupDamage = ObjectsPooler.instance.GetPooledObjects(1);
        popupDamage.transform.position = popupDamagePosition.position;
        PopupDamage damageText = popupDamage.GetComponent<PopupDamage>();
        damageText.SetDamageColor(Color.blue);
        damageText.Setup((int)strength);
        popupDamage.SetActive(true);
    }
    public void Destroyed()
    {
        Debug.Log("Womp Womp!");
    }
    #endregion
}
