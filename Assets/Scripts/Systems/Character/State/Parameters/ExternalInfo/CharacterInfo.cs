using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    private CharacterBlank _character;

    private CharacterCombatStats _characterCombatStats;
    private CharacterStatsInfo _characterStats;

    void Start()
    {
        _character = GetComponent<CharacterBlank>();

        _characterCombatStats = new CharacterCombatStats(_character);
        _characterStats = new CharacterStatsInfo(_character.Stats);
    }

}
