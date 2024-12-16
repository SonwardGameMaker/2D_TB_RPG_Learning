using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApMpController : CharResourceControllerBase
{
    [SerializeField] private Color ApColor;
    [SerializeField] private Color MpColor;

    public override void Setup()
    {
        CharResourceInfo charResourceInfo;
        if (crFieldType == CharResourceFieldType._actionPoints)
            charResourceInfo = characterInfo.CharacterCombatInfo.ActionPoints;
        else
            charResourceInfo = characterInfo.CharacterCombatInfo.MovementPoints;

        Init(charResourceInfo);
        transform.GetChild(0).GetComponent<TMP_Text>().text = _currentCrField.Name;

        if (crFieldType == CharResourceFieldType._actionPoints)
            InitColour(ApColor);
        else if (crFieldType == CharResourceFieldType._movementPoints)
            InitColour(MpColor);

    }
    private void OnDestroy()
    {
        Transform IncButton = transform.Find("PlusHpBtn");
        IncButton?.GetComponent<Button>().onClick.RemoveAllListeners();
        Transform DecButton = transform.Find("MinusHpBtn");
        DecButton?.GetComponent<Button>().onClick.RemoveAllListeners();

        MyDestroy();
    }

    private void InitColour(Color color)
    {
        transform.GetChild(0).GetComponent<TMP_Text>().color = color;
        for (int i = 0; i < 3; i++)
            _valueTextHolder.GetChild(i).GetComponent<TMP_Text>().color = color;
    }
}
