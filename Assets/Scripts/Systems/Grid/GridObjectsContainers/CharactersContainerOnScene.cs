using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactersContainerOnScene : MonoBehaviour
{
    public List<(Vector3, CharacterInfo)> GetCharacters()
    {
        List<(Vector3, CharacterInfo)> result = new List<(Vector3, CharacterInfo)>();

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out CharactersContainerOnScene container))
            {
                result.AddRange(container.GetCharacters());
            }

            (Vector3, CharacterInfo) character = new(child.position, child.GetComponent<CharacterInfo>());
            if (character.Item2 != null)
                result.Add(character);
        }

        return result.Select(cr => (cr.Item1 + transform.position, cr.Item2)).ToList();
    }
}
