using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StatType 
{ 
    Level,
    Strength, 
    Dexterity, 
    Agility, 
    Constitution, 
    Perception, 
    Charisma, 
    Intelligence,
    LightFirearm,
    Firearm,
    Melee,
    HeavyMelee,
    Dodge,
    Stealth,
    Hacking,
    Lockpicking,
    Pickpocketing,
    Persuasion,
    Intimidation,
    Mercantile
}

public class AttributeController : MonoBehaviour
{
    public CharacterBlank characterBlank;
    [SerializeField] StatType statType;
    [SerializeField] Stat currentStat;

    private CharacterStatsSystem stats;

    public void Start()
    {
        stats = characterBlank.Stats;
        currentStat = TypeToStat();
        currentStat.CurrentValChanged += OnStatValueChangedHandler;
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
        currentStat.CurrentValChanged -= OnStatValueChangedHandler;
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
    
    private Stat TypeToStat()
    {
        return (Stat)stats.GetType().GetProperty(statType.ToString()).GetValue(stats);
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
    private void OnClickIncreaseHandler() => stats.UpDownAttribute(currentStat, true);
    private void OnClickDecreaseHandler() => stats.UpDownAttribute(currentStat, false);
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
