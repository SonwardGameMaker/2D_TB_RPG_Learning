using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IPlayerIdleState
{
    public void Setup(PlayerHoldAttackState holdAttackState);
}
