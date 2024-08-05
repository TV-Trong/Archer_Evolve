using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float velocity { get; set; }
    float lifetime { get; set; }
    float projectileDamage {  get; set; }
    GameObject projectilePrefab { get; set; }
    PlayerBehaviour playerInstance { get; set; }
    Rigidbody2D rigidBody { get; set; }
    
    void FlyToDirection(Vector2 shooterOriginPosition, Vector2 trajectoryDirection);
    IEnumerator DeactivateProjectile();

}
