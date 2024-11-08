using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrValUnmod
{
    float CurrentValue { get; set; }

    public event Action CurrentValChanged;
}
