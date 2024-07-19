using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharParameterBase
{
    protected const float DEFAULT_MIN_VALUE = 0.0f;
    protected const float DEFAULT_MAX_VALUE = 30.0f;
    protected const float DEFAULT_CURR_VALUE = 30.0f;
    protected const string DEFAULT_NAME = "Default_Name";

    protected string _name;

    public string Name { get { return _name; } }

    public CharParameterBase(string name) {  _name = name; }
    public CharParameterBase() : this(DEFAULT_NAME) { }

    public event Action MinValChanged;
    public event Action CurrentValChanged;
    public event Action MaxValChanged;

    protected void MinValChangedInvoke() => MinValChanged?.Invoke();
    protected void CurrentValChangedInvoke() => CurrentValChanged?.Invoke();
    protected void MaxValChangedInvoke() => MaxValChanged?.Invoke();
}
