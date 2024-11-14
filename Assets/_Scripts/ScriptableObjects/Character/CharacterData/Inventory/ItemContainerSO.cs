using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Character/CharacterData/Inventory/ItemContainerSO")]
public class ItemContainerSO : ScriptableObject
{
    [SerializeField] private List<ItemSO> _itemSOs;

    public List<Item> CreateItems()
        => _itemSOs.Select(itemSO => itemSO.CreateItem()).ToList();
}
