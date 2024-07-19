using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthLabelScript : MonoBehaviour
{
    public CharacterBlank characterBlank;
    [SerializeField] CharHealth _health;

    private int _amount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _health = characterBlank.Health;
        OnHealthValueChanged();
        _health.HealthChanged += OnHealthValueChanged;
        _health.CharDeath += CharDeathEventHandler;
        
        // Input init
        Transform IncButton = transform.Find("PlusHpBtn");
        IncButton?.GetComponent<Button>().onClick.AddListener(OnClickPlusHandler);
        Transform DecButton = transform.Find("MinusHpBtn");
        DecButton?.GetComponent<Button>().onClick.AddListener(OnClickMinusHanddler);
        Transform InputAmount = transform.Find("InputField (Amount HP)");
        InputAmount?.GetComponent<TMP_InputField>().onValueChanged.AddListener(OnInputValueChanged);
    }
    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthValueChanged;
        _health.CharDeath -= CharDeathEventHandler;
        Transform IncButton = transform.Find("PlusHpBtn");
        IncButton?.GetComponent<Button>().onClick.RemoveAllListeners();
        Transform DecButton = transform.Find("MinusHpBtn");
        DecButton?.GetComponent<Button>().onClick.RemoveAllListeners();
        Transform InputAmount = transform.Find("InputField (Amount HP)");
        InputAmount?.GetComponent<TMP_InputField>().onValueChanged.RemoveAllListeners();
    }
    private void OnInputValueChanged(string value)
    {
        _amount = Convert.ToInt32(value);
    }
    private void OnHealthValueChanged()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = _health.CurrentHp.ToString();
        transform.GetChild(2).GetComponent<TMP_Text>().text = _health.MaxHp.ToString();
    }
    private void OnClickPlusHandler() => _health.ChangeHp(_amount);
    private void OnClickMinusHanddler() => _health.ChangeHp(-_amount);
    private void CharDeathEventHandler() => Debug.Log("Character is dead");
}
