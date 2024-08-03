using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : Item, IEquipable, IDurable
{
    [SerializeField] private DamageType _damageType;
    [SerializeField] private WeaponDamageParam _weaponDamage;
    [SerializeField] private CharResource _durability;
    [SerializeField] private bool _isBroken;

    private AffectCharParameters _affectionLogic;
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
        AffectCharParameters AffectionLogic,
        Sprite spriteUI) : base(name, description, price, spriteUI)
    {
        _damageType = damageType;
        _weaponDamage = new WeaponDamageParam("Weapon Damage", minDamage, maxDamage);
        _durability = new CharResource("Durability", maxDurabilty);
        _isBroken = false;
        _affectionLogic = AffectionLogic;
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
        AffectCharParameters AffectionLogic)
        : this(name, description, damageType, maxDamage, minDamage, price, maxDurabilty, AffectionLogic, null)
    { }

    #region properties
    public DamageType DamageType { get { return _damageType; } }
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
    public float CalculateeDamage => _weaponDamage.CurrentValue;

    public HitDataContainer HitData 
        => new HitDataContainer(this, new Damage(_weaponDamage.CurrentValue, _damageType));

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
        _parInteractions = _affectionLogic(_bearer);
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
    #endregion
}
