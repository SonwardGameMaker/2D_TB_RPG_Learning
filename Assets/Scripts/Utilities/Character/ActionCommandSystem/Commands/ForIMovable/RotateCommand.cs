using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ActionCommandBase
{
    private IMovable _object;
    private Vector3 _targetPosition;

    public RotateCommand(IMovable @object, Vector3 targetPosition)
    {
        _object = @object;
        _targetPosition = targetPosition;
    }

    public override void Execute()
    {
        _object.Rotate(_targetPosition, base.Execute);
    }
}
