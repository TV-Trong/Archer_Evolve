using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    [field: SerializeField] public float speed { get; set; } = 8f;
    [field: SerializeField] public float lifetime { get; set; } = 3f;
    [field: SerializeField] public GameObject projectilePrefab { get; set; }
    public Rigidbody2D rigidBody { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 origin = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 direction = (mouseWorldPosition - origin).normalized;
        FlyToDirection(origin, direction);
    }

    public void FlyToDirection(Vector2 origin, Vector2 direction)
    {
        rigidBody.velocity = direction * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Destroy(gameObject, lifetime);
    }
}
