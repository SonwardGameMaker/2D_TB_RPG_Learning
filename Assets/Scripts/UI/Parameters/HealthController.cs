using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : CharResourceControllerBase
{
    private CharHealthSystem _health;

    public void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = "Healths";
        Init();
        characterInfo.CharDeath += CharDeathEventHandler;
    }
    private void OnDestroy()
    {
        characterInfo.CharDeath -= CharDeathEventHandler;

        Transform IncButton = transform.Find("PlusHpBtn");
        IncButton?.GetComponent<Button>().onClick.RemoveAllListeners();
        Transform DecButton = transform.Find("MinusHpBtn");
        DecButton?.GetComponent<Button>().onClick.RemoveAllListeners();

        MyDestroy();
    }

    private void CharDeathEventHandler(GameObject character) => Debug.Log("Character is dead");
}
