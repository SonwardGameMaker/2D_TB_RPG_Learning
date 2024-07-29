using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventorySystem
{
    private List<Item> _inventory;

    public void Start()
    {
        _inventory = new List<Item>();
    }

    public Item GetItem(int index)
        => _inventory[index];

    public void AddItem(Item item)
        => _inventory.Add(item);

    public void InsertItem(int index, Item item)
        => _inventory.Insert(index, item);

    public bool RemoveItem(Item item)
        => _inventory.Remove(item);

    public void RemoveItem(int index)
        => _inventory.RemoveAt(index);
}
