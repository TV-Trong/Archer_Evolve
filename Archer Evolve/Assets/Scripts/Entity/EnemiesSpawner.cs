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
            //int spawnNumber = (int)Random.Range(1, 5);
            yield return new WaitForSeconds(spawnRate);
            GameObject enemyObject = ObjectsPooler.instance.GetPooledObjects(2);
            enemyObject.SetActive(true);
        }
    }

}
