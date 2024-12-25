using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ControllerManagerBase : MonoBehaviour
{
    public GameManager GameManager { get; set; }

    public virtual void Setup(CharacterInfo character, CharacterIngameController characterIngameController)
    {
    }

    public abstract void NewTurn();
    public void EndTurn()
    {
        
    }

}
