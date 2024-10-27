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

        //_healthPoints = (int)character.Health.HealthInfo.CurrentValue;
        //_movementPoints = (int)character.ApMpSystem.MovementPoints.CurrentValue;
        //_actionPoints = (int)character.ApMpSystem.ActionPoints.CurrentValue;

        //character.Health.HealthInfo.SubscribeToAll(SetHp);
        //character.ApMpSystem.MovementPoints.SubscribeToAll(SetMp);
        //character.ApMpSystem.ActionPoints.SubscribeToAll(SetAp);

        if (_character != null)
        {
            _character.Health.SetHp(_healthPoints);
            _character.ApMpSystem.SetMp(_movementPoints);
            _character.ApMpSystem.SetAp(_actionPoints);
        }
    }

    private void OnDestroy()
    {
        //_character.Health.HealthInfo.UnsubscribeToAll(SetHp);
        //_character.ApMpSystem.MovementPoints.UnsubscribeToAll(SetMp);
        //_character.ApMpSystem.ActionPoints.UnsubscribeToAll(SetAp);
    }

    //private void OnValidate()
    //{
    //    if (_character != null)
    //    {
    //        _character.Health.SetHp(_healthPoints);
    //        _character.ApMpSystem.SetMp(_movementPoints);
    //        _character.ApMpSystem.SetAp(_actionPoints);
    //    }
    //}

    private void SetHp()
        => _healthPoints = (int)_character.Health.HealthInfo.CurrentValue;

    private void SetMp()
        => _movementPoints = (int)_character.ApMpSystem.MovementPoints.CurrentValue;

    private void SetAp()
        => _actionPoints = (int)_character.ApMpSystem.ActionPoints.CurrentValue;
}
