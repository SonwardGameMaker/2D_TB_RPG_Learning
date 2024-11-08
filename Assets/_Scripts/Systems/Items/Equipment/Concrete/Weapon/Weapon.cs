using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipmentItem
{
    protected AttackRangeType _attackRangeType;
    protected DamageType _damageType;
    protected int _attackRange;
    protected WeaponDamageParam _damage;
    protected float _penetrationPercentage;
    protected float _critChancePercents;
    protected float _critDamageBonusPercents;

    #region init
    public Weapon(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public Weapon(string name, string description, float price) : base(name, description, price) { }
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
