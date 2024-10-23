using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ControllerManagerBase : MonoBehaviour
{
    public virtual GridManager GridManager { get; set; }
    public GameManager GameManager { get; set; }

    public abstract void NewTurn();
    public void EndTurn()
    {
        
    }
}
