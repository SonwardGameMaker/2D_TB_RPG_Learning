using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum ArmorType { HeadArmor, BodyArmor, FeetArmor }
public delegate List<ParInteraction> AffectCharParameters(CharacterBlank characterBlank);
[Serializable]
public class Armor : Item, IEquipable, IDurable
{
    [SerializeField] private ArmourSO _armourSO;

    [SerializeField] private List<DamageResistance> _damageResistances;
    [SerializeField] private CharResource _durability;
    [SerializeField] private bool _isBroken;
    [SerializeField] private ArmorType _armorType;

    private List<EquipAffectCharBaseSO> _equipAffectCharBaseInstances;
    private List<ParInteraction> _parInteractions;
    private CharacterBlank _bearer;

    #region constructors
    public Armor(
        string name,
        string description,
        float price,
        float maxDurabilty,
        Sprite spriteUI) : base(name, description, price, spriteUI)
    {
        _durability = new CharResource("Durability", maxDurabilty);
        _isBroken = false;
        _parInteractions = new List<ParInteraction>();
    }
    public Armor(
    string name,
    string description,
    float price,
    float maxDurabilty) 
        : this(name, description, price, maxDurabilty, null) 
    { }
    public Armor(ArmourSO armourSO)
    {
        Debug.Log($"{nameof(ArmourSO)} constructor worked");
        Init(armourSO);
    }
    public Armor()
    {
        //Debug.Log("Empty constructor worked");
        //Init(_armourSO);
    }
    #endregion

    #region properties
    public float MaxDurability { get => _durability.MaxValue; }
    public float CurrentDurability { get => _durability.CurrentValue; }
    public bool IsBroken { get => _isBroken; }
    public CharacterBlank Bearer { get => _bearer; }
    public ArmorType ArmorType { get => _armorType; }
    #endregion

    #region events
    public event Action Brokes;
    public event Action Repairs;
    #endregion

    #region external interaction
    public void Init()
        => Init(_armourSO);

    public void Init(ArmourSO armourSO)
    {
        Name = armourSO.Name;
        Description = armourSO.Description;
        Price = armourSO.Price;
        ImageUI = armourSO.ImageUI;

        _damageResistances = new List<DamageResistance>();
        foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
            _damageResistances.Add(new DamageResistance(damageType));
        foreach (DamageResistance damageResistance in armourSO._damageResistances)
        {
            DamageResistance currResistance = _damageResistances.Find(dr => dr.DamageType == damageResistance.DamageType);
            if (currResistance == null) throw new Exception($"Item hasn't {damageResistance.DamageType} damage resistamce type");
            currResistance = new DamageResistance(damageResistance);
        }

        _armorType = armourSO._armourType;
        _durability = armourSO._durability;
        _isBroken = armourSO._isBroken;

        foreach (EquipAffectCharBaseSO iter in armourSO._equipAffectCharBase)
            _equipAffectCharBaseInstances.Add(ScriptableObject.Instantiate(iter));
    }
    public void ChangeDurability(float amount)
    {
        _durability.CurrentValue += amount;

        if (_isBroken = CheckIfBroken())
            _durability.CurrentValue = 0;
    }
    public void Equip(CharacterBlank character)
    {
        if (_bearer != null || _bearer != character)
            Unequip();
        
        _bearer = character;
        _parInteractions = AffectCharacter(_bearer);
        character.AddParInteractionRange(_parInteractions);
    }
    public void Unequip()
    {
        foreach (ParInteraction interaction in _parInteractions)
            _bearer.RemoveParInteraction(interaction);
        _parInteractions.Clear();
        _bearer = null;
    }
    #endregion

    #region calculation methods
    private bool CheckIfBroken()
    {
        if (_durability.CurrentValue <= 0)
        {
            if (!_isBroken)
                Brokes?.Invoke();
            return true;
        }
        else
        {
            if (_isBroken)
                Repairs?.Invoke();
            return false;
        }
    }

    private List<ParInteraction> AffectCharacter(CharacterBlank character)
    {
        List<ParInteraction> result = new List<ParInteraction>();
        result.AddRange(InflictItemEffects(character));
        result.AddRange(CreateDamageResistancesModifiersFromItem(character));
        return result;
    }

    private List<ParInteraction> InflictItemEffects(CharacterBlank character)
    {
        List<ParInteraction> result = new List<ParInteraction>();
        foreach (EquipAffectCharBaseSO iter in _equipAffectCharBaseInstances)
            result.Add(iter.AffectCharacter(character));
        return result;
    }

    private List<ParInteraction> CreateDamageResistancesModifiersFromItem(CharacterBlank character)
    {
        List<ParInteraction> result = new List<ParInteraction>();

        foreach (DamageResistance damageResistance in  _damageResistances)
        {
            result.Add(new ParInteraction(damageResistance,
                character.CharacterIngameParameters
                .DamageResistances
                .FirstOrDefault(dr => dr.DamageType == damageResistance.DamageType),
                AffectDamageResistanceLogic));
        }

        return result;
    }
    private void AffectDamageResistanceLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref targets,
            UtilityFunctionsParam.GetTrashholdValFloat,
            UtilityFunctionsParam.GetTrashholdValueMod,
            AffectDamageResistanceModCalculating
            );
    private (float, ModifierType) AffectDamageResistanceModCalculating(CharParameterBase affector)
        => new(UtilityFunctionsParam.GetTrashholdValFloat(affector), ModifierType.Flat);
    #endregion
}
