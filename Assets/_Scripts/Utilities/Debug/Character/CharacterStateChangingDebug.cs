using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

internal class CharacterStateChangingDebug : MonoBehaviour
{
    private CharacterBlank _character;

    [SerializeField] private int _healthPoints;
    [SerializeField] private int _movementPoints;
    [SerializeField] private int _actionPoints;

    public void Setup(CharacterBlank character)
    {
        _character = character;

        if (_character != null)
        {
            _character.Health.SetHp(_healthPoints);
            _character.ApMpSystem.SetMp(_movementPoints);
            _character.ApMpSystem.SetAp(_actionPoints);
        }
    }

    private void SetHp()
        => _healthPoints = (int)_character.Health.HealthInfo.CurrentValue;

    private void SetMp()
        => _movementPoints = (int)_character.ApMpSystem.MovementPoints.CurrentValue;

    private void SetAp()
        => _actionPoints = (int)_character.ApMpSystem.ActionPoints.CurrentValue;
}
