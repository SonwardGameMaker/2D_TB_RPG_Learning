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
    public WeaponSO Weapon_1 { get => _weapon1; }
    public WeaponSO Weapon_2 { get => _weapon2; }

    public HeadGearSO HeadEquipment { get => _headEquipment; }
    public TorsoGearSO TorsoEquipment { get => _torsoEquipment; }
    public BootsSO Boots { get => _boots; }

    public CombatToolSO CombatTool_1 { get => _combatTool1; }
    public CombatToolSO CombatTool_2 { get => _combatTool2; }
    public CombatToolSO CombatTool_3 { get => _combatTool3; }
    public CombatToolSO CombatTool_4 { get => _combatTool4; }
    public CombatToolSO CombatTool_5 { get => _combatTool5; }
    #endregion
}
