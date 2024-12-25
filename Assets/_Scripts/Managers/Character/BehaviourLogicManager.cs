using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviourLogicManager : MonoBehaviour
{
    public void Setup(CharacterBlank character)
    {
        foreach (BehaviourScriptBase behaviourScript in GetBehaviours())
            behaviourScript.Setup(character);
    }

    public List<BehaviourScriptBase> GetBehaviours()
        => GetComponents<BehaviourScriptBase>().ToList();
}
