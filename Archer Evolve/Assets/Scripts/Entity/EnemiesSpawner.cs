using System.Collections;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private int enemyIndex;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject enemyObject = ObjectsPooler.instance.GetPooledObjects(enemyIndex);
            if (enemyObject != null )
            {
                if (enemyObject != null) enemyObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Not enought enemy pool!");
            }
        }
    }

}
