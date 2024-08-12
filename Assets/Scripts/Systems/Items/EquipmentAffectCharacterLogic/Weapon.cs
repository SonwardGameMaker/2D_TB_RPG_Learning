using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Melee, Rangeed }
public enum WeaponWeight { Light, Normal }
[Serializable]
public class Weapon : Item, IEquipable, IDurable
{
    [SerializeField] private WeaponSO _weaponSO;

    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private WeaponWeight _weaponWeight;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private WeaponDamageParam _weaponDamage;
    [SerializeField] private CharResource _durability;
    [SerializeField] private bool _isBroken;

    private List<EquipAffectCharBaseSO> _equipAffectCharBaseInstances;
    private List<ParInteraction> _parInteractions;
    private CharacterBlank _bearer;
    public Weapon(
        string name,
        string description,
        DamageType damageType,
        float maxDamage,
        float minDamage,
        float price,
        float maxDurabilty,
        CharacterBlank bearer,
        Sprite spriteUI) : base(name, description, price, spriteUI)
    {
        _damageType = damageType;
        _weaponDamage = new WeaponDamageParam("Weapon Damage", minDamage, maxDamage);
        _durability = new CharResource("Durability", maxDurabilty);
        _isBroken = false;
        _bearer = bearer;
        _parInteractions = new List<ParInteraction>();
    }
    public Weapon(
        string name,
        string description,
        DamageType damageType,
        float maxDamage,
        float minDamage,
        float price,
        float maxDurabilty,
        CharacterBlank bearer)
        : this(name, description, damageType, maxDamage, minDamage, price, maxDurabilty, bearer, null)
    { }
    public Weapon(
        string name,
        string description,
        DamageType damageType,
        float maxDamage,
        float minDamage,
        float price,
        float maxDurabilty)
        : this(name, description, damageType, maxDamage, minDamage, price, maxDurabilty, null, null)
    { }
    public Weapon(WeaponSO weaponSO)
    {
        _parInteractions = new List<ParInteraction>();
    }
    public Weapon() 
    {
        _parInteractions = new List<ParInteraction>();
    }

    #region properties
    public WeaponType WeaponType { get => _weaponType; }
    public WeaponWeight WeaponWeight { get => _weaponWeight; }
    public DamageType DamageType { get => _damageType; }
    public float MaxDamage { get => _weaponDamage.MaxValue; }
    public float MinDamage { get => _weaponDamage.MinValue; }
    public float MaxDurability { get => _durability.MaxValue; }
    public float CurrentDurability { get => _durability.CurrentValue; }
    public bool IsBroken { get => _isBroken; }
    public CharacterBlank Bearer { get => _bearer; }
    #endregion

    public event Action Brokes;
    public event Action Repairs;

    #region external interaction
    public void Init(CharacterBlank bearer)
        => Init(bearer, _weaponSO);
    public void Init(CharacterBlank bearer, WeaponSO weaponSO)
    {
        Name = weaponSO.Name;
        Description = weaponSO.Description;
        Price = weaponSO.Price;
        ImageUI = weaponSO.ImageUI;

        _weaponType = weaponSO.WeaponType;
        _weaponWeight = weaponSO.WeaponWeight;
        _damageType = weaponSO.DamageType;
        _weaponDamage = weaponSO.WeaponDamage;
        _durability = weaponSO.Durability;
        _isBroken = weaponSO.IsBroken;

        _equipAffectCharBaseInstances = new List<EquipAffectCharBaseSO>();
        foreach (EquipAffectCharBaseSO iter in weaponSO.EquipAffectCharBase)
            _equipAffectCharBaseInstances.Add(ScriptableObject.Instantiate(iter));

        Equip(bearer);
    }

    public Damage CalculateeDamage => new Damage((int)_weaponDamage.CurrentValue, _damageType);

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
    }

    public void Unequip()
    {
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
        return result;
    }

    private List<ParInteraction> InflictItemEffects(CharacterBlank character)
    {
        List<ParInteraction> result = new List<ParInteraction>();
        foreach (EquipAffectCharBaseSO iter in _equipAffectCharBaseInstances)
            result.Add(iter.AffectCharacter(character));
        return result;
    }
    #endregion
}
