using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinValUnmod
{
    public float MinValue { get; set; }

    public event Action MinValChanged;
}
