using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    public float PricePerStack { get; set; }

    public void ChangeAmount(float amount);
}
