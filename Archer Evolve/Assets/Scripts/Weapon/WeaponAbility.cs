using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAbility : MonoBehaviour
{
    public void ChoseWeaponAbility(string weaponID, string weaponType)
    {
        if (weaponType.Equals("Bow"))
        {
            switch (weaponID)
            {
                case "B00":
                    B00();
                    break;
                case "B01":
                    B01();
                    break;
                default:
                    Debug.LogWarning("Ability not Found!");
                    break;
            }
        }
        else if (weaponType.Equals("Crossbow"))
        {

        }
        else Debug.LogWarning("Weapon is invalid!");
    }


    private void B00()
    {
        Debug.Log("This weapon has no Ability!");
    }

    private void B01()
    {
        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        weaponManager.UpdateWeaponStats(pullForce: 5);
    }
}
