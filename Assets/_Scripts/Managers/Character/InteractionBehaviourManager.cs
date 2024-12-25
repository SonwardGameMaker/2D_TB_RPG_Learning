using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionBehaviourManager : MonoBehaviour
{
    public void Setup(CharacterBlank character)
    {
        foreach (CharInteractionBase charInteraction in GetInteractionLogics())
            charInteraction.Setup(character);
    }

   public List<CharInteractionBase> GetInteractionLogics()
    => GetComponents<CharInteractionBase>().ToList();
}
