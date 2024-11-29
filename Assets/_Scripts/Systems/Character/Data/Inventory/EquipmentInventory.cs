using System;
using UnityEngine;

[Serializable]
public class EquipmentInventory
{
    [SerializeField] private Weapon _weapon1;
    [SerializeField] private Weapon _weapon2;

    [SerializeField] private HeadGear _headEquipment;
    [SerializeField] private TorsoGear _torsoEquipment;
    [SerializeField] private Boots _boots;

    [SerializeField] private CombatTool _combatTool1;
    [SerializeField] private CombatTool _combatTool2;
    [SerializeField] private CombatTool _combatTool3;
    [SerializeField] private CombatTool _combatTool4;
    [SerializeField] private CombatTool _combatTool5;

    private CharacterBlank _character;

    private WeaponSO _defaultWeapon;

    #region init
    public EquipmentInventory(CharacterBlank character)
    {
        _character = character;
    }
    public EquipmentInventory(CharacterBlank character, DefaultEquipmentSO defaultEquipmentSO, EquipmentInventorySO equipmentSO = null) : this(character)
    {

        if (defaultEquipmentSO == null 
            || ScriptablebjectsExistUtils.IsDynamicallyGenerated(defaultEquipmentSO) 
            || defaultEquipmentSO.DefaultWeaponSOs.Count == 0) throw new System.Exception("Default Equiopment doesn't set");

        _defaultWeapon = defaultEquipmentSO.DefaultWeaponSOs[0];

        if (equipmentSO != null)
        {
            if (ScriptablebjectsExistUtils.IsDynamicallyGenerated(equipmentSO.Weapon_1))
                EquipIn(ref _weapon1, _defaultWeapon.CreateItem() as Weapon);
            else
                EquipIn(ref _weapon1, equipmentSO.Weapon_1.CreateItem() as Weapon);
            
            if (ScriptablebjectsExistUtils.IsDynamicallyGenerated(equipmentSO.Weapon_2))
                EquipIn(ref _weapon2, _defaultWeapon.CreateItem() as Weapon);
            else
                EquipIn(ref _weapon2, equipmentSO.Weapon_2.CreateItem() as Weapon);

            EquipIn(ref _headEquipment, equipmentSO.HeadEquipment.CreateItem() as HeadGear);
            EquipIn(ref _torsoEquipment, equipmentSO.TorsoEquipment.CreateItem() as TorsoGear);
            EquipIn(ref _boots, equipmentSO.Boots.CreateItem() as Boots);

            EquipIn(ref _combatTool1, equipmentSO.CombatTool_1.CreateItem() as CombatTool);
            EquipIn(ref _combatTool2, equipmentSO.CombatTool_2.CreateItem() as CombatTool);
            EquipIn(ref _combatTool3, equipmentSO.CombatTool_3.CreateItem() as CombatTool);
            EquipIn(ref _combatTool4, equipmentSO.CombatTool_4.CreateItem() as CombatTool);
            EquipIn(ref _combatTool5, equipmentSO.CombatTool_5.CreateItem() as CombatTool);
        }
        else
        {
            EquipIn(ref _weapon1, _defaultWeapon.CreateItem() as Weapon);
            EquipIn(ref _weapon2, _defaultWeapon.CreateItem() as Weapon);
        }
    }
    #endregion

    #region properties
    public Weapon Weapon_1 { get => _weapon1; }
    public Weapon Weapon_2 { get => _weapon2; }

    public HeadGear HeadEquipment { get => _headEquipment; }
    public TorsoGear TorsoEquipment { get => _torsoEquipment; }
    public Boots Boots { get => _boots; }

    public CombatTool CombatTool_1 { get => _combatTool1; }
    public CombatTool CombatTool_2 { get => _combatTool2; }
    public CombatTool CombatTool_3 { get => _combatTool3; }
    public CombatTool CombatTool_4 { get => _combatTool4; }
    public CombatTool CombatTool_5 { get => _combatTool5; private set => _combatTool5 = value; }
    #endregion

    #region equipment interactions
    public void EquipWeapon(Weapon weaponToEquip, WeaponSlot slot)
    {
        if (slot == WeaponSlot.Slot_1)
            EquipIn(ref _weapon1, weaponToEquip);
        else
            EquipIn(ref _weapon2, weaponToEquip);
    }

    public void EquipHeadGear(HeadGear headGear)
        => EquipIn(ref _headEquipment, headGear);

    public void EquipTorsoGear(TorsoGear torsoGear)
        => EquipIn(ref _torsoEquipment, torsoGear);

    public void EquipBoots(Boots boots)
        => EquipIn(ref _boots, boots);

    public void EquipCombatTool(CombatTool combatTool, CombatToolSlot slot)
    {
        switch (slot)
        {
            case CombatToolSlot.Slot_1:
                EquipIn(ref _combatTool1, combatTool);
                break;
            case CombatToolSlot.Slot_2:
                EquipIn(ref _combatTool2, combatTool);
                break;
            case CombatToolSlot.Slot_3:
                EquipIn(ref _combatTool3, combatTool);
                break;
            case CombatToolSlot.Slot_4:
                EquipIn(ref _combatTool4, combatTool);
                break;
            case CombatToolSlot.Slot_5:
                EquipIn(ref _combatTool5, combatTool);
                break;
        }
    }
    #endregion

    #region uneqipment interactions
    public void UnequipWeapon(Weapon weaponToEquip, WeaponSlot slot)
    {
        if (slot == WeaponSlot.Slot_1)
        {
            UnequipOut(ref _weapon1);
            EquipIn(ref _weapon1, _defaultWeapon.CreateItem() as Weapon);
        }
        else
        {
            UnequipOut(ref _weapon2);
            EquipIn(ref _weapon2, _defaultWeapon.CreateItem() as Weapon);
        }
    }

    public void UnequipHeadGear(HeadGear headGear)
        => UnequipOut(ref _headEquipment);

    public void UnequipTorsoGear(TorsoGear torsoGear)
        => UnequipOut(ref _torsoEquipment);

    public void UnequipBoots(Boots boots)
        => UnequipOut(ref _boots);

    public void UnequipCombatTool(CombatToolSlot slot)
    {
        switch (slot)
        {
            case CombatToolSlot.Slot_1:
                UnequipOut(ref _combatTool1);
                break;
            case CombatToolSlot.Slot_2:
                UnequipOut(ref _combatTool2);
                break;
            case CombatToolSlot.Slot_3:
                UnequipOut(ref _combatTool3);
                break;
            case CombatToolSlot.Slot_4:
                UnequipOut(ref _combatTool4);
                break;
            case CombatToolSlot.Slot_5:
                UnequipOut(ref _combatTool5);
                break;
        }
    }
    #endregion

    #region internal operations
    private void EquipIn<T>(ref T slot, T equipment) where T : EquipmentItem
    {
        if (equipment == null)
            return;

        if (slot != null)
            UnequipOut(ref slot);

        slot = equipment;
        slot.Equip(_character);
    }

    private void UnequipOut<T>(ref T slot) where T : EquipmentItem
    {
        slot.Unequip();
        slot = null;
    }

    private void SetupDefaultWeapon()
    {
        if (_weapon1 == null)
            EquipIn(ref _weapon1, _defaultWeapon.CreateItem() as Weapon);
    }

    #endregion
}
