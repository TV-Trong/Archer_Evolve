using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private Vector2 getMouseWorldPosition;
    private void Update()
    {
        SpinToCursor(GetDirectionToAim());
        FlipMySprite();
    }

    private void FlipMySprite()
    {
        float myScaleX = playerTransform.localScale.x;
        transform.localScale = new Vector3(myScaleX, 1, 1);
    }

    private Vector2 GetDirectionToAim()
    {
        Vector2 myPosition = transform.position;
        getMouseWorldPosition = PlayerBehaviour.GetMousePositionOnScreen();
        return (getMouseWorldPosition - myPosition).normalized;
    }

    private void SpinToCursor(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
