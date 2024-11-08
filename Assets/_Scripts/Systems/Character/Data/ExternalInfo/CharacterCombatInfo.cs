using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CharacterCombatInfo
{
    private Stat _weaponSkill;
    //private Weapon _currnetWeapon;

    private CharacterBlank _character;
    private CharacterInfo _characterInfo;

    #region constructor and destructor
    internal CharacterCombatInfo(CharacterInfo characterInfo, CharacterBlank character)
    {
        _characterInfo = characterInfo;
        _character = character;

        Init();

        _character.Stats.LightFirearm.CurrentValChanged += SetWeaponSkill;
        // SetWeapon
    }
    ~CharacterCombatInfo()
    {
        _character.Stats.LightFirearm.CurrentValChanged -= SetWeaponSkill;
        // SetWeapon
    }

    private void Init()
    {
        SetWeaponSkill();
        SetWeapon();
    }
    #endregion

    #region properties
    public CharResourceInfo Health { get => _character.Health.HealthInfo; }
    public CharResourceInfo ActionPoints { get => _character.ApMpSystem.ActionPoints; }
    public CharResourceInfo MovementPoints { get => _character.ApMpSystem.MovementPoints; }
    public StatInfo DodgeSkill { get => new StatInfo(_character.Stats.Dodge); }
    public StatInfo WeaponSkill { get => new StatInfo(_weaponSkill); }
    // Only for debug purposes
    public (float, float) WeaponDamage { get => (15, 20); }
    public int WeaponRange { get => 6; }
    public List<DamageResistanceInfo> DamageResistances 
        { get => _character.IngameParameters.DamageResistances.Select(dri => new DamageResistanceInfo(dri)).ToList(); }
    #endregion

    #region external interactions
    //public Damage CalculateDamage() => _currnetWeapon.CalculateDamage();
    public Damage CalculateDamage() => new Damage(UnityEngine.Random.Range(15, 20), DamageType.Mechanical, false, 0, 1, 100);
    public HitDataContainer CalculateHitData()
    {
        //Debug.Log($"Attackin skill: {CharacterCombatStats.WeaponSkill.CurrentValue}");
        return new HitDataContainer(_characterInfo, CalculateDamage(), WeaponSkill.CurrentValue);
    }
    public DamageResistance DamageResistanceByType(DamageType damageType)
        => _character.IngameParameters.DamageResistances.First(dr => dr.DamageType == damageType);
    #endregion

    #region calculation Logics
    private void SetWeaponSkill()
    {
        // TODO: Make system to update weapon skill when weapon is changing
        _weaponSkill = _character.Stats.LightFirearm;
    }

    private void SetWeapon()
    {
        //// TODO: Make system to update current weapon when it's changing
        //_currnetWeapon = new Weapon(
        //    "Sample weapon",
        //    "This is sample weapon",
        //    DamageType.Mechanical,
        //    20.0f,
        //    15,
        //    1,
        //    200,
        //    1000,
        //    _character);
    }
    #endregion
}
