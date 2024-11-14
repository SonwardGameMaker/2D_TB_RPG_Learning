using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] DefaultEquipmentSO _defaultEquipmentSO;
    [SerializeField] CharacterInventorySO _inventoryInitSO;

    private EquipmentInventory _equipment;
    private ItemContainer _itemContainer;

    #region init
    // ? - Awake
    private void Start()
    {
        if (_defaultEquipmentSO == null) throw new System.Exception("Default Equiopment doesn't set");

        if (_inventoryInitSO != null)
        {
            _equipment = new EquipmentInventory(GetComponent<CharacterBlank>(), _defaultEquipmentSO, _inventoryInitSO.EquipmentInventorySO);
            _itemContainer = new ItemContainer(_inventoryInitSO.ItemContainerSO);
        }
        else
        {
            Setup();
        }
    }

    public void Setup()
    {
        _equipment = new EquipmentInventory(GetComponent<CharacterBlank>());
        _itemContainer = new ItemContainer();
    }
    #endregion

    #region properties
    public EquipmentInventory Equipment { get => _equipment; }
    public ItemContainer ItemContainer { get => _itemContainer; }
    #endregion
}
