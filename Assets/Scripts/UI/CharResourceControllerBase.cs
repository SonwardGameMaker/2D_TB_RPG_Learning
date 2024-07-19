using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharResourceControllerBase : MonoBehaviour
{
    public ICharResourseFieldGettable charResourceSystem;
    [SerializeField] CharResourceFieldType crFieldType;

    private CharResource _currentCrField;
    private Transform _valueTextHolder;
    private int _amount = 0;

    public void Start()
    {
        _currentCrField = charResourceSystem.GetFieldByEnum(crFieldType);
        _currentCrField.MaxValChanged += OnResourceValueChanged;
        _currentCrField.MinValChanged += OnResourceValueChanged;
        _currentCrField.CurrentValChanged += OnResourceValueChanged;

        _valueTextHolder = transform.GetChild(1);
        OnResourceValueChanged();

        // Input init
        Transform InputAmount = transform.Find("InputField (Amount)");
        InputAmount?.GetComponent<TMP_InputField>().onValueChanged.AddListener(OnInputValueChanged);
    }
    private void OnDestroy()
    {
        _currentCrField.MaxValChanged -= OnResourceValueChanged;
        _currentCrField.MinValChanged -= OnResourceValueChanged;
        _currentCrField.CurrentValChanged -= OnResourceValueChanged;

        Transform InputAmount = transform.Find("InputField (Amount)");
        InputAmount?.GetComponent<TMP_InputField>().onValueChanged.RemoveAllListeners();
    }

    private void OnInputValueChanged(string value)
    {
        _amount = Convert.ToInt32(value);
    }
    private void OnResourceValueChanged()
    {
        _valueTextHolder.GetChild(0).GetComponent<TMP_Text>().text = _currentCrField.CurrentValue.ToString();
        _valueTextHolder.GetChild(2).GetComponent<TMP_Text>().text = _currentCrField.MaxValue.ToString();
    }
}
