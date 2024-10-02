using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionCommandBase
{
    public ActionCommandBase(Action onExecutionEnded)
    {
        OnExecutionEnded = onExecutionEnded;
    }
    public ActionCommandBase() : this(null) { }

    public virtual void Execute()
    {
        OnExecutionEnded?.Invoke();
    }

    public event Action OnExecutionEnded;
}
