using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class AttackableContainer : PlayerBehaviourScriptContainer
{
    private Attackable _attackable;

    public AttackableContainer(Attackable attackable)
    {
        _attackable = attackable;
    }

    public override void Activate()
    {
        if (_stateMachine == null) throw new NullReferenceException($"{typeof(PlayerStateMachine).Name} isnt set in Container");

        // TODO Make attack radius as BehaviourScriptBase property
        _stateMachine.GetState<PlayerAttackState>().Setup(_attackable, _attackable.AttackRadius);
        _stateMachine.ChangeState<PlayerAttackState>();
    }
}
