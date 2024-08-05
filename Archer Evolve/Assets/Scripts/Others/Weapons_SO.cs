using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapons_SO : ScriptableObject
{
    public string weaponName;
    public float weaponFireRate;
    public float pullPower;
    public float weight;
    public Sprite weaponSprite;
}
