using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaxValModifiable
{
    public ModVar MaxValue { get; set; }
    public void SetMaxValueBase(float baseValue);
    public void AddMaxValueModifier(Modifier modifier);
}
