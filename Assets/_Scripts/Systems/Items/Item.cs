using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item 
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Sprite ImageUI { get; set; }
    
    public bool CanBeSold { get; protected set; }
    public virtual float Price { get; protected set; }

    #region init
    public Item() { Init(); }
    public Item(string name, string description, float price)
    {
        Name = name;
        Description = description;
        Price = price;

        Init();
    }
    public Item(string name, string description, float price, Sprite imageUI) : this(name, description, price)
    {
        ImageUI = imageUI;

        Init();
    }
    public Item(ItemBaseSO itemSO)
    {
        Name = itemSO.Name;
        Description = itemSO.Description;
        ImageUI = itemSO.ImageUI;
        CanBeSold = itemSO.CanBeSold;
        Price = itemSO.Price;

        Init();
    }

    protected virtual void Init() { }
    #endregion
}
