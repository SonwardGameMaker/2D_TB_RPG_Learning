using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class MoveCommand : ActionCommandBase
{
    private IMovable _object;
    private List<PathfinderNodeBase> _path;

    public MoveCommand(IMovable @object, List<PathfinderNodeBase> path) : base(@object)
    {
        _object = @object;
        _path = path;
    }

    public override void Execute()
    {
        _object.Move(_path, Execute);
    }

    public override bool CanPerform()
        => _object.CheckIfEnoughtResources(_path);
}
