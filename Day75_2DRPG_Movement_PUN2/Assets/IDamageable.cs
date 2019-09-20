using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection);
    void TakeDamage(int damage);
}
