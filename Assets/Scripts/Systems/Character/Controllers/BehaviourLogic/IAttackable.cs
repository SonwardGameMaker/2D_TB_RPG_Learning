using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    public void Setup(CharacterInfo characterInfo, Animator animator);
    public void Attack(IDamagable target, Action onEndCorutineAction);
}
