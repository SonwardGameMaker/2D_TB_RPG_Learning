using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDurable
{
    public float MaxDurability {  get; }
    public float CurrentDurability {  get; }
    public bool IsBroken { get; }

    public event Action Brokes;
    public event Action Repairs;

    public void ChangeDurability(float amount);
}
