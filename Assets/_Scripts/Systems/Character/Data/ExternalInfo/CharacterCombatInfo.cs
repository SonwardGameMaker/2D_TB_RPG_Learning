using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CharacterCombatInfo
{
    private Stat _weaponSkill;
    private Weapon _currnetWeapon;

    private CharacterBlank _character;
    private CharacterInfo _characterInfo;
    private CharacterInventory _inventory;

    #region events
    public event Action WeaponChanged;
    #endregion

    #region constructor and destructor
    public CharacterCombatInfo(CharacterInfo characterInfo, CharacterBlank character)
    {
        _characterInfo = characterInfo;
        _character = character;
        _inventory = character.GetComponent<CharacterInventory>();

        Init();

        _weaponSkill.CurrentValChanged += SetWeaponSkill;
    }
    ~CharacterCombatInfo()
    {
        _weaponSkill.CurrentValChanged -= SetWeaponSkill;
    }

    private void Init()
    {
        ChangeWeapon();
    }
    #endregion

    #region properties
    public CharResourceInfo Health { get => _character.Health.HealthInfo; }
    public CharResourceInfo ActionPoints { get => _character.ApMpSystem.ActionPoints; }
    public CharResourceInfo MovementPoints { get => _character.ApMpSystem.MovementPoints; }
    public StatInfo DodgeSkill { get => new StatInfo(_character.Stats.Dodge); }
    public StatInfo WeaponSkill { get => new StatInfo(_weaponSkill); }
    public Weapon CurrentWeapon { get => _currnetWeapon; }
    public (float, float) WeaponDamage { get => (_currnetWeapon.MinDamage, _currnetWeapon.MaxDamage); }
    public int WeaponRange { get => _currnetWeapon.AttackRange; }
    public List<DamageResistanceInfo> DamageResistances 
        { get => _character.IngameParameters.DamageResistances.Select(dri => new DamageResistanceInfo(dri)).ToList(); }
    #endregion

    #region external interactions
    public Damage CalculateDamage() => _currnetWeapon.CalculateDamage();

    public HitDataContainer CalculateHitData()
        => new HitDataContainer(_characterInfo, CalculateDamage(), WeaponSkill.CurrentValue);

    public DamageResistance DamageResistanceByType(DamageType damageType)
        => _character.IngameParameters.DamageResistances.First(dr => dr.DamageType == damageType);

    public void ChangeWeapon()
    {
        if (_currnetWeapon == null || _currnetWeapon == _inventory.Equipment.Weapon_2)
            SetWeapon(_inventory.Equipment.Weapon_1);
        else
            SetWeapon(_inventory.Equipment.Weapon_2);

        WeaponChanged?.Invoke();
    }
    #endregion

    #region calculation Logics
    private void SetWeaponSkill()
    {
        if (_currnetWeapon.AttackRangeType == AttackRangeType.Melee)
            _weaponSkill = _character.Stats.Melee;
        else
            _weaponSkill = _character.Stats.Firearm;
    }

    private void SetWeapon(Weapon weapon)
    { 
        _currnetWeapon = weapon;

        SetWeaponSkill();
    }
    #endregion
}
