using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CharacterStatsSystem
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

    //public List<Stat> Attributes;
    //public List<Stat> Skills;

    private readonly List<ParInteraction> _interactions2;

    #region attributes
    // Attributes
    public Stat Strength { get; private set; }
    public Stat Dexterity { get; private set; }
    public Stat Agility { get; private set; }
    public Stat Constitution { get; private set; }
    public Stat Perception { get; private set; }
    public Stat Charisma { get; private set; }
    public Stat Intelligence { get; private set; }
    #endregion
    #region skills
    // Skills
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
    #endregion

    // Affecters
    private readonly List<ParInteraction> _interactions;
    #endregion

    #region init
    public CharacterStatsSystem()
    {
        SetUp();
        _interactions = SetUpInteractions();
    }
    public CharacterStatsSystem(StatsInitSO statsInitSO)
    {
        if (statsInitSO == null)
        {
            Debug.LogError("Stats Init SO is null. Object initialized with default constructor");
            SetUp();
        }
        else
        {
            SetUp(statsInitSO);
        }

        _interactions = SetUpInteractions();
    }

    private void SetUp()
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
    public void SetUp(StatsInitSO statsInitSO)
    {
        // Level
        Level = new Stat(statsInitSO.Level);

        // Attributes
        Strength = new Stat(statsInitSO.Strength);
        Dexterity = new Stat(statsInitSO.Dexterity);
        Agility = new Stat(statsInitSO.Agility);
        Constitution = new Stat(statsInitSO.Constitution);
        Perception = new Stat(statsInitSO.Perception);
        Charisma = new Stat(statsInitSO.Charisma);
        Intelligence = new Stat(statsInitSO.Intelligence);

        // Skills
        LightFirearm = new Stat(statsInitSO.LightFirearm);
        Firearm = new Stat(statsInitSO.Firearm);
        Melee = new Stat(statsInitSO.Melee);
        HeavyMelee = new Stat(statsInitSO.HeavyMelee);
        Dodge = new Stat(statsInitSO.Dodge);
        Stealth = new Stat(statsInitSO.Stealth);
        Hacking = new Stat(statsInitSO.Hacking);
        Lockpicking = new Stat(statsInitSO.Lockpicking);
        Pickpocketing = new Stat(statsInitSO.Pickpocketing);
        Persuasion = new Stat(statsInitSO.Persuasion);
        Intimidation = new Stat(statsInitSO.Intimidation);
        Mercantile = new Stat(statsInitSO.Mercantile);
    }

    private List<ParInteraction> SetUpInteractions()
    {
        List<ParInteraction> result = new List<ParInteraction>()
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
            new ParInteraction(new List<CharParameterBase> { Intelligence, Charisma}, Mercantile)
        };
        result[0].CalculateLogic = LevelAffectionOnSkills;
        for (int i = 1; i < result.Count; i++)
        {
            result[i].CalculateLogic = AttributeAffectionOnSkills;
        }

        return result;
    }

    private Stat InitDefautAttribute(string name) => new Stat(name, DEFAULT_MAX_VALUE_FOR_ATTRIBUTE, DEFAULT_MIN_VALUE_FOR_ATTRIBUTE, DEFAULT_CURRENT_VALUE_FOR_ATTRIBUTE);
    private Stat InitDdefaultSkill(string name) => new Stat(name, DEFAULT_MAX_VALUE_FOR_SKILL, DEFAULT_MIN_VALUE_FOR_SKILL, DEFAULT_CURRENT_VALUE_FOR_SKILL);
    #endregion

    #region external interaction
    public void UpDownAttribute(Stat attribute, bool increase)
    {
        attribute.CurrentValueBase = increase ? attribute.CurrentValueBase + 1
            : attribute.CurrentValueBase- 1;
    }

    // Affection Logic
    public (List<CharParameterBase>, ModValueCalculateLogic) LevelConstAffectHelath()
        => (new List<CharParameterBase>() { Level, Constitution }, LevelConstAffectHelathLogic);

    public (List<CharParameterBase>, ModValueCalculateLogic) AgilityAffectMovementPoints()
        => (new List<CharParameterBase>() { Agility }, AgilityAffectMovementPointsLogic);

    public (List<CharParameterBase>, ModValueCalculateLogic) StrengthAffectMeleeDamage()
        => (new List<CharParameterBase>() { Strength }, StrengthAffectMeleeDamageLogic);

    public (List<CharParameterBase>, ModValueCalculateLogic) DexterityAffectLightMeleeCritChance()
        => (new List<CharParameterBase>() { Dexterity }, DexterityAffectLightMeleeCritChanceLogic);

    public (List<CharParameterBase>, ModValueCalculateLogic) PerceptionAffectFirearmCritChance()
        => (new List<CharParameterBase>() { Perception }, PerceptionAffectFirearmCritChanceLogic);

    #endregion

    #region calculation methods
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

    // Level Constitution affect HP
    private void LevelConstAffectHelathLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
    => UtilityFunctionsParam.AffectorsAllTargetsEvery(
        ref affectors,
        ref targets,
        UtilityFunctionsParam.GetCurrentValFloat,
        UtilityFunctionsParam.GetMaxValueMod,
        LevelConstAffectingModCalculating
        );
    private List<Modifier> LevelConstAffectingModCalculating(List<CharParameterBase> affectors)
    {
        CharParameterBase level = affectors.Find(a => a.Name == Level.Name);
        CharParameterBase constitution = affectors.Find(a => a.Name == Constitution.Name);
        if (level == null || constitution == null) throw new Exception($"{nameof(LevelConstAffectingModCalculating)} didn't find correct properties");

        float constMod = UtilityFunctionsParam.GetCurrentValFloat(constitution) - 5.0f;
        float ConstModFirstLvl = constMod * 4.0f;
        float result = ConstModFirstLvl;
        for (int i = 2; i <= UtilityFunctionsParam.GetCurrentValFloat(level); i++)
            result += constMod + 5.0f;

        return new List<Modifier> { new Modifier(result, ModifierType.Flat, constitution) };
    }

    // Agility affect Movement Points
    private void AgilityAffectMovementPointsLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
            => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
                ref affectors,
                ref targets,
                UtilityFunctionsParam.GetCurrentValFloat,
                UtilityFunctionsParam.GetMaxValueMod,
                AgilityAffectsMpModCalculating);
    private (float, ModifierType) AgilityAffectsMpModCalculating(CharParameterBase agility)
    {
        float agilMod = UtilityFunctionsParam.GetCurrentValFloat(agility) - 5;
        float mod = 4;
        float result = agilMod * mod;
        return new(result, ModifierType.Flat);
    }

    // Strength affect Melee Damage
    private void StrengthAffectMeleeDamageLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref targets,
            UtilityFunctionsParam.GetCurrentValFloat,
            UtilityFunctionsParam.GetCurrValueMod,
            StrengthAffectMeleeDamageModCalculating
            );
    private (float, ModifierType) StrengthAffectMeleeDamageModCalculating(CharParameterBase strength)
    {
        if (strength.Name != Strength.Name)
            throw new Exception($"{strength.Name} is incorrect for {nameof(StrengthAffectMeleeDamageModCalculating)} method");

        float strMod = UtilityFunctionsParam.GetCurrentValFloat(strength) - 5;
        float mod = 0.05f;
        float result = strMod * mod;
        return new(result, ModifierType.Additive);
    }

    // Dexterity affect Light Melee Crit Chance
    private void DexterityAffectLightMeleeCritChanceLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref targets,
            UtilityFunctionsParam.GetCurrentValFloat,
            UtilityFunctionsParam.GetCurrValueMod,
            DexterityAffectLightMeleeCritChanceModCalculation
            );
    private (float, ModifierType) DexterityAffectLightMeleeCritChanceModCalculation(CharParameterBase dexterity)
    {
        if (dexterity.Name != Dexterity.Name)
            throw new Exception($"{dexterity.Name} is incorrect for {nameof(DexterityAffectLightMeleeCritChanceModCalculation)} method");

        float strMod = UtilityFunctionsParam.GetCurrentValFloat(dexterity) - 5;
        float mod = 0.02f;
        float result = strMod * mod;
        return new(result, ModifierType.Additive);
    }

    // Perception affect Firearm Crit Chance Logic
    private void PerceptionAffectFirearmCritChanceLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref targets,
            UtilityFunctionsParam.GetCurrentValFloat,
            UtilityFunctionsParam.GetCurrValueMod,
            PerceptionAffectFirearmCritChanceModCalculation
            );
    private (float, ModifierType) PerceptionAffectFirearmCritChanceModCalculation(CharParameterBase perception) // same as for Dex, but I will leave it as it is in case I'l do some changes to one of those
    {
        if (perception.Name != Perception.Name)
            throw new Exception($"{perception.Name} is incorrect for {nameof(PerceptionAffectFirearmCritChanceModCalculation)} method");

        float strMod = UtilityFunctionsParam.GetCurrentValFloat(perception) - 5;
        float mod = 0.02f;
        float result = strMod * mod;
        return new(result, ModifierType.Additive);
    }
    #endregion
}
