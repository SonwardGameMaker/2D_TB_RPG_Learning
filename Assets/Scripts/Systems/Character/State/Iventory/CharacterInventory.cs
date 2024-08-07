using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private EquipmentSlots _equipmentSlots;
    [SerializeField] private CharacterInventorySystem _inventory;

    private CharacterBlank _bearer;

    public void Awake()
    {
        _bearer = GetComponent<CharacterBlank>();

        _equipmentSlots.Init(_bearer);
    }

    public EquipmentSlots EquipmentSlots { get => _equipmentSlots; }
    public CharacterInventorySystem Inventory { get => _inventory; }
}
