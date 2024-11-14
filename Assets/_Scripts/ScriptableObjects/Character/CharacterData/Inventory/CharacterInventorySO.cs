using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Character/CharacterData/Inventory/CharacterInventorySO")]
public class CharacterInventorySO : ScriptableObject
{
    [SerializeField] private EquipmentInventorySO _equipmentInventorySO;
    [SerializeField] private ItemContainerSO _ItemContainerSO;

    public EquipmentInventorySO EquipmentInventorySO { get => _equipmentInventorySO; }
    public ItemContainerSO ItemContainerSO { get => _ItemContainerSO; }
}
