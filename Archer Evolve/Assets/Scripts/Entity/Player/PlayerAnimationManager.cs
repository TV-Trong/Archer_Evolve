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
        if (playerBehaviour.GetMoveInput() != Vector2.zero) playerAnimator.speed = playerBehaviour.moveSpeed / playerBehaviour.baseMoveSpeed;
        else playerAnimator.speed = 0;
    }

    private void FlipMySprite()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 getMouseWorldPosition = playerBehaviour.GetMousePositionOnScreen();
        playerTransform.localScale = (getMouseWorldPosition.x - transform.position.x < -0.1f) ? new Vector3(-1, 1, 1) : Vector3.one;
    }
}
