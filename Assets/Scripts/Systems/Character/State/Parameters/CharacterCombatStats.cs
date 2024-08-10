using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CharacterCombatStats
{
    private float _maxHp;
    private float _currentHp;
    private float _dodge;
    private float _weaponSkill;
    private Weapon _currnetWeapon;
    private ReadOnlyCollection<DamageResistance> _damageResistances;

    private CharacterBlank _character;
    private CharacterInventory _inventory;

    #region constructor and destructor
    public CharacterCombatStats(CharacterBlank character, CharacterInventory characterInventory)
    {
        _character = character;
        _inventory = characterInventory;

        Init();

        _character.Health.HealthChanged += SetHp;
        _character.Stats.Dodge.CurrentValChanged += SetDodgeSkill;
        _character.Stats.Melee.CurrentValChanged += SetWeaponSkill;
        // SetWeapon
        _character.IngameParameters.DamageResistanceChanged += SetDamageResistances;
    }
    ~CharacterCombatStats()
    {
        _character.Health.HealthChanged -= SetHp;
        _character.Stats.Dodge.CurrentValChanged -= SetDodgeSkill;
        _character.Stats.Melee.CurrentValChanged -= SetWeaponSkill;
        // SetWeapon
        _character.IngameParameters.DamageResistanceChanged -= SetDamageResistances;
    }
    #endregion

    #region properties
    public float MaxHp { get => _maxHp; }
    public float CurrantHp { get => _currentHp; }
    public float DodgeSkill { get => _dodge; }
    public float WeaponSkill { get => _weaponSkill; }
    public ReadOnlyCollection<DamageResistance> DamageResistances { get => _damageResistances; }
     
    #endregion

    #region external interactions
    public void Init()
    {
        SetHp();
        SetDodgeSkill();
        SetWeaponSkill();
        SetWeapon();
        SetDamageResistances();
    }

    public HitDataContainer CalculateHitData
        => new HitDataContainer(_currnetWeapon, _currnetWeapon.CalculateeDamage, WeaponSkill);
    public DamageResistance DamageResistanceByType(DamageType damageType)
        => _damageResistances.First(dr => dr.DamageType == damageType);
    #endregion

    #region Calculation Logics
    private void SetHp()
    {
        _maxHp = _character.Health.MaxHp;
        _currentHp = _character.Health.CurrentHp;
    }

    private void SetDodgeSkill()
    {
        _dodge = _character.Stats.Dodge.CurrentValue;
    }

    private void SetWeaponSkill()
    {
        // TODO: Make system to update weapon skill when weapon is changing
        _weaponSkill = _character.Stats.Melee.CurrentValue;
    }

    private void SetWeapon()
    {
        // TODO: Make system to update current weapon when it's changing
        _currnetWeapon = _inventory.EquipmentSlots.MainHand;
    }

    private void SetDamageResistances()
    {
        _damageResistances = _character.IngameParameters.DamageResistances;
    }
    #endregion
}
