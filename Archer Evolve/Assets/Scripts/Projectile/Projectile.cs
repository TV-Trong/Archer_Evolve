using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float speed { get; set; }
    float lifetime { get; set; }
    GameObject projectilePrefab { get; set; }
    Rigidbody2D rigidBody { get; set; }

    void FlyToDirection(Vector2 origin, Vector2 direction);
}
