using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDurable
{
    public event Action ItemBroken;
    public event Action ItemNoMoreBroken;

    public CharResourceInfo Durability { get; }
    public bool IsBroken {  get; }

    public void ChangeDurability(int amount);
    public void SetDurability(int amount);
    public string DisplayDurability(); 
}
