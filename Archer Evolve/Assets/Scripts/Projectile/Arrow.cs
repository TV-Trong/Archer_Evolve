using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    [field: SerializeField] public float velocity { get; set; }
    [field: SerializeField] public float lifetime { get; set; }
    [field: SerializeField] public GameObject projectilePrefab { get; set; }
    [field: SerializeField] public float projectileDamage { get; set; }
    public Rigidbody2D rigidBody { get; set; }
    public PlayerBehaviour playerInstance { get; set; }

    private PickingWeapon pickedWeapon;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        pickedWeapon = FindObjectOfType<PickingWeapon>();
        WeaponPullPowerToVelocity();
    }

    private void OnEnable()
    {
        playerInstance = PlayerBehaviour.instance;

        Vector2 weaponPosition = (Vector2)playerInstance.transform.position + new Vector2(0.1f, 0.32f);
        Vector2 getMouseWorldPosition = playerInstance.GetMousePositionOnScreen();
        Vector2 trajectoryDirection = (getMouseWorldPosition - weaponPosition).normalized;

        FlyToDirection(weaponPosition, trajectoryDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamge(CalculatedDamage());

            Debug.LogWarning($"Enemy takes: {CalculatedDamage()} Float Damage!");

            gameObject.SetActive(false);
        }
    }

    private void WeaponPullPowerToVelocity()
    {
        float pullPowerToProjectileVelocity;
        if (pickedWeapon.myWeapon.pullPower < 40) pullPowerToProjectileVelocity = pickedWeapon.myWeapon.pullPower / 5;
        else pullPowerToProjectileVelocity = pickedWeapon.myWeapon.pullPower * 10 / 100 + 1;
        velocity += pullPowerToProjectileVelocity;
        Mathf.Clamp(velocity, 10, 50);
    }

    private float CalculatedDamage()
    {
        float rollDamageRange = Random.Range(0.8f, 1.2f);
        return (playerInstance.strength + projectileDamage) * rollDamageRange;
    }

    public void FlyToDirection(Vector2 origin, Vector2 direction)
    {
        rigidBody.velocity = direction * velocity;
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
