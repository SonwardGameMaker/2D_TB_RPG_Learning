using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInteractionBase : MonoBehaviour
{
    protected CharacterBlank _thisCharacter;

    public virtual void Setup(CharacterBlank character)
    {
        _thisCharacter = character;
    }
}
