using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionBehaviourManager : MonoBehaviour
{
   public List<CharInteractionBase> GetInteractionLogics()
    => GetComponents<CharInteractionBase>().ToList();
}
