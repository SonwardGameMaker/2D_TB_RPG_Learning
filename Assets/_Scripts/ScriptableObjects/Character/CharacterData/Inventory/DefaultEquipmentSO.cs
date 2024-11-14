using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Character/CharacterData/Inventory/DefaultEquipmentSO")]
public class DefaultEquipmentSO : ScriptableObject
{
    [SerializeField] private List<WeaponSO> _defaultWeaponSOs;
    [SerializeField] private List<ArmourGearSO> _defaultArmourGearSOs;
    [SerializeField] private List<CombatToolSO> _defaultCombatToolSOs;

    public List<WeaponSO> DefaultWeaponSOs { get => _defaultWeaponSOs; }
    public List<ArmourGearSO> DefaultArmourGearSOs { get => _defaultArmourGearSOs; }
    public List<CombatToolSO> DefaultCombatToolSOs { get => _defaultCombatToolSOs;}
}
