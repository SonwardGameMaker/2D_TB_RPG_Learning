using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanCharacterInteractable : Interactable, IDamagable, IStealable, ITalkable
{
    public DamagableBaseSO DamagableBaseSO;

    private CharacterBlank _character;

    private void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _character.GetComponent<CharacterBlank>().Health.CharDeath += HandleCharDeath;
    }

    public event Action<GameObject> CharDeath;

    public (bool, float) TakeHit(HitDataContainer hit) => DamagableBaseSO.TakeHit(_character, hit); 
    public void TakeDamage(Damage damage) => DamagableBaseSO?.TakeDamage(_character, damage);
    public void TakeHealing(float amount) => DamagableBaseSO?.TakeHealing(_character, amount);

    private void HandleCharDeath() => CharDeath?.Invoke(gameObject);
}
