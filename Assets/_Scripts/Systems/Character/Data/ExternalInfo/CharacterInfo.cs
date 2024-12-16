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
    public void Setup()
    {
        _character = GetComponent<CharacterBlank>();
        _character.Setup();

        _healthSystem = _character.GetComponent<CharacterBlank>().Health;

        CharacterCombatInfo = new CharacterCombatInfo(this, _character);
        CharacterStats = new CharacterStatsInfo(_character.Stats);

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
