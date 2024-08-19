using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    private CharacterBlank _character;

    public CharacterCombatStats CharacterCombatStats;
    public CharacterStatsInfo CharacterStats;

    private void Awake()
    {
        _character = GetComponent<CharacterBlank>();
        _character.GetComponent<CharacterBlank>().Health.CharDeath += HandleCharDeath;

        CharacterCombatStats = new CharacterCombatStats(_character);
        CharacterStats = new CharacterStatsInfo(_character.Stats);
    }
    private void OnDestroy()
    {
        _character.GetComponent<CharacterBlank>().Health.CharDeath -= HandleCharDeath;
    }

    #region events
    public event Action<GameObject> CharDeath;
    #endregion

    public HitDataContainer CalculateHitData()
        => new HitDataContainer(this, CharacterCombatStats.CalculateDamage(), CharacterCombatStats.WeaponSkill.CurrentValue);

    private void HandleCharDeath() => CharDeath?.Invoke(gameObject);
}
