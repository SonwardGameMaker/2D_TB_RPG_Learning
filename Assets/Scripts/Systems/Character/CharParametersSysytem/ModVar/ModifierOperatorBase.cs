using System.Collections.Generic;

public abstract class ModifierOperatorBase : IModifierOperations
{
    protected readonly List<Modifier> modifiers;

    public ModifierOperatorBase() => modifiers = new List<Modifier>();

    public virtual void AddModifier(Modifier modifier) => modifiers.Add(modifier);
    public virtual bool RemoveModifier(Modifier modifier) => modifiers.Remove(modifier);

    public abstract float CalculateValue(float baseValue, float currentValue);

    public virtual List<Modifier> GetAllModifiers() => modifiers;

    
}
