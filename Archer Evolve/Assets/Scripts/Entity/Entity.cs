using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    float moveSpeed {  get; set; }
    int healthPoint {  get; set; }
    float strength {  get; set; }
    float attackSpeed {  get; set; }

    void Moving(Vector2 moveInput);
    void Attack();
    void TakeDamge(float strength);
}
