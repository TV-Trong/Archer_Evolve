using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDamagePool : MonoBehaviour
{
    public static PopupDamagePool instance;

    [SerializeField] private GameObject popupDamagePrefab;
    [SerializeField] private int maxPopupNumber;
    private Transform popupDamageHolder;
    private List<GameObject> popupObjects = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        popupDamageHolder = transform;
    }

    private void Start()
    {
        for (int i = 0; i < maxPopupNumber; i++)
        {
            GameObject popupDamage = Instantiate(popupDamagePrefab);
            popupDamage.transform.SetParent(popupDamageHolder, true);
            popupDamage.SetActive(false);
            popupObjects.Add(popupDamage);
        }
    }

    public GameObject GetPooledPopupDamageObjects()
    {
        for (int i = 0; i < popupObjects.Count; i++)
        {
            if (!popupObjects[i].activeInHierarchy) return popupObjects[i];
        }
        return null;
    }
}
