using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        playerAnimator = GetComponentInParent<Animator>();
        playerBehaviour = GetComponentInParent<PlayerBehaviour>();
    }

    private void Update()
    {
        FlipMySprite();
        AdjustAnimationSpeed();
    }

    private void AdjustAnimationSpeed()
    {
        if (playerBehaviour.GetMoveInput() != Vector2.zero) playerAnimator.speed = 1f;
        else playerAnimator.speed = 0f;
    }

    private void FlipMySprite()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 getMouseWorldPosition = PlayerBehaviour.GetMousePositionOnScreen();
        playerTransform.localScale = (getMouseWorldPosition.x - transform.position.x < -0.1f) ? new Vector3(-1, 1, 1) : Vector3.one;
    }
}
