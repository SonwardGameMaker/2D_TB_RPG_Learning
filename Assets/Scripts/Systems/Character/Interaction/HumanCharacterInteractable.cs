using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanCharacterInteractable : Interactable, IDamagable, IStealable, ITalkable
{
    [SerializeField] private DamagableBaseSO DamagableBaseSO;

    private CharacterBlank _character;

    private new  void Start()
    {
        _character = GetComponent<CharacterBlank>();
    }

    public event Action<bool, float, Damage> CharacterHitted;

    public void TakeHit(HitDataContainer hit)
    {
        (bool, float) hitResult = DamagableBaseSO.TakeHit(_character, hit);
        CharacterHitted?.Invoke(hitResult.Item1, hitResult.Item2, hit.Damage);
    }
    public void TakeDamage(Damage damage) => DamagableBaseSO?.TakeDamage(_character, damage);
    public void TakeHealing(float amount) => DamagableBaseSO?.TakeHealing(_character, amount);
}
