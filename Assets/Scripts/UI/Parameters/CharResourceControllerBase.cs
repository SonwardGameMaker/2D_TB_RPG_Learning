using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharResourceControllerBase : MonoBehaviour
{
    public CharacterInfo characterInfo;
    [SerializeField] protected CharResourceFieldType crFieldType;

    protected PlayerIngameController _playerIngameController;
    protected CharResourceInfo _currentCrField;
    protected Transform _valueTextHolder;
    protected int _amount = 0;

    protected void Init(CharResourceInfo resource)
    {
        _currentCrField = resource;
        _currentCrField.SubscribeToAll(OnResourceValueChanged);

        _valueTextHolder = transform.GetChild(1);
        OnResourceValueChanged();

        // Input init
        Transform InputAmount = transform.Find("InputField (Amount)");
        InputAmount?.GetComponent<TMP_InputField>().onValueChanged.AddListener(OnInputValueChanged);
    }
    protected void Init()
    {
        Init(characterInfo.CharacterCombatStats.Health);
    }
    protected void MyDestroy()
    {
        _currentCrField.UnsubscribeToAll(OnResourceValueChanged);

        Transform InputAmount = transform.Find("InputField (Amount)");
        InputAmount?.GetComponent<TMP_InputField>().onValueChanged.RemoveAllListeners();
    }

    private void OnInputValueChanged(string value)
    {
        _amount = Convert.ToInt32(value);
    }
    private void OnResourceValueChanged()
    {
        _valueTextHolder.GetChild(0).GetComponent<TMP_Text>().text = _currentCrField.MaxValue.ToString();
        _valueTextHolder.GetChild(2).GetComponent<TMP_Text>().text = _currentCrField.CurrentValue.ToString();
    }
}
