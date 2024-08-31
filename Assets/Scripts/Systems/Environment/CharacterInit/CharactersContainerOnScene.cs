using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersContainerOnScene : MonoBehaviour
{
    public List<(Vector3, CharacterInfo)> GetCharacters()
    {
        List<(Vector3, CharacterInfo)> result = new List<(Vector3, CharacterInfo)>();

        foreach (Transform child in transform)
        {
            (Vector3, CharacterInfo) character = new(child.position, child.GetComponent<CharacterInfo>());
            if (character.Item2 != null)
                result.Add(character);
        }

        return result;
    }
}
