using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeController : MonoBehaviour
{
    public CharacterInfo characterInfo;
    [SerializeField] StatType statType;
    [SerializeField] StatInfo currentStat;

    private PlayerIngameController playerController;
    private CharacterStatsInfo stats;

    private void Start()
    {
        playerController = characterInfo.GetComponent<PlayerIngameController>();
        stats = characterInfo.CharacterStats;
        currentStat = TypeToStat();
        currentStat.SubscribeToAll(OnStatValueChangedHandler);
        Transform IncButton = transform.Find("IncreaseBaseValueBtn");
        if (IncButton != null)
        {
            IncButton.GetComponent<Button>().onClick.AddListener(OnClickIncreaseHandler);
        }
        Transform DecButton = transform.Find("DecreaseBaseValueBtn");
        if (DecButton != null)
        {
            DecButton.GetComponent<Button>().onClick.AddListener(OnClickDecreaseHandler);
        }
        BindStat();
    }
    public void OnDestroy()
    {
        currentStat.UnsubscribeToAll(OnStatValueChangedHandler);
        Transform IncButton = transform.Find("IncreaseBaseValueBtn");
        if (IncButton != null)
        {
            IncButton.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        Transform DecButton = transform.Find("DecreaseBaseValueBtn");
        if (DecButton != null)
        {
            DecButton.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    
    private StatInfo TypeToStat()
    {
        return (StatInfo)stats.GetType().GetProperty(statType.ToString()).GetValue(stats);
    }
    private void BindStat()
    {
        Transform nameLabel = transform.Find("StatNameLabel");
        if (nameLabel != null)
        {
            //Debug.Log(nameLabel.name);
            nameLabel.GetComponent<TMP_Text>().text = statType.ToString();
        }
        SetValues();
    }
    private void OnStatValueChangedHandler() => SetValues();
    private void OnClickIncreaseHandler() => playerController.UpDownAttribute(currentStat.Name, true);
    private void OnClickDecreaseHandler() => playerController.UpDownAttribute(currentStat.Name, false);
    private void SetValues()
    {
        Transform baseValue = transform.Find("BaseValue");
        if (baseValue != null)
        {
            int number = (int)currentStat.CurrentValueBase;
            baseValue.GetComponent<TMP_Text>().text = number.ToString();
        }
        Transform realValue = transform.Find("RealValue");
        if (realValue != null)
        {
            if ((int)currentStat.CurrentValueBase != (int)currentStat.CurrentValue)
            {
                int number = (int)currentStat.CurrentValue;
                realValue.GetComponent<TMP_Text>().text = number.ToString();
            }
            else realValue.GetComponent<TMP_Text>().text = "";
        }
    }
}
