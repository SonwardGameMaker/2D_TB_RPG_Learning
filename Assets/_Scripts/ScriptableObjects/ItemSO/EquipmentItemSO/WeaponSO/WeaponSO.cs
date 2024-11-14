using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/WeaponSO")]
[Serializable]
public class WeaponSO : EquipmentItemSO
{
    [SerializeField] protected AttackRangeType _attackRangeType;
    [SerializeField] protected DamageType _damageType;
    [SerializeField] protected int _attackRange;
    [SerializeField] protected float _minDamage;
    [SerializeField] protected float _maxDamage;
    [SerializeField] protected float _critChancePercents;
    [SerializeField] protected float _critDamageBonusPercents;
    [SerializeField] protected float _penetrationPercents;

    [Header("Animations")]
    [SerializeField] protected Animation _preparingAttackAnimation;
    [SerializeField] protected Animation _attackAnimation;
    [SerializeField] protected Animation _afterAttackAnimation;

    public AttackRangeType AttackRangeType { get => _attackRangeType; }
    public DamageType DamageType { get => _damageType; }
    public int AttackRange { get => _attackRange; }
    public float MinDamage { get => _minDamage; }
    public float MaxDamage { get => _maxDamage; }
    public float CritChancePercents { get => _critChancePercents; }
    public float CritDamageBonusPercents { get => _critDamageBonusPercents; }
    public float PenetrationPercentage { get => _penetrationPercents; }

    private void Start()
    {
        if (_attackRange <= 0)
            _attackRange = 1;
    }

    public override Item CreateItem() => new Weapon(this);
}
