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
    public float weaponFireRate;
    public float pullPower;
    public float weight;
    public Sprite weaponSprite;
    public string discription;
    public GameObject weaponAbilityObject;
    private WeaponAbility weaponAbility;

    public void ActivateWeaponAbility()
    {
        char[] weaponNameChars = weaponType.ToCharArray();
        char firstChar = weaponNameChars[0];
        string abilityCode = firstChar + weaponID.ToString();
        
        WeaponAbility weaponAbility = weaponAbilityObject.GetComponent<WeaponAbility>();
        weaponAbility.ChoseWeaponAbility(abilityCode);
    }
}
