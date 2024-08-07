using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private CharacterBlank characterBlank;

    private void Start()
    {
        characterBlank = GetComponent<CharacterBlank>();
    }

    public void Interact()
    {
        Debug.Log(characterBlank.Name);
    }
}
