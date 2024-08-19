using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthLabelScript : MonoBehaviour
{
    public CharacterInfo _characterInfo;

    private PlayerIngameController _playerIngameController;
    private CharResourseInfo _health;
    private int _amount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _playerIngameController = _characterInfo.GetComponent<PlayerIngameController>();
        _health = _characterInfo.CharacterCombatStats.Health;
        OnHealthValueChanged();
        _health.SubscribeToAll(OnHealthValueChanged);
        _characterInfo.CharDeath += CharDeathEventHandler;
    }
    private void OnDestroy()
    {
        _health.SubscribeToAll(OnHealthValueChanged);
        _characterInfo.CharDeath -= CharDeathEventHandler;
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
        transform.GetChild(0).GetComponent<TMP_Text>().text = _health.CurrentValue.ToString();
        transform.GetChild(2).GetComponent<TMP_Text>().text = _health.MaxValue.ToString();
    }
    private void CharDeathEventHandler(GameObject character) => Debug.Log("Character is dead");
}
