using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

internal abstract class ActionCommandBase
{
    protected IngameActionBase _objectBase;

    #region init
    public ActionCommandBase(object objectBase, Action<bool, string> onExecutionEnded)
    {
        OnExecutionEnded = onExecutionEnded;
        SetObjectBase(objectBase);
    }
    public ActionCommandBase(object objectBase) : this(objectBase, null) { }
    #endregion

    #region external interactions
    public void Execute(bool status, string message)
    {
        OnExecutionEnded?.Invoke(status, message);
    }
    public abstract void Execute();

    public virtual bool CanPerform()
        => _objectBase.CheckIfEnoughtResources();
    #endregion

    #region events
    public event Action<bool, string> OnExecutionEnded;
    #endregion

    #region internal operations
    private void SetObjectBase(object obj)
    {
        if (obj is IngameActionBase iObj)
            _objectBase = iObj;
        else
            throw new Exception($"Object boes not inheratate from {typeof(IngameActionBase)}");
    }
    #endregion
}
