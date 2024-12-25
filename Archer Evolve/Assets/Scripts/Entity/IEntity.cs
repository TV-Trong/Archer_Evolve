using UnityEngine;

public interface IEntity
{
    float moveSpeed {  get; set; }
    int healthPoint {  get; set; }
    float strength {  get; set; }
    float attackSpeed {  get; set; }
    Transform popupDamagePosition { get; set; }
    Transform weaponPosition { get; set; }

    void Moving(Vector2 moveInput);
    void Attack();
    void TakeDamge(float strength);
    void ShowPopupDamage(float strength);
    void Die();
}
