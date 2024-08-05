using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingWeapon : MonoBehaviour
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
        DistributeStats();
    }

    private void DistributeStats()
    {
        playerBehaviour.attackSpeed = (playerBehaviour.attackSpeed + myWeapon.weaponFireRate) / 2;
        playerBehaviour.moveSpeed = (playerBehaviour.moveSpeed - myWeapon.weight);
        playerBehaviour.strength = (playerBehaviour.strength * (1 + (myWeapon.pullPower * 10 / 100)));

        Debug.LogWarning("After weapon distribute stats");
        Debug.Log($"Speed = {playerBehaviour.moveSpeed}");
        Debug.Log($"Strength = {playerBehaviour.strength}");
        Debug.Log($"Attack Speed = {playerBehaviour.attackSpeed}");
    }
}
