using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;
    [field: SerializeField] public Transform weaponPosition { get; set; }
    [field: SerializeField] public Transform popupDamagePosition { get; set; }

    [SerializeField] private InputActionReference movingAction;
    [SerializeField] private float baseInvincibilityFrame = 0.5f;
    public float baseMoveSpeed { get; set; }
    public int baseHP { get; set; }
    public float baseStrength { get; set; }
    public float baseAttackSpeed { get; set; }
    private Rigidbody2D myRigidbody;
    private float attackTimer;
    private Vector2 moveInput;
    private bool isInvincible;
    private float invincibilityFrame;
    private WeaponManager weapon;
    private HUD_Update uiUpdater;
    private LevelUp levelUpScript;

    [field: Header("Base Stats Variable")]
    [field:SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public int healthPoint { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    [Tooltip("Critical chance calculate in percentage")]
    [field: SerializeField] public float critChance { get; set; }
    [Tooltip("Final damage *= critical damage")]
    [field: SerializeField] public float critDamage { get; set; }
    [HideInInspector] public float expPoint;
    [HideInInspector] public int level;
    public float baseExpToLevelUp;
    private float levelUpExp;
    private void Awake()
    {
        SetupVariables();
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
    private void SetupVariables()
    {
        if (instance == null) instance = this;
        myRigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<WeaponManager>();
        baseMoveSpeed = moveSpeed;
        baseHP = healthPoint;
        baseStrength = strength;
        baseAttackSpeed = attackSpeed;
        attackTimer = attackSpeed;
        invincibilityFrame = baseInvincibilityFrame;
        levelUpExp = Mathf.Floor(baseExpToLevelUp + level * 50 * (1 + level * .1f));
        uiUpdater = GetComponentInChildren<HUD_Update>();
        uiUpdater.InitialHUD(baseHP, healthPoint, levelUpExp, expPoint, level);
        levelUpScript = FindObjectOfType<LevelUp>();
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
            Arrow arrow = projectile.GetComponent<Arrow>();
            arrow.isAttackCrit = CalculateCritChance();
            projectile.transform.position = weaponPosition.position;
            projectile.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Not enought arrow pool!");
        }
    }
    public bool CalculateCritChance()
    {
        float randomCrit = Random.Range(0f, 100f);
        return (randomCrit <= critChance);
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
            uiUpdater.UpdateHPSlider(healthPoint);
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
        uiUpdater.UpdateEXPSlider(expPoint);
        if (expPoint >= (levelUpExp))
        {
            LevelUp(expPoint - levelUpExp);
        }
    }

    private void LevelUp(float excessExp)
    {
        baseHP += 10;
        healthPoint = baseHP;
        uiUpdater.SetUpSlider(baseHP: baseHP);
        uiUpdater.UpdateHPSlider(healthPoint);

        level++;
        uiUpdater.UpdateLevelText(level);
        expPoint = 0 + excessExp;
        levelUpExp = Mathf.Floor(baseExpToLevelUp + level * 50 * (1 + level * .1f));
        uiUpdater.SetUpSlider(baseEXP: levelUpExp);
        uiUpdater.UpdateEXPSlider(expPoint);
        levelUpScript.OpenAttributeCanvas();
        Debug.Log("LEVEL UP! LEVEL: " + level);
    }

    public void Die()
    {
        Debug.Log("Womp Womp!");
    }
    #endregion
}
