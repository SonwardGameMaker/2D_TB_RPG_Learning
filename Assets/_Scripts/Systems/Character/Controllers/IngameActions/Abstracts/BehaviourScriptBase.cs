using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourScriptBase : MonoBehaviour
{
    protected const string SUCCESSFUL_EXECUTION_MEASSAGE = "Execution successfuly complited";
    protected const string NOT_ENOUGHT_AP_MEASSAGE = "Not enought Action points";

    // TODO make this throught SO -------------
    [SerializeField] protected int _cooldown;
    // ----------------------------------------
    protected string _name;
    protected CharacterBlank _character;
    protected Coroutine _coroutine;
    protected bool _isSet = false;

    #region init
    protected virtual void SetActionName()
        => _name = typeof(BehaviourScriptBase).Name;

    public virtual void Setup(CharacterBlank character)
    {
        if (_isSet) return;

        SetActionName();
        
        _character = character;

        _isSet = true;
    }
    #endregion

    #region properties
    public string Name { get => _name; }
    public int Cooldown { get => _cooldown; }
    public bool IsSet { get => _isSet; }
    #endregion

    #region external interactions
    public abstract bool CheckIfEnoughtResources();

    public abstract void ConsumeResources();

    public virtual bool TryConsumeResources()
    {
        if (CheckIfEnoughtResources())
        {
            ConsumeResources();
            return true;
        }
        else 
            return false;
    }

    public abstract PlayerBehaviourScriptContainer GenerateScriptContainer();

    public void StopExecuting()
        => StopCoroutine(_coroutine);
    #endregion
}
