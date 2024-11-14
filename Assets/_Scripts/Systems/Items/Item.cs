using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item 
{
    [SerializeField] protected string _name;

    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Sprite ImageUI { get; set; }
    
    public bool CanBeSold { get; protected set; }
    public virtual float Price { get; protected set; }
    public float Weight { get; protected set; }

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
    public Item(ItemSO itemSO)
    {
        Name = itemSO.Name;
        Description = itemSO.Description;
        ImageUI = itemSO.ImageUI;
        CanBeSold = itemSO.CanBeSold;
        Price = itemSO.Price;
        Weight = itemSO.Weight;

        Init();
    }

    protected virtual void Init() { }
    #endregion
}
