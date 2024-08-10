using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapons_SO : ScriptableObject
{
    public string weaponName;
    public string weaponType;
    public string weaponID;
    public float fireRate;
    public float pullForce;
    public float weight;
    public Sprite weaponSprite;
    public string discription;
    public GameObject weaponAbilityObject;

    public void ActivateWeaponAbility()
    {
        WeaponAbility weaponAbility = weaponAbilityObject.GetComponent<WeaponAbility>();
        weaponAbility.ChoseWeaponAbility(weaponID, weaponType);
    }
}
