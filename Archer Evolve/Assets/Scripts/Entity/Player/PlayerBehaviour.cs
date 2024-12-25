using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IEntity
{
    public static PlayerBehaviour instance;
    [field: SerializeField] public Transform weaponPosition { get; set; }
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
    private WeaponManager weapon;

    [field: Header("Base Stats Variable")]
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    [HideInInspector] public float expPoint;
    [HideInInspector] public int level;
    public float baseExpToLevelUp;
    private float levelUpExp;
    private void Awake()
    {
        if (instance == null) instance = this;
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
        myRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<WeaponManager>();
        baseMoveSpeed = moveSpeed;
        baseHP = healthPoint;
        baseStrength = strength;
        baseAttackSpeed = attackSpeed;
        attackTimer = attackSpeed;
        invincibilityFrame = baseInvincibilityFrame;
        levelUpExp = Mathf.Floor(baseExpToLevelUp + level * 50 * (1 + level * .1f));
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
        if (projectile != null)
        {
            projectile.transform.position = weaponPosition.position;
            projectile.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Not enought arrow pool!");
        }
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
        if (healthPoint <= 0)
        {
            Die();
        }
    }
    public void ShowPopupDamage(float strength)
    {
        GameObject popupDamage = ObjectsPooler.instance.GetPooledObjects(1);
        if (popupDamage != null)
        {   
            popupDamage.transform.position = popupDamagePosition.position;
            PopupDamage damageText = popupDamage.GetComponent<PopupDamage>();
            damageText.SetDamageColor(Color.blue);
            damageText.Setup((int)strength);
            popupDamage.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Not enought damage popup text!");
        }
    }
    public void GainExp(float amount)
    {
        expPoint += amount;
        if (expPoint >= (levelUpExp))
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        baseHP += 10;
        healthPoint = baseHP;

        level++;
        levelUpExp = Mathf.Floor(baseExpToLevelUp + level * 50 * (1 + level * .1f));
        Debug.Log("LEVEL UP! LEVEL: " + level);
    }

    public void Die()
    {
        Debug.Log("Womp Womp!");
    }
    #endregion
}
