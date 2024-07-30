using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IEntity
{
    [SerializeField] private InputActionReference movingAction;
    private Rigidbody2D myRigidbody;

    [SerializeField] private float moveSpeed = 300f;
    private Vector2 moveInput;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = movingAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Moving(moveInput);
    }

    public void Moving(Vector2 moveInput)
    {
        myRigidbody.velocity = moveInput * moveSpeed * Time.deltaTime;
    }
}
