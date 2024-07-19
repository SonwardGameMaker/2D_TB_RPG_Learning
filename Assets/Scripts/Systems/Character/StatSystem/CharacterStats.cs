using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    private const float DEFAULT_MIN_VALUE_FOR_ATTRIBUTE = 3.0f;
    private const float DEFAULT_CURRENT_VALUE_FOR_ATTRIBUTE = 5.0f;
    private const float DEFAULT_MAX_VALUE_FOR_ATTRIBUTE = 10.0f;
    private const float DEFAULT_MIN_VALUE_FOR_SKILL = 0.0f;
    private const float DEFAULT_CURRENT_VALUE_FOR_SKILL = 0.0f;
    private const float DEFAULT_MAX_VALUE_FOR_SKILL = 15.0f;

    #region properties
    // Level
    public Stat Level { get; private set; }
    private ParInteraction levelAffectsSkillsMaxValue;

    // Attributes
    public Stat Strength { get; private set; }
    public Stat Dexterity { get; private set; }
    public Stat Agility { get; private set; }
    public Stat Constitution { get; private set; }
    public Stat Perception { get; private set; }
    public Stat Charisma { get; private set; }
    public Stat Intelligence { get; private set; }

    // Skills
    public List<Stat> Skills { get; private set; }
    public Stat LightFirearm { get; private set; }
    public Stat Firearm { get; private set; }
    public Stat Melee { get; private set; }
    public Stat HeavyMelee { get; private set; }
    public Stat Dodge { get; private set; }
    public Stat Stealth { get; private set; }
    public Stat Hacking { get; private set; }
    public Stat Lockpicking { get; private set; }
    public Stat Pickpocketing { get; private set; }
    public Stat Persuasion { get; private set; }
    public Stat Intimidation { get; private set; }
    public Stat Mercantile { get; private set; }

    // Affecters
    private readonly List<ParInteraction> _interactions;
    #endregion

    #region init
    public CharacterStats()
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

        _interactions = new List<ParInteraction>
        {
            new ParInteraction(Level, new List<CharParameterBase>
            {
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
            }),
            new ParInteraction(new List<CharParameterBase> { Dexterity, Perception}, LightFirearm),
            new ParInteraction(Perception, Firearm),
            new ParInteraction(new List<CharParameterBase> { Strength, Dexterity }, Melee),
            new ParInteraction(new List<CharParameterBase> { Strength, Dexterity }, HeavyMelee),
            new ParInteraction(Agility, Dodge),
            new ParInteraction(Agility, Stealth),
            new ParInteraction(Intelligence, Hacking),
            new ParInteraction(Dexterity, Lockpicking),
            new ParInteraction(Dexterity, Pickpocketing),
            new ParInteraction(Charisma, Persuasion),
            new ParInteraction(new List<CharParameterBase> { Strength, Charisma}, Intimidation),
            new ParInteraction(new List<CharParameterBase> { Strength, Charisma}, Intimidation)
        };
        _interactions[0].CalculateLogic = LevelAffectionOnSkills;
        for (int i = 1; i < _interactions.Count; i++)
        {
            _interactions[i].CalculateLogic = AttributeAffectionOnSkills;
        }
    }

    public void SetUp()
    {
        // Create initialization by Scriptable Object
    }

    private Stat InitDefautAttribute(string name) => new Stat(name, DEFAULT_MAX_VALUE_FOR_ATTRIBUTE, DEFAULT_MIN_VALUE_FOR_ATTRIBUTE, DEFAULT_CURRENT_VALUE_FOR_ATTRIBUTE);
    private Stat InitDdefaultSkill(string name) => new Stat(name, DEFAULT_MAX_VALUE_FOR_SKILL, DEFAULT_MIN_VALUE_FOR_SKILL, DEFAULT_CURRENT_VALUE_FOR_SKILL);
    #endregion

    #region external_interaction
    public void UpDownAttribute(Stat attribute, bool increase)
    {
        attribute.CurrentValueBase = increase ? attribute.CurrentValueBase + 1
            : attribute.CurrentValueBase- 1;
        //Debug.Log("Max value: " + attribute.MaxValue.RealValue);
    }

    public void LevelConstAffectHelath(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
  => UtilityFunctionsParam.AffectorsAllTargetsEvery(
      ref affectors,
      ref targets,
      UtilityFunctionsParam.GetCurrentValFloat,
      UtilityFunctionsParam.GetMaxValueMod,
      LevelConstAffectingLogic
      );
    public void AgilityAffectMovementPoints(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
            => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
                ref affectors,
                ref targets,
                UtilityFunctionsParam.GetCurrentValFloat,
                UtilityFunctionsParam.GetMaxValueMod,
                AgilityAffectsMpModBase);
    #endregion

    #region calculation_methods
    private static void LevelAffectionOnSkills(ref List<CharParameterBase> level, ref List<CharParameterBase> skills)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(ref level, ref skills, UtilityFunctionsParam.GetCurrentValFloat, UtilityFunctionsParam.GetMaxValueMod, CalculateLevelLogic);
    private static void AttributeAffectionOnSkills(ref List<CharParameterBase> attributes, ref List<CharParameterBase> skills)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(ref attributes, ref skills, UtilityFunctionsParam.GetCurrentValFloat, UtilityFunctionsParam.GetCurrValueMod, CalculateLogic);

    private static (float, ModifierType) CalculateLevelLogic(CharParameterBase affector)
    {
        float mod = 5;
        float levels = UtilityFunctionsParam.GetCurrentValFloat(affector) - 1;
        float result = mod * levels;
        return new(result, ModifierType.Flat);
    }
    private static (float, ModifierType) CalculateLogic(CharParameterBase affector)
    {
        float mod = UtilityFunctionsParam.GetCurrentValFloat(affector) - 5;
        float percentPerPoint = 0.1f;
        float result = mod * percentPerPoint;
        return (result, ModifierType.Multiplicative);
    }

    private List<Modifier> LevelConstAffectingLogic(List<CharParameterBase> affectors)
    {
        CharParameterBase level = affectors.Find(a => a.Name == Level.Name);
        CharParameterBase constitution = affectors.Find(a => a.Name == Constitution.Name);
        if (level == null || constitution == null) throw new Exception($"{nameof(LevelConstAffectingLogic)} didn't find correct properties");

        float constMod = UtilityFunctionsParam.GetCurrentValFloat(constitution) - 5.0f;
        float ConstModFirstLvl = constMod * 4.0f;
        float result = ConstModFirstLvl;
        for (int i = 2; i <= UtilityFunctionsParam.GetCurrentValFloat(level); i++)
            result += constMod + 5.0f;

        return new List<Modifier> { new Modifier(result, ModifierType.Flat, constitution) };
    }
    private (float, ModifierType) AgilityAffectsMpModBase(CharParameterBase agility)
    {
        float agilMod = UtilityFunctionsParam.GetCurrentValFloat(agility) - 5;
        float mod = 4;
        float result = agilMod * mod;
        return new(result, ModifierType.Flat);
    }
    #endregion
}
