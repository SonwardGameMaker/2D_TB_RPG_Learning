using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : CharResourceControllerBase
{
    private CharHealth _health;

    public void Start()
    {
        _health = blank.Health;
        _currentCrField = _health.GetFieldByEnum(crFieldType);
        transform.GetChild(0).GetComponent<TMP_Text>().text = _currentCrField.Name;
        Init();
        _health.CharDeath += CharDeathEventHandler;

        // Input init
        Transform IncButton = transform.Find("PlusHpBtn");
        IncButton?.GetComponent<Button>().onClick.AddListener(OnClickPlusHandler);
        Transform DecButton = transform.Find("MinusHpBtn");
        DecButton?.GetComponent<Button>().onClick.AddListener(OnClickMinusHanddler);
    }
    private void OnDestroy()
    {
        _health.CharDeath -= CharDeathEventHandler;

        Transform IncButton = transform.Find("PlusHpBtn");
        IncButton?.GetComponent<Button>().onClick.RemoveAllListeners();
        Transform DecButton = transform.Find("MinusHpBtn");
        DecButton?.GetComponent<Button>().onClick.RemoveAllListeners();

        MyDestroy();
    }

    private void OnClickPlusHandler() => _health.ChangeHp(_amount);
    private void OnClickMinusHanddler() => _health.ChangeHp(-_amount);
    private void CharDeathEventHandler() => Debug.Log("Character is dead");
}
