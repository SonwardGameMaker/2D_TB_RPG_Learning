using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinValModifiable
{
    public ModVar MinValue { get; set; }
    public void SetMinBase(float baseValue);
    public void AddMinValueModifier(Modifier modifier);
}
