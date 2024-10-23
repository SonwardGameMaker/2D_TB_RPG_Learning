using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[Serializable]
public sealed class ModVar
{
    private const int DEFAULT_LIST_CAPACITY = 4;
    private const int DEFAULT_DIGIT_ACCURACY = 2;
    internal const int MAXIMUM_ROUND_DIGITS = 8;

    #region fields
    [SerializeField] private float baseValue;

    [SuppressMessage("NDepend", "ND1902:AvoidStaticFieldsWithAMutableFieldType", Justification = "Cannot mutate after Instantiation of Stat, will throw.")]
    [SuppressMessage("NDepend", "ND1901:AvoidNonReadOnlyStaticFields", Justification = "Not readonly so that it can be called from Init() for reset.")]
    private static ModifierOperationsCollection _ModifierOperationsCollection = new();

    private readonly int _digitAccuracy;
    private readonly List<Modifier> _modifiersList = new();
    private readonly SortedList<ModifierType, IModifierOperations> _modifiersOperations = new();

    private float _currentValue;
    private bool _isDirty;

    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "Needed for Unity's disable domain reload feature.")]
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init() => _ModifierOperationsCollection = new();

    [HideInInspector] public bool IsLowerBounded = false;
    [HideInInspector] public float LowerBound = 0.0f;
    [HideInInspector] public bool IsUpperBounded = false;
    [HideInInspector] public float UpperBound = 30.0f;
    #endregion

    #region constructors
    public ModVar(float baseValue, int digitAccuracy, int modsMaxCapacity)
    {
        this.baseValue = baseValue;
        _currentValue = baseValue;
        _digitAccuracy = digitAccuracy;

        InitializeModifierOperations(modsMaxCapacity);

        // local method
        void InitializeModifierOperations(int capacity)
        {
            var modifierOperations = _ModifierOperationsCollection.GetModifierOperations(capacity);

            foreach (var operationType in modifierOperations.Keys)
                _modifiersOperations[operationType] = modifierOperations[operationType]();
        }
    }
    public ModVar(float baseValue) : this(baseValue, DEFAULT_DIGIT_ACCURACY, DEFAULT_LIST_CAPACITY) { }
    public ModVar(float baseValue, int digitAccuracy) : this(baseValue, digitAccuracy, DEFAULT_LIST_CAPACITY) { }
    public ModVar(ModVar other) : this(other.baseValue, other._digitAccuracy, DEFAULT_LIST_CAPACITY) 
    {
        _modifiersOperations = other._modifiersOperations;
        IsDirty = true;
    }
    #endregion

    #region properties
    public float BaseValue
    {
        get => baseValue;
        set
        {
            baseValue = value;
            _currentValue = CalculateModifiedValue(_digitAccuracy);
            OnValueChanged();
        }
    }

    public float RealValue
    {
        get
        {
            if (IsDirty)
            {
                _currentValue = CalculateModifiedValue(_digitAccuracy);
                OnValueChanged();
            }

            return _currentValue;
        }
    }

    private bool IsDirty
    {
        get => _isDirty;
        set
        {
            _isDirty = value;
            if (_isDirty)
                OnModifiersChanged();
        }
    }
    #endregion

    public event Action ValueChanged;
    public event Action ModifiersChanged;

    #region modifiers operations
    public void AddModifier(Modifier modifier)
    {
        IsDirty = true;
        _modifiersOperations[modifier.Type].AddModifier(modifier);
        OnValueChanged(); // !!!WARNING!!! For some reason author of this code didn't add this call in origianl code, coud couse a problems in future
    }

    public static ModifierType NewModifierType(int order, Func<IModifierOperations> modifierOperationsDelegate)
    {
        try
        {
            return _ModifierOperationsCollection.AddModifierOperation(order, modifierOperationsDelegate);
        }
        catch
        {
            throw new InvalidOperationException("Add any modifier operations before any initialization of the Stat class!");
        }
    }

    public IReadOnlyList<Modifier> GetModifiers()
    {
        _modifiersList.Clear();

        foreach (var modifiersOperation in _modifiersOperations.Values)
            _modifiersList.AddRange(modifiersOperation.GetAllModifiers());

        return _modifiersList.AsReadOnly();
    }

    public IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => _modifiersOperations[modifierType].GetAllModifiers().AsReadOnly();

    public bool TryRemoveModifier(Modifier modifier)
    {
        var isModifierRemoved = false;

        if (_modifiersOperations[modifier.Type].RemoveModifier(modifier))
        {
            IsDirty = true;
            isModifierRemoved = true;
        }

        return isModifierRemoved;
    }

    public bool TryRemoveAllModifiersOf(object source)
    {
        bool isModifierRemoved = false;

        for (int i = 0; i < _modifiersOperations.Count; i++)
        {
            if (TryRemoveAllModifiersOfSourceFromList(source,
                   _modifiersOperations.Values[i].GetAllModifiers()))
            {
                isModifierRemoved = true;
                IsDirty = true;
            }
        }

        return isModifierRemoved;

        // local method, static guarantees that it won't be allocated to the heap
        // (It is never converted to delegate, no variable captures)
        static bool TryRemoveAllModifiersOfSourceFromList(object source, List<Modifier> listOfModifiers)
        {
            bool modifierHasBeenRemoved = false;

            for (var i = listOfModifiers.Count - 1; i >= 0; i--)
            {
                if (ReferenceEquals(source, listOfModifiers[i].Source))
                {
                    listOfModifiers.RemoveAt(i);
                    modifierHasBeenRemoved = true;
                }
            }

            return modifierHasBeenRemoved;
        }
    }

    private float CalculateModifiedValue(int digitAccuracy)
    {
        digitAccuracy = Math.Clamp(digitAccuracy, 0, MAXIMUM_ROUND_DIGITS);

        float finalValue = baseValue;

        for (int i = 0; i < _modifiersOperations.Count; i++)
            finalValue += _modifiersOperations.Values[i].CalculateValue(baseValue, finalValue);

        if (IsLowerBounded && finalValue < LowerBound) finalValue = LowerBound;
        else if (IsUpperBounded && finalValue > UpperBound) finalValue = UpperBound;
        
        IsDirty = false;

        return (float)Math.Round(finalValue, digitAccuracy);
    }
    #endregion

    private void OnValueChanged() => ValueChanged?.Invoke();
    private void OnModifiersChanged() => ModifiersChanged?.Invoke();
}
