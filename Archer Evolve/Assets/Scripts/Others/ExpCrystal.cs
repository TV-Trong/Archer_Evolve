using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrystal : MonoBehaviour
{
    private float expContaining;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Magnet"))
        {
            PlayerBehaviour.instance.GainExp(expContaining);
            gameObject.SetActive(false);
        }
    }

    public void StoreExp(float amount)
    {
        expContaining = amount;
    }
}
