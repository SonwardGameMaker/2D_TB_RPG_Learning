using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ApMpSystem : ICharResourseFieldGettable
{
    private const string DEFAULT_AP_NAME = "Action Points";
    private const string DEFAULT_MP_NAME = "Movement Points";
    private const float DEFAULT_AP_MAX_VALUE = 50.0f;
    private const float DEFAULT_MP_MAX_VALUE = 30.0f;

    [SerializeField] private CharResource _actionPoints;
    [SerializeField] private CharResource _movementPoints;

    #region constructor and destructor
    public ApMpSystem()
    {
        _actionPoints = new CharResource(DEFAULT_AP_NAME, DEFAULT_AP_MAX_VALUE, 0.0f, DEFAULT_AP_MAX_VALUE);
        _movementPoints = new CharResource(DEFAULT_MP_NAME, DEFAULT_MP_MAX_VALUE, 0.0f, DEFAULT_MP_MAX_VALUE);

        _actionPoints.SubscribeToAll(HandleApEvents);
        _movementPoints.SubscribeToAll(HandleMpEvents);
    }
    ~ApMpSystem()
    {
        _actionPoints.UnsubscribeToAll(HandleApEvents);
        _movementPoints.UnsubscribeToAll(HandleMpEvents);
    }
    #endregion

    #region properties
    public CharResourceInfo ActionPoints { get => new CharResourceInfo(_actionPoints); }

    public CharResourceInfo MovementPoints { get => new CharResourceInfo(_movementPoints); }
    #endregion

    public event Action ApChanged;
    public event Action MpChanged;

    #region event handlers
    private void HandleApEvents()
    {
        ApChanged?.Invoke();
    }
    private void HandleMpEvents()
    {
        MpChanged?.Invoke();
    }
    #endregion

    #region extrnal interations
    public CharResource GetFieldByEnum(CharResourceFieldType fieldType)
    {
        //CharResource result = (CharResource)GetType().GetField(fieldType.ToString()).GetValue(this);
        //if (result != null) return result;
        //else throw new Exception($"Field with name {fieldType.ToString()} doesn't exist in current class");
        if (fieldType == CharResourceFieldType._actionPoints) return _actionPoints;
        if (fieldType == CharResourceFieldType._movementPoints) return _movementPoints;
        throw new Exception($"Field with name {fieldType.ToString()} doesn't exist in current class");
    }

    public bool TryChangeCurrAp(float amount)
    {
        //Debug.Log("AP changing");
        if (_actionPoints.CurrentValue + amount < 0) return false;
        else
        {
            _actionPoints.CurrentValue += amount;
            return true;
        }
    }
    public bool TryChangeCurrMp(float amount)
    {
        //Debug.Log("MP changing");
        if (_movementPoints.CurrentValue + amount < 0)
        {
            if (TryChangeCurrAp(_movementPoints.CurrentValue + amount))
            {
                _movementPoints.CurrentValue = 0.0f;
                return true;
            }
            else return false;
        }  
        else
        {
            _movementPoints.CurrentValue += amount;
            return true;
        }
    }

    public void SetMp(float amount)
    {
        if (amount < _movementPoints.MinValue)
            _movementPoints.CurrentValue = _movementPoints.MinValue;
        else
            _movementPoints.CurrentValue = amount;
    }

    public void SetAp(float amount)
    {
        if (amount < _actionPoints.MinValue)
            _actionPoints.CurrentValue = _actionPoints.MinValue;
        else
            _actionPoints.CurrentValue = amount;
    }

    public void ResetAp() => _actionPoints.CurrentValue = _actionPoints.MaxValue;
    public void ResetMp() => _movementPoints.CurrentValue = _movementPoints.MaxValue;
    public void ResetAll()
    {
        ResetAp();
        ResetMp();
    }
    public ParInteraction CreateApEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _actionPoints, CalculateLogic);
    public ParInteraction CreateApEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => CreateApEffect(new List<CharParameterBase> { affector }, CalculateLogic);
    public ParInteraction CreateApEffect((List<CharParameterBase>, ModValueCalculateLogic) parameters)
        => new ParInteraction(parameters.Item1, _actionPoints, parameters.Item2);

    public ParInteraction CreateMpEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _movementPoints, CalculateLogic);
    public ParInteraction CreateMpEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => CreateMpEffect(new List<CharParameterBase> { affector }, CalculateLogic);
    public ParInteraction CreateMpEffect((List<CharParameterBase>, ModValueCalculateLogic) parameters)
        => new ParInteraction(parameters.Item1, _movementPoints, parameters.Item2);
    #endregion


    #region calculation methods
    #endregion
}
