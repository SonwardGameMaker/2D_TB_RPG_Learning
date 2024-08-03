using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CharParameterBase
{
    protected const float DEFAULT_MIN_VALUE = 0.0f;
    protected const float DEFAULT_MAX_VALUE = 30.0f;
    protected const float DEFAULT_CURR_VALUE = 30.0f;
    protected const string DEFAULT_NAME = "Default_Name";

    [SerializeField] protected string _name;

    public string Name { get { return _name; } }

    public CharParameterBase(string name) {  _name = name; }
    public CharParameterBase() : this(DEFAULT_NAME) { }
}
