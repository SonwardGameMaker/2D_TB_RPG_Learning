using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class AttackCommand : ActionCommandBase
{
    private IAttackable _object;
    private IDamagable _target;

    public AttackCommand(IAttackable @object, IDamagable target) : base(@object)
    {
        _object = @object;
        _target = target;
    }

    public override void Execute()
    {
        _object.Attack(_target, Execute);
    }
}
