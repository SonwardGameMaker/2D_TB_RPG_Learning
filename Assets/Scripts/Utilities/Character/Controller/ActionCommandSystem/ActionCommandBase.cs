using System;
using UnityEngine;

internal abstract class ActionCommandBase
{
    protected IngameActionBase _objectBase;

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
        if (obj == null)
        {
            Debug.Log("Object is null");
            return;
        }
        Debug.Log($"{obj.GetType()}");
        if (obj is IngameActionBase iObj)
            _objectBase = iObj;
        else
            throw new Exception($"Object does not inheratate from {typeof(IngameActionBase)}");
    }
    #endregion
}
