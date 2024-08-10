using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public event Action<bool, float, Damage> CharacterHitted;

    public void TakeHit(HitDataContainer hit);
    public void TakeDamage(Damage damage);
    public void TakeHealing(float amount);
}
