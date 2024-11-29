using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Character/CharacterData/Inventory/EquipmentInventorySO")]
public class EquipmentInventorySO : ScriptableObject
{
    [SerializeField] private WeaponSO _weapon1;
    [SerializeField] private WeaponSO _weapon2;

    [SerializeField] private HeadGearSO _headEquipment;
    [SerializeField] private TorsoGearSO _torsoEquipment;
    [SerializeField] private BootsSO _boots;

    [SerializeField] private CombatToolSO _combatTool1;
    [SerializeField] private CombatToolSO _combatTool2;
    [SerializeField] private CombatToolSO _combatTool3;
    [SerializeField] private CombatToolSO _combatTool4;
    [SerializeField] private CombatToolSO _combatTool5;

    #region properties
    public WeaponSO Weapon_1 { get => GetEquipmentItem(_weapon1); }
    public WeaponSO Weapon_2 { get => GetEquipmentItem(_weapon2); }

    public HeadGearSO HeadEquipment { get => GetEquipmentItem(_headEquipment); }
    public TorsoGearSO TorsoEquipment { get => GetEquipmentItem(_torsoEquipment); }
    public BootsSO Boots { get => GetEquipmentItem(_boots); }

    public CombatToolSO CombatTool_1 { get => GetEquipmentItem(_combatTool1); }
    public CombatToolSO CombatTool_2 { get => GetEquipmentItem(_combatTool2); }
    public CombatToolSO CombatTool_3 { get => GetEquipmentItem(_combatTool3); }
    public CombatToolSO CombatTool_4 { get => GetEquipmentItem(_combatTool4); }
    public CombatToolSO CombatTool_5 { get => GetEquipmentItem(_combatTool5); }
    #endregion

    private T GetEquipmentItem<T>(T item) where T : EquipmentItemSO
    {
        if (item == null)
            return CreateInstance<T>();
        else
            return item;
    }
}
