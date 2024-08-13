using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipAffectCharBaseSO : ScriptableObject
{
    public virtual ParInteraction AffectCharacter(CharacterBlank character) { return null; }
}
