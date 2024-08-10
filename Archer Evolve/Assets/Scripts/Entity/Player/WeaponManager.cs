using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public Weapons_SO myWeapon;
    private PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        SpriteRenderer weaponSprite = gameObject.GetComponent<SpriteRenderer>();
        weaponSprite.sprite = myWeapon.weaponSprite;
    }

    private void Start()
    {
        playerBehaviour = PlayerBehaviour.instance;

        float fireRate = myWeapon.fireRate;
        float pullPower = myWeapon.pullForce;
        float weight = myWeapon.weight;
        DistributeStats(fireRate, pullPower, weight);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            myWeapon.ActivateWeaponAbility();
            Debug.Log(myWeapon.pullForce);
        }
    }

    public void UpdateWeaponStats(float fireRate = 0f, float pullForce = 0, float weight = 0f)
    {
        myWeapon.fireRate += fireRate;
        myWeapon.pullForce += pullForce;
        myWeapon.weight += weight;
        DistributeStats(fireRate: myWeapon.fireRate, pullForce: myWeapon.pullForce, weight: myWeapon.weight);
    }

    public void DistributeStats(float fireRate = 0f, float pullForce = 0, float weight = 0f)
    {
        if (fireRate != 0f) playerBehaviour.attackSpeed = (playerBehaviour.attackSpeed + myWeapon.fireRate) / 2;
        if (pullForce != 0f) playerBehaviour.moveSpeed = (playerBehaviour.moveSpeed - myWeapon.weight);
        if (weight != 0f) playerBehaviour.strength = (playerBehaviour.strength * (myWeapon.pullForce * 10 / 100));
    }
}
