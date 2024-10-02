using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ActionCommandBase
{
    private IMovable _object;
    private List<PathfinderNodeBase> _path;

    public MoveCommand(IMovable @object, List<PathfinderNodeBase> path)
    {
        _object = @object;
        _path = path;
    }

    public override void Execute()
    {
        _object.Move(_path, base.Execute);
    }
}
