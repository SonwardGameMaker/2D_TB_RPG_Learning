using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines if item is stackable. Can be used only to items that will always be identical
/// </summary>
public interface IStackable
{
    public int Amount { get; protected set; }

    public void AddItems(int  amount);
}
