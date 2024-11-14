using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : EquipmentItem
{
    protected AttackRangeType _attackRangeType;
    protected DamageType _damageType;
    [SerializeField] protected int _attackRange;
    protected WeaponDamageParam _damage;
    protected float _penetrationPercentage;
    protected float _critChancePercents;
    protected float _critDamageBonusPercents;

    #region init
    public Weapon(
        string name,
        string description,
        int maxDamage,
        int minDamage,
        DamageType damageType,
        AttackRangeType attackRangeType,
        int attackRange = 1,
        float penetrationPercentage = 0,
        float critChancePercents = 0,
        float critDamageBonusPercents = 100,
        float price = 0, 
        Sprite imageUI = null) : base(name, description, price, imageUI)
    {
        _damage = new WeaponDamageParam("Damage", maxDamage, minDamage);
        _damageType = damageType;
        _attackRangeType = attackRangeType;
        _attackRange = attackRange;
        _penetrationPercentage = penetrationPercentage;
        _critChancePercents = critChancePercents;
        _critDamageBonusPercents= critDamageBonusPercents;
    }
    public Weapon(WeaponSO itemSO) : base(itemSO)
    {
        _attackRangeType = itemSO.AttackRangeType;
        _damageType = itemSO.DamageType;
        _attackRange = itemSO.AttackRange;
        _damage = new WeaponDamageParam("Weapon Damage", itemSO.MaxDamage, itemSO.MinDamage);
        _penetrationPercentage = itemSO.PenetrationPercentage;
        _critChancePercents = itemSO.CritChancePercents;
        _critDamageBonusPercents = itemSO.CritDamageBonusPercents;
    }
    #endregion

    #region properties
    public AttackRangeType AttackRangeType { get => _attackRangeType; }
    public DamageType DamageType { get => _damageType; }
    public int AttackRange { get => _attackRange; }
    public float MinDamage { get => _damage.MinValue; }
    public float MaxDamage { get => _damage.MaxValue; }
    public float CritChancePercents { get => _critChancePercents; }
    public float CritDamageBonusPercents { get => _critDamageBonusPercents; }
    public float PenetrationPercentage { get => _penetrationPercentage; }
    #endregion

    #region external interactions
    public virtual Damage CalculateDamage()
        => new Damage(_damage.CalculateDamage(), _damageType, false, _penetrationPercentage, _critChancePercents, _critDamageBonusPercents);
    #endregion
}
