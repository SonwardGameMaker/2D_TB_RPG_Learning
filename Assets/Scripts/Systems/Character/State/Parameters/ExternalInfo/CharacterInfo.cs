using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    private CharacterBlank _character;

    public CharacterCombatStats CharacterCombatStats;
    public CharacterStatsInfo CharacterStats;

    void Start()
    {
        _character = GetComponent<CharacterBlank>();

        CharacterCombatStats = new CharacterCombatStats(_character);
        CharacterStats = new CharacterStatsInfo(_character.Stats);
    }

    public HitDataContainer CalculateHitData()
        => new HitDataContainer(this, CharacterCombatStats.CalculateDamage(), CharacterCombatStats.WeaponSkill);
}
