using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyPatch : MonoBehaviour
{
    public List<GameObject> dummies = new List<GameObject>();
    private void Start()
    {
        foreach (GameObject obj in dummies)
        {   
            Vector3 initiatePos = obj.transform.position;
            obj.SetActive(true);
            obj.transform.position = initiatePos;
        }
    }
}
