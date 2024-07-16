using System;
using UnityEngine;

[Serializable]
public class Stat : CharParameterBase, IMinValUnmod, ICurrValModifiable, IMaxValModifiable
{
    [SerializeField] private float _minValue;
    [SerializeField] private ModVar _currentValue;
    [SerializeField] private ModVar _maxValue;
    
    public Stat(float maxValue, float minValue, float currentValue)
    {
        _minValue = minValue;
        _maxValue = new ModVar(maxValue);
        _currentValue = new ModVar(currentValue);

        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = minValue;

        _currentValue.ValueChanged += HandleCurrentValEvents;
        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    ~Stat() // дивна поведінка, перевірити
    {
        _currentValue.ValueChanged -= HandleCurrentValEvents;
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }
    public Stat( float maxValue, float minValue) : this(maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public Stat(float maxValue) : this(DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public Stat() : this(DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    public float MinValue
    {
        get => _minValue;
        set 
        {
            if (value > _maxValue.RealValue) _minValue= _maxValue.RealValue;
            else _minValue = value;
            OnMinValChanged();
        }
    }
    public ModVar CurrentValue
    {
        get => _currentValue;
        set
        {
            if (value.RealValue > _maxValue.RealValue) _currentValue.BaseValue = _maxValue.RealValue;
            else if (value.RealValue < _minValue) _currentValue.BaseValue = _minValue;
            else _currentValue.BaseValue = value.RealValue;
        }
    }
    public void SetCurrentValueBase(float baseValue)
    {
        if (baseValue > _maxValue.RealValue) _currentValue.BaseValue = _maxValue.RealValue;
        else if (baseValue < _minValue) _currentValue.BaseValue = _minValue;
        else _currentValue.BaseValue = baseValue;
    }
    public ModVar MaxValue
    {
        get => _maxValue;
        set
        {
            //if (value.BaseValue < _minValue) _maxValue.BaseValue = _minValue;
            //else _maxValue.BaseValue = value.BaseValue;
            SetMaxValueBase(value.BaseValue);
        }
    }
    public void SetMaxValueBase(float baseValue) 
    {
        //Debug.Log("Max value changed");
        if (baseValue < _minValue) _maxValue.BaseValue = _minValue;
        else _maxValue.BaseValue = baseValue;
    }

    public void AddCurrentValueModifier(Modifier modifier) => _currentValue.AddModifier(modifier);
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
        if (_currentValue.BaseValue > _maxValue.RealValue)
            _currentValue.BaseValue = _maxValue.RealValue;
    } 

    public override string ToString()
    {
        return "Stat can have values from " + _minValue + " to " + _maxValue.RealValue + "; Current value is " + _currentValue.RealValue;
    }
}
