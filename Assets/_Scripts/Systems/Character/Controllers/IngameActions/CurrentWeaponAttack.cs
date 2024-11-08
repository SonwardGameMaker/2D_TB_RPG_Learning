using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CurrentWeaponAttack : Attackable
{
    #region init
    protected override void SetActionName()
        => _name = typeof(CurrentWeaponAttack).Name;


    #endregion
}
