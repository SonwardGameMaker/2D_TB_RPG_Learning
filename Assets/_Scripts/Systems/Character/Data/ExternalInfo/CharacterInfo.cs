using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBlank))]
public class CharacterInfo : MonoBehaviour
{
    private CharacterBlank _character;
    private CharHealthSystem _healthSystem;

    public CharacterCombatInfo CharacterCombatInfo;
    public CharacterStatsInfo CharacterStats;


    #region init
    internal void SetUp(CharacterBlank character)
    {
        _character = character;
        _healthSystem = character.GetComponent<CharacterBlank>().Health;
        

        CharacterCombatInfo = new CharacterCombatInfo(this, character);
        CharacterStats = new CharacterStatsInfo(character.Stats);

        _healthSystem.CharDeath += HandleCharDeath;
    }

    private void OnDestroy()
    {
        _healthSystem.CharDeath -= HandleCharDeath;
    }
    #endregion

    public string GetBaseInfoString()
    {
        return $"Name: {_character.Name}, Health: {CharacterCombatInfo.Health.CurrentValue}/{CharacterCombatInfo.Health.MaxValue}";
    }
    public override string ToString()
        => GetBaseInfoString();

    #region events
    public event Action<GameObject> CharDeath;
    #endregion

    private void HandleCharDeath() => CharDeath?.Invoke(gameObject);
}
