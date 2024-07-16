using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterResource
{
    private const float DEFAULT_MIN_VALUE = 0.0f;
    private const float DEFAULT_MAX_VALUE = 30.0f;

    [SerializeField] private float _minValue;
    [SerializeField] private float _currentValue;
    [SerializeField] private ModVar _maxValue;

    public CharacterResource(float maxValue, float minValue, float currentValue)
    {
        _minValue = minValue;
        _maxValue = new ModVar(maxValue);
        _currentValue = currentValue;

        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = minValue;

        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    ~CharacterResource() // дивна поведінка, перевірити
    {
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }
    public CharacterResource(float maxValue, float minValue) : this(maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public CharacterResource(float maxValue) : this(DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public CharacterResource() : this(DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    public float MinValue
    {
        get => _minValue;
        set
        {
            if (value > _maxValue.RealValue) _minValue = _maxValue.RealValue;
            else _minValue = value;
            OnMinValChanged();
        }
    }
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (value > _maxValue.RealValue) _currentValue = _maxValue.RealValue;
            else if (value < _minValue) _currentValue = _minValue;
            else _currentValue = value;
            OnCurrentValChanged();
        }
    }
    public ModVar MaxValue
    {
        get => _maxValue;
    }

    public void SetMaxValueBase(float baseValue)
    {
        //Debug.Log("Max value changed");
        if (baseValue < _minValue) _maxValue.BaseValue = _minValue;
        else _maxValue.BaseValue = baseValue;
    }

    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);

    public event Action OnMinValChanged;
    public event Action OnCurrentValChanged;
    public event Action OnMaxValChanged;

    private void HandleMinValEvents()
    {
        _maxValue.LowerBound = _minValue;
        OnMinValChanged?.Invoke();
    }
    private void HandleCurrentValEvents() => OnCurrentValChanged?.Invoke();
    private void HandleMaxValEvents()
    {
        OnMaxValChanged?.Invoke();
        if (_currentValue > _maxValue.RealValue)
            _currentValue = _maxValue.RealValue;
    }

    public override string ToString()
    {
        return "Stat can have values from " + _minValue + " to " + _maxValue.RealValue + "; Current value is " + _currentValue;
    }
}
