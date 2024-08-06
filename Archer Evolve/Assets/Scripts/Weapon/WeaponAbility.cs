using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAbility : MonoBehaviour
{
    public void ChoseWeaponAbility(string abilityCode)
    {
        switch (abilityCode)
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


    private void B00()
    {
        Debug.Log("This weapon has no Ability!");
    }

    private void B01()
    {
        Debug.Log("Activate Ability 01");
    }
}
