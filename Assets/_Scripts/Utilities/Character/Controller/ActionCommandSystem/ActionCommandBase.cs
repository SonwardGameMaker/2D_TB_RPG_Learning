using System;
using UnityEngine;

public abstract class ActionCommandBase
{
    protected BehaviourScriptBase _objectBase;

    #region init
    public ActionCommandBase(object objectBase, Action<bool, string> onExecutionEnded)
    {
        ExecutionEnded = onExecutionEnded;
        SetObjectBase(objectBase);
    }
    public ActionCommandBase(object objectBase) : this(objectBase, null) { }
    #endregion

    #region external interactions
    public void Execute(bool status, string message)
    {
        ExecutionEnded?.Invoke(status, message);
    }
    public abstract void Execute();

    public virtual bool CanPerform()
        => _objectBase.CheckIfEnoughtResources();
    #endregion

    #region events
    public event Action<bool, string> ExecutionEnded;
    #endregion

    #region internal operations
    private void SetObjectBase(object obj)
    {
        if (obj is BehaviourScriptBase iObj)
            _objectBase = iObj;
        else
            throw new Exception($"Object does not inheratate from {typeof(BehaviourScriptBase)}");
    }
    #endregion
}
