using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApMpController : CharResourceControllerBase
{
    [SerializeField] private Color ApColor;
    [SerializeField] private Color MpColor;
    private ApMpSystem _apMpSystem;

    void Start()
    {
        _apMpSystem = blank.ApMpSystem;
        _currentCrField = _apMpSystem.GetFieldByEnum(crFieldType);
        transform.GetChild(0).GetComponent<TMP_Text>().text = _currentCrField.Name;
        Init();
        

        if (crFieldType == CharResourceFieldType._actionPoints)
            InitColour(ApColor);
        else if (crFieldType == CharResourceFieldType._movementPoints)
            InitColour(MpColor);

        Transform IncButton = transform.Find("PlusBtn");
        IncButton?.GetComponent<Button>().onClick.AddListener(OnClickPlusHandler);
        Transform DecButton = transform.Find("MinusBtn");
        DecButton?.GetComponent<Button>().onClick.AddListener(OnClickMinusHanddler);
        Transform ResButton = transform.Find("ResetBtn");
        ResButton?.GetComponent<Button>().onClick.AddListener(OnClickResetHanler);
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
    private void OnClickPlusHandler()
    {
        if (crFieldType == CharResourceFieldType._actionPoints)
            _apMpSystem.TryChangeCurrAp(_amount);
        else if (crFieldType == CharResourceFieldType._movementPoints)
            _apMpSystem.TryChangeCurrMp(_amount);
    }
    private void OnClickMinusHanddler()
    {
        if (crFieldType == CharResourceFieldType._actionPoints)
                _apMpSystem.TryChangeCurrAp(-_amount);
        else if (crFieldType == CharResourceFieldType._movementPoints)
                _apMpSystem.TryChangeCurrMp(-_amount);
        
    }
    private void OnClickResetHanler()
    {
        if (crFieldType == CharResourceFieldType._actionPoints)
                _apMpSystem.ResetAp();
        else if (crFieldType == CharResourceFieldType._movementPoints)
                _apMpSystem.ResetMp();
        
    }
}
