using System;
using System.Collections;
using UnityEngine;

internal class BiteAttackContainer : PlayerBehaviourScriptContainer
{
    private BiteAttack _biteAttackScript;

    public BiteAttackContainer(BiteAttack biteAttackScript)
    {
        _biteAttackScript = biteAttackScript;
    }

    public override void Activate()
    {
        if (_stateMachine == null) throw new NullReferenceException(typeof(PlayerStateMachine).Name);

        // TODO Make attack radius as BehaviourScriptBase property
        _stateMachine.GetState<PlayerAttackState>().Setup(_biteAttackScript, _biteAttackScript.AttackRadius);
        _stateMachine.ChangeState<PlayerAttackState>();
    }
}
