using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaxValUnmod
{
    float MaxValue { get; set; }

    public event Action MaxValChanged;
}
