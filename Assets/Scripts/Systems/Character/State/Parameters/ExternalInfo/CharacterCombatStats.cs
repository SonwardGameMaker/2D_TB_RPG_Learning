using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CharacterCombatStats
{
    private float _weaponSkill;
    private Weapon _currnetWeapon;

    private CharacterBlank _character;
    //private CharacterInventory _inventory;

    #region constructor and destructor
    internal CharacterCombatStats(CharacterBlank character)
    {
        _character = character;
        //_inventory = characterInventory;

        Init();

        _character.Stats.Melee.CurrentValChanged += SetWeaponSkill;
        // SetWeapon
    }
    ~CharacterCombatStats()
    {
        _character.Stats.Melee.CurrentValChanged -= SetWeaponSkill;
        // SetWeapon
    }
    #endregion

    #region properties
    public float MaxHp { get => _character.Health.MaxHp; }
    public float CurrantHp { get => _character.Health.CurrentHp; }
    public float MaxAp { get => _character.ApMpSystem.MaxAp; }
    public float CurrentAp { get => _character.ApMpSystem.CurrentAp; }
    public float MaxMp { get => _character.ApMpSystem.MaxMp; }
    public float CurrentMp { get => _character.ApMpSystem.CurrentMp; }
    public float DodgeSkill { get => _character.Stats.Dodge.CurrentValue; }
    public float WeaponSkill { get => _weaponSkill; }
    public (float, float) WeaponDamage { get => (_currnetWeapon.MinDamage, _currnetWeapon.MaxDamage); }
    public ReadOnlyCollection<DamageResistance> DamageResistances { get => _character.IngameParameters.DamageResistances; }
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

    #region Calculation Logics
    private void SetWeaponSkill()
    {
        // TODO: Make system to update weapon skill when weapon is changing
        _weaponSkill = _character.Stats.Melee.CurrentValue;
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
            200,
            1000,
            _character);
    }
    #endregion
}
