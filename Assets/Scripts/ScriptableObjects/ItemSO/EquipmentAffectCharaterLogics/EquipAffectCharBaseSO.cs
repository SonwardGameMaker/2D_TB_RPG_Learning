using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAffectCharBaseSO : ScriptableObject
{
    public virtual ParInteraction AffectCharacter(CharacterBlank character) { return null; }
}
