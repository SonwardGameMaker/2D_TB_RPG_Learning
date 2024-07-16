using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrValModifiable
{
    public ModVar CurrentValue { get; set; }

    public void SetCurrentValueBase(float baseValue);
    public void AddCurrentValueModifier(Modifier modifier);
}
