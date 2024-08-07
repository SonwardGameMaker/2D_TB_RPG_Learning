using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public (bool, float) TakeHit(HitDataContainer hit);
    public void TakeDamage(Damage damage);
    public void TakeHealing(float amount);
}
