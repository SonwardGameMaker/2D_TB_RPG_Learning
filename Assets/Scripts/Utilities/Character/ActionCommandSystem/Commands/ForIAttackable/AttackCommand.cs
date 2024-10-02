using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : ActionCommandBase
{
    IAttackable _object;
    IDamagable _target;

    public AttackCommand(IAttackable @object, IDamagable target)
    {
        _object = @object;
        _target = target;
    }

    public override void Execute()
    {
        _object.Attack(_target, base.Execute);
    }
}
