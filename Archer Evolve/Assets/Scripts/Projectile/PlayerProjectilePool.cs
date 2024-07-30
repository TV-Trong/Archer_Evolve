using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePool : MonoBehaviour
{
    public static PlayerProjectilePool instance;

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private int maxProjectileNumber;
    private List<GameObject> playerProjectilePool = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < maxProjectileNumber; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.parent = GameObject.FindGameObjectWithTag("Player Projectile").transform;
            projectile.SetActive(false);
            playerProjectilePool.Add(projectile);

        }
    }

    public GameObject PlayerGetPooledProjectile()
    {
        for (int i = 0; i < playerProjectilePool.Count; i++)
        {
            if (!playerProjectilePool[i].activeInHierarchy) return playerProjectilePool[i];
        }
        return null;
    }
}
