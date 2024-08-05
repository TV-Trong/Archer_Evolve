using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private GameObject playerGameOject;
    public List<GameObject> tileObjects;

    private void Awake()
    {
        playerGameOject = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckExitWay();
        }
    }

    private void CheckExitWay()
    {
        Vector2 playerPosition = playerGameOject.transform.position;
        Vector2 myPosition = transform.position;
        Vector2 exitPosition = playerPosition - myPosition;

        if (exitPosition.x > 29.9f)
        {
            transform.position += new Vector3(60, 0, 0);
        }
        else if (exitPosition.x < -29.9f)
        {
            transform.position -= new Vector3(60, 0, 0);
        }

        if (exitPosition.y > 29.9f)
        {
            transform.position += new Vector3(0, 60, 0);
        }
        else if (exitPosition.y < -29.9f)
        {
            transform.position -= new Vector3(0, 60, 0); ;
        }
    }
}
