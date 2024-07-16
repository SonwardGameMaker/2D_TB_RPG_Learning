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
    [SerializeField] private readonly List<ParInteraction> Affectors;
    [SerializeField] private readonly List<ParInteraction> ExternalAffectors;
    #endregion

    #region init
    public CharacterStats()
    {
        // Level
        Level = new Stat(20, 1, 1);
        Level.MaxValue.IsLowerBounded = true;
        Level.MaxValue.IsUpperBounded = true;
        Level.MaxValue.LowerBound = 20;
        Level.MaxValue.UpperBound = 20;
        Level.CurrentValue.IsLowerBounded = true;
        Level.CurrentValue.IsUpperBounded = true;
        Level.CurrentValue.LowerBound = 1;
        Level.CurrentValue.UpperBound = 20;

        // Attributes
        Strength = InitDefautAttribute();
        Dexterity = InitDefautAttribute();
        Agility = InitDefautAttribute();
        Constitution = InitDefautAttribute();
        Perception = InitDefautAttribute();
        Charisma = InitDefautAttribute();
        Intelligence = InitDefautAttribute();

        // Skills
        LightFirearm = InitDdefaultSkill();
        Firearm = InitDdefaultSkill();
        Melee = InitDdefaultSkill();
        HeavyMelee = InitDdefaultSkill();
        Dodge = InitDdefaultSkill();
        Stealth = InitDdefaultSkill();
        Hacking = InitDdefaultSkill();
        Lockpicking = InitDdefaultSkill();
        Pickpocketing = InitDdefaultSkill();
        Persuasion = InitDdefaultSkill();
        Intimidation = InitDdefaultSkill();
        Mercantile = InitDdefaultSkill();

        Affectors = new List<ParInteraction>
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
        Affectors[0].CalculateLogic = LevelAffectionOnSkills;
        for (int i = 1; i < Affectors.Count; i++)
        {
            Affectors[i].CalculateLogic = AttributeAffectionOnSkills;
        }
        ExternalAffectors = new List<ParInteraction>();
    }

    public void SetUp()
    {
        // Create initialization by Scriptable Object
    }

    private Stat InitDefautAttribute() => new Stat(DEFAULT_MAX_VALUE_FOR_ATTRIBUTE, DEFAULT_MIN_VALUE_FOR_ATTRIBUTE, DEFAULT_CURRENT_VALUE_FOR_ATTRIBUTE);
    private Stat InitDdefaultSkill() => new Stat(DEFAULT_MAX_VALUE_FOR_SKILL, DEFAULT_MIN_VALUE_FOR_SKILL, DEFAULT_CURRENT_VALUE_FOR_SKILL);
    #endregion

    #region external_interaction
    public void AddAffector(ParInteraction interaction)
    {
        ExternalAffectors.Add(interaction);
        //Debug.Log("External added; External count: " + ExternalAffectors.Count);
    }
    public void AddAffector(
        List<CharParameterBase> affectors,
        List<CharParameterBase> targets,
        ModValueCalculateLogic modValueCalculateLogic)
    {
        ParInteraction parInteraction = new ParInteraction(affectors, targets, modValueCalculateLogic);
        AddAffector(parInteraction);
    }
    public void AddAffector(
        CharParameterBase affector,
        List<CharParameterBase> targets,
        ModValueCalculateLogic modValueCalculateLogic)
        => AddAffector(new List<CharParameterBase> { affector }, targets, modValueCalculateLogic);
    public void AddAffector(
        List<CharParameterBase> affectors,
        CharParameterBase target,
        ModValueCalculateLogic modValueCalculateLogic)
        => AddAffector(affectors, new List<CharParameterBase> { target }, modValueCalculateLogic);
    public void AddAffector(
        CharParameterBase affector,
        CharParameterBase target,
        ModValueCalculateLogic modValueCalculateLogic)
        => AddAffector(new List<CharParameterBase> { affector }, new List<CharParameterBase> { target }, modValueCalculateLogic);

    public void UpDownAttribute(Stat attribute, bool increase)
    {
        attribute.SetCurrentValueBase(increase ? attribute.CurrentValue.BaseValue + 1
            : attribute.CurrentValue.BaseValue - 1);
        //Debug.Log("Max value: " + attribute.MaxValue.RealValue);
    }
    #endregion

    #region calculation_methods
    private static void LevelAffectionOnSkills(ref List<CharParameterBase> level, ref List<CharParameterBase> skills)
        => UtilityFunctionsParam.ParamModifyingValueSelectively(ref level, ref skills, UtilityFunctionsParam.GetCurrentValFloat, UtilityFunctionsParam.GetMaxVal, CalculateLevelLogic);
    private static void AttributeAffectionOnSkills(ref List<CharParameterBase> attributes, ref List<CharParameterBase> skills)
        => UtilityFunctionsParam.ParamModifyingValueSelectively(ref attributes, ref skills, UtilityFunctionsParam.GetCurrentValFloat, UtilityFunctionsParam.GetCurrentVal, CalculateLogic);

    private static (float, ModifierType) CalculateLevelLogic(CharParameterBase affector)
    {
        float mod = 5;
        float levels = UtilityFunctionsParam.GetCurrentVal(affector).RealValue - 1;
        float result = mod * levels;
        return new(result, ModifierType.Flat);
    }
    private static (float, ModifierType) CalculateLogic(CharParameterBase affector)
    {
        float mod = UtilityFunctionsParam.GetCurrentVal(affector).RealValue - 5;
        float percentPerPoint = 0.1f;
        float result = mod * percentPerPoint;
        return (result, ModifierType.Multiplicative);
    }
    #endregion
}
