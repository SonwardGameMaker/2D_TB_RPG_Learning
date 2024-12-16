using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviourLogicManager : MonoBehaviour
{
    public List<BehaviourScriptBase> GetBehaviours()
        => GetComponents<BehaviourScriptBase>().ToList();
}
