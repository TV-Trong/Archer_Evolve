using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    [field: SerializeField] public float speed { get; set; } = 8f;
    [field: SerializeField] public float lifetime { get; set; }
    [field: SerializeField] public GameObject projectilePrefab { get; set; }
    [field: SerializeField] public int quality { get; set; }
    public Rigidbody2D rigidBody { get; set; }
    public GameObject playerGameOject { get; set; }

    private PlayerBehaviour playerBehaviour;

    private void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerGameOject = GameObject.FindGameObjectWithTag("Player");
        playerBehaviour = playerGameOject.GetComponent<PlayerBehaviour>();

        Vector2 getMouseWorldPosition = playerBehaviour.GetMousePositionOnScreen();
        Vector2 shooterOriginPosition = GameObject.Find("Projectile Shooter").transform.position;
        Vector2 trajectoryDirection = (getMouseWorldPosition - shooterOriginPosition).normalized;

        FlyToDirection(shooterOriginPosition, trajectoryDirection);
    }

    private int CalculatedDamage()
    {
        PlayerBehaviour playerBehaviour = playerGameOject.GetComponent<PlayerBehaviour>();
        return playerBehaviour.strength + quality;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamge(CalculatedDamage());
            gameObject.SetActive(false);
        }
    }

    public void FlyToDirection(Vector2 origin, Vector2 direction)
    {
        rigidBody.velocity = direction * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        StartCoroutine(DeactivateProjectile());
    }

    public IEnumerator DeactivateProjectile()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
