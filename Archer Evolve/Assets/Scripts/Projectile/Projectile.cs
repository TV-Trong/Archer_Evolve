using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float speed { get; set; }
    float lifetime { get; set; }
    int quality {  get; set; }
    GameObject projectilePrefab { get; set; }
    GameObject playerGameOject { get; set; }
    Rigidbody2D rigidBody { get; set; }
    
    void FlyToDirection(Vector2 shooterOriginPosition, Vector2 trajectoryDirection);
    IEnumerator DeactivateProjectile();

}
