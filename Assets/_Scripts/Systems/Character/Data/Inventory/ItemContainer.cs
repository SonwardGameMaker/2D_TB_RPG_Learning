using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemContainer
{
    private List<Item> _items;
    private float _itemWeight;

    #region init
    public ItemContainer() 
    {
        _items = new List<Item>();
        _itemWeight = 0f;
    }

    public ItemContainer(List<Item> items)
    {
        _items = items;
        _itemWeight = CalculateItemWeight();
    }

    public ItemContainer(ItemContainerSO itemContainerSO) : this(itemContainerSO.CreateItems()) { }
    #endregion

    #region external interaction
    public void Add(Item item)
    {
        _items.Add(item);
        _itemWeight += item.Weight;
    }

    public void Remove(Item item)
    { 
        _items.Remove(item);
        _itemWeight -= item.Weight;
    }
    #endregion

    #region internal operations
    private float CalculateItemWeight()
        => _items.Sum(it => it.Weight);
    #endregion
}
