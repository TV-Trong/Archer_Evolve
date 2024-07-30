using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float speed { get; set; }
    float lifetime { get; set; }
    GameObject projectilePrefab { get; set; }
    Rigidbody2D rigidBody { get; set; }
    
    void FlyToDirection(Vector2 shooterOriginPosition, Vector2 trajectoryDirection);
    IEnumerator DeactivateProjectile();

}
