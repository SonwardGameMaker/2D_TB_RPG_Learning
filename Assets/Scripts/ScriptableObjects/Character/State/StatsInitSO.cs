using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Character/State/StatsInitSO")]
public class StatsInitSO : ScriptableObject
{
    private const float DEFAULT_MIN_VALUE_FOR_ATTRIBUTE = 3.0f;
    private const float DEFAULT_CURRENT_VALUE_FOR_ATTRIBUTE = 5.0f;
    private const float DEFAULT_MAX_VALUE_FOR_ATTRIBUTE = 5.0f;
    private const float DEFAULT_MIN_VALUE_FOR_SKILL = 0.0f;
    private const float DEFAULT_CURRENT_VALUE_FOR_SKILL = 0.0f;
    private const float DEFAULT_MAX_VALUE_FOR_SKILL = 15.0f;

    // Level
    public Stat Level;
    // Attributes
    public Stat Strength;
    public Stat Dexterity;  
    public Stat Agility;
    public Stat Constitution;
    public Stat Perception;
    public Stat Charisma;
    public Stat Intelligence;
    // Skills
    public Stat LightFirearm;
    public Stat Firearm;
    public Stat Melee;
    public Stat HeavyMelee;
    public Stat Dodge;
    public Stat Stealth;
    public Stat Hacking;
    public Stat Lockpicking;
    public Stat Pickpocketing;
    public Stat Persuasion;
    public Stat Intimidation;
    public Stat Mercantile;

    public StatsInitSO()
    {
        // Level
        Level = new Stat("Level", 20, 1, 1);

        // Attributes
        Strength = InitDefautAttribute(nameof(Strength));
        Dexterity = InitDefautAttribute(nameof(Dexterity));
        Agility = InitDefautAttribute(nameof(Agility));
        Constitution = InitDefautAttribute(nameof(Constitution));
        Perception = InitDefautAttribute(nameof(Perception));
        Charisma = InitDefautAttribute(nameof(Charisma));
        Intelligence = InitDefautAttribute(nameof(Intelligence));

        // Skills
        LightFirearm = InitDdefaultSkill(nameof(LightFirearm));
        Firearm = InitDdefaultSkill(nameof(Firearm));
        Melee = InitDdefaultSkill(nameof(Melee));
        HeavyMelee = InitDdefaultSkill(nameof(HeavyMelee));
        Dodge = InitDdefaultSkill(nameof(Dodge));
        Stealth = InitDdefaultSkill(nameof(Stealth));
        Hacking = InitDdefaultSkill(nameof(Hacking));
        Lockpicking = InitDdefaultSkill(nameof(Lockpicking));
        Pickpocketing = InitDdefaultSkill(nameof(Pickpocketing));
        Persuasion = InitDdefaultSkill(nameof(Persuasion));
        Intimidation = InitDdefaultSkill(nameof(Intimidation));
        Mercantile = InitDdefaultSkill(nameof(Mercantile));
    }

    private Stat InitDefautAttribute(string name) => new Stat(name, DEFAULT_MAX_VALUE_FOR_ATTRIBUTE, DEFAULT_MIN_VALUE_FOR_ATTRIBUTE, DEFAULT_CURRENT_VALUE_FOR_ATTRIBUTE);
    private Stat InitDdefaultSkill(string name) => new Stat(name, DEFAULT_MAX_VALUE_FOR_SKILL, DEFAULT_MIN_VALUE_FOR_SKILL, DEFAULT_CURRENT_VALUE_FOR_SKILL);
}
