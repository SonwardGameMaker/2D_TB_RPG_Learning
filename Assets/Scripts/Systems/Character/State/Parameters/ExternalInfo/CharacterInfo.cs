using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBlank))]
public class CharacterInfo : MonoBehaviour
{
    private CharacterBlank _character;

    public CharacterCombatStats CharacterCombatStats;
    public CharacterStatsInfo CharacterStats;


    #region init
    private void OnDestroy()
    {
        _character.GetComponent<CharacterBlank>().Health.CharDeath -= HandleCharDeath;
    }

    internal void SetUp(CharacterBlank character)
    {
        _character = character;
        character.GetComponent<CharacterBlank>().Health.CharDeath += HandleCharDeath;

        CharacterCombatStats = new CharacterCombatStats(character);
        CharacterStats = new CharacterStatsInfo(character.Stats);

        //Debug.Log($"{nameof(CharacterInfo)} enabled");
    }

    #endregion

    public string GetBaseInfoString()
    {
        return $"Name: {_character.Name}, Health: {CharacterCombatStats.Health.CurrentValue}/{CharacterCombatStats.Health.MaxValue}";
    }

    #region events
    public event Action<GameObject> CharDeath;
    #endregion

    public HitDataContainer CalculateHitData()
    {
        //Debug.Log($"Attackin skill: {CharacterCombatStats.WeaponSkill.CurrentValue}");
        return new HitDataContainer(this, CharacterCombatStats.CalculateDamage(), CharacterCombatStats.WeaponSkill.CurrentValue);
    }

    private void HandleCharDeath() => CharDeath?.Invoke(gameObject);
}
