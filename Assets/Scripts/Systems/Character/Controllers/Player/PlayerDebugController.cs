using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugController : PlayerIngameController
{
    private CharacterBlank _character;
    private CharHealthSystem _healthSystem;
    private ApMpSystem _apMpSystem;

    private void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _healthSystem = _character.Health;
    }

    public void ChangeHp(float amount)
        => _healthSystem.ChangeHp(amount);

    public void ChangeMp(float amount)
        => _apMpSystem.TryChangeCurrMp(amount);

    public void ChangeAp(float amount)
        => _apMpSystem.TryChangeCurrAp(amount);
}
