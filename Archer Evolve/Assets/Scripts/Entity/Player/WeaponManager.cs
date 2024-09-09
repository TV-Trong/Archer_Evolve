using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public Weapons_SO myWeapon;
    private PlayerBehaviour playerBehaviour;
    private WeaponAbility weaponAbility;

    private void Awake()
    {
        weaponAbility = GetComponent<WeaponAbility>();
        SetupWeaponSprite();
        myWeapon.SetupWeapon();
    }

    private void Start()
    {
        playerBehaviour = PlayerBehaviour.instance;
        SetupWeaponStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            weaponAbility.ActivateWeaponAbility(myWeapon.weaponID, myWeapon.weaponType);
            Debug.Log(myWeapon.pullForce);
        }
    }

    #region Methods
    private void SetupWeaponSprite()
    {
        SpriteRenderer weaponSprite = GetComponent<SpriteRenderer>();
        weaponSprite.sprite = myWeapon.weaponSprite;
    }

    private void SetupWeaponStats()
    {
        float fireRate = myWeapon.fireRate;
        float pullPower = myWeapon.pullForce;
        float weight = myWeapon.weight;
        DistributeStats(fireRate, pullPower, weight);
    }

    public void UpdateWeaponStats(float fireRate = 0f, float pullForce = 0, float weight = 0f)
    {
        if (fireRate > 0.1f)
        {
            myWeapon.fireRate += fireRate;
            DistributeStats(fireRate: myWeapon.fireRate);
        }
        if (pullForce > 0.1f)
        {
            myWeapon.pullForce += pullForce;
            DistributeStats(pullForce: myWeapon.pullForce);
        }
        if (weight > 0.1f)
        {
            myWeapon.weight += weight;
            DistributeStats(weight: myWeapon.weight);
        }
        
    }

    public void DistributeStats(float fireRate = 0f, float pullForce = 0, float weight = 0f)
    {
        if (fireRate > 0.1f) playerBehaviour.attackSpeed = (playerBehaviour.baseAttackSpeed + myWeapon.fireRate) / 2;
        if (weight > 0.1f) playerBehaviour.moveSpeed = (playerBehaviour.baseMoveSpeed - myWeapon.weight);
        if (pullForce > 0.1f) playerBehaviour.strength = (playerBehaviour.baseStrength * (myWeapon.pullForce * 10 / 100));
    } 
    #endregion
}
