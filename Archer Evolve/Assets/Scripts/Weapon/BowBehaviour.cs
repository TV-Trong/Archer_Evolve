using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private Vector2 getMouseWorldPosition;
    private PlayerBehaviour playerBehaviour;
    

    private void Awake()
    {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    
    private void Update()
    {
        SpinToCursor(GetDirectionToAim());
        FlipMySprite();
    }

    private void FlipMySprite()
    {
        transform.localScale = new Vector3(playerTransform.localScale.x, 1, 1);
    }

    private Vector2 GetDirectionToAim()
    {
        Vector2 myPosition = transform.position;
        getMouseWorldPosition = playerBehaviour.GetMousePositionOnScreen();
        return (getMouseWorldPosition - myPosition).normalized;
    }

    private void SpinToCursor(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
