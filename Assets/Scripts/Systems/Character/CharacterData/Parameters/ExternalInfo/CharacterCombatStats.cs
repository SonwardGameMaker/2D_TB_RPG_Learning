using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CharacterCombatStats
{
    private Stat _weaponSkill;
    private Weapon _currnetWeapon;

    private CharacterBlank _character;
    //private CharacterInventory _inventory;

    #region constructor and destructor
    internal CharacterCombatStats(CharacterBlank character)
    {
        _character = character;
        //_inventory = characterInventory;

        Init();

        _character.Stats.LightFirearm.CurrentValChanged += SetWeaponSkill;
        // SetWeapon
    }
    ~CharacterCombatStats()
    {
        _character.Stats.LightFirearm.CurrentValChanged -= SetWeaponSkill;
        // SetWeapon
    }
    #endregion

    #region properties
    public CharResourseInfo Health { get => _character.Health.HealthInfo; }
    public CharResourseInfo ActionPoints { get => _character.ApMpSystem.ActionPoints; }
    public CharResourseInfo MovementPoints { get => _character.ApMpSystem.MovementPoints; }
    public StatInfo DodgeSkill { get => new StatInfo(_character.Stats.Dodge); }
    public StatInfo WeaponSkill { get => new StatInfo(_weaponSkill); }
    public (float, float) WeaponDamage { get => (_currnetWeapon.MinDamage, _currnetWeapon.MaxDamage); }
    public int WeaponRange { get => _currnetWeapon.WeaponRange; }
    public List<DamageResistanceInfo> DamageResistances 
        { get => _character.IngameParameters.DamageResistances.Select(dri => new DamageResistanceInfo(dri)).ToList(); }
    #endregion

    #region external interactions
    public void Init()
    {
        SetWeaponSkill();
        SetWeapon();
    }
    
    public Damage CalculateDamage() => _currnetWeapon.CalculateDamage();
    public DamageResistance DamageResistanceByType(DamageType damageType)
        => _character.IngameParameters.DamageResistances.First(dr => dr.DamageType == damageType);
    #endregion

    #region event subscription
    public void SubsribeToCharDeath(Action subscription) => _character.Health.CharDeath += subscription;

    public void UnsubsribeToCharDeath(Action subscription) => _character.Health.CharDeath -= subscription;
    #endregion

    #region calculation Logics
    private void SetWeaponSkill()
    {
        // TODO: Make system to update weapon skill when weapon is changing
        _weaponSkill = _character.Stats.LightFirearm;
    }

    private void SetWeapon()
    {
        // TODO: Make system to update current weapon when it's changing
        _currnetWeapon = new Weapon(
            "Sample weapon",
            "This is sample weapon",
            DamageType.Mechanical,
            20.0f,
            15,
            1,
            200,
            1000,
            _character);
    }
    #endregion
}
