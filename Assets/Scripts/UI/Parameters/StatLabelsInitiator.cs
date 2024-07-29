using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLabelsInitiator : MonoBehaviour
{
    [SerializeField] CharacterBlank charBlank;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).GetComponent<AttributeController>().characterBlank = charBlank;
        }
    }
}
