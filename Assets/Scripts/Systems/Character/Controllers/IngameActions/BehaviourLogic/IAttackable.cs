using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    public int ApCost { get; }
    public void Setup(CharacterInfo characterInfo, Animator animator);
    public void Attack(IDamagable target, Action<bool, string> onEndCorutineAction);
}
