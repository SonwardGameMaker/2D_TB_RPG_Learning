using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatParameter : CharParameterBase, ICurrValModifiable
{
    [SerializeField] private ModVar _currentValue;

    public FlatParameter(string name, float value) : base(name)
    {
        _currentValue = new ModVar(value);

        _currentValue.ValueChanged += HandleCurrentValEvents;
    }
    public FlatParameter(string name) : this(name, DEFAULT_MIN_VALUE) { }
    public FlatParameter() : this(DEFAULT_NAME, DEFAULT_MIN_VALUE) { }
    ~FlatParameter()
    {
        _currentValue.ValueChanged -= HandleCurrentValEvents;
    }

    public float CurrentValueBase
    {
        get => _currentValue.BaseValue;
        set => _currentValue.BaseValue = value;
    }
    public float CurrentValue { get => _currentValue.RealValue; }

    public void AddCurrentValueModifier(Modifier modifier) => _currentValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetCurrentValueModifiers() => _currentValue.GetModifiers();
    public IReadOnlyList<Modifier> GetCurrentValueModifiers(ModifierType modifierType) => _currentValue.GetModifiers(modifierType);
    public bool TryRemoveCurrentValueModifier(Modifier modifier) => _currentValue.TryRemoveModifier(modifier);
    public bool TryRemoveCurrentValueAllModifiersOf(object source) => _currentValue.TryRemoveAllModifiersOf(source);

    private void HandleCurrentValEvents() => CurrentValChangedInvoke();
}
