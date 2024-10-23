using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLabelsInitiator : MonoBehaviour
{
    [SerializeField] CharacterInfo characterInfo;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).GetComponent<AttributeController>().characterInfo = characterInfo;
        }
    }
}
