using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInteractable : Interactable, IDamagable
{
    public DamagableBaseSO DamagableBaseSO;

    private CharacterBlank _character;

    private void Start()
    {
        _character = GetComponent<CharacterBlank>();
    }

    public (bool, float) TakeHit(HitDataContainer hit) => DamagableBaseSO.TakeHit(_character, hit);
    public void TakeDamage(Damage damage) => DamagableBaseSO?.TakeDamage(_character, damage);
    public void TakeHealing(float amount) => DamagableBaseSO?.TakeHealing(_character, amount);
}
