using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    private CharacterBlank _character;
    private CharacterCombatStats _characterCombatStats;

    private void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _characterCombatStats = _character.CombatStats;
    }

    public void Hit(IDamagable target)
    {
        target.TakeHit(_characterCombatStats.CalculateHitData());
    }
}
