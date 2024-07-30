using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerBehaviour = GetComponentInParent<PlayerBehaviour>();
    }

    private void Update()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 getMouseWorldPosition = PlayerBehaviour.GetMousePositionOnScreen();
        playerTransform.localScale = (getMouseWorldPosition.x - transform.position.x < -0.1f) ? new Vector3 (-1, 1 ,1) : Vector3.one;
    }
}
