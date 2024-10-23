using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item 
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Sprite ImageUI { get; set; }
    public int Amount { get; protected set; }
    public virtual float Price { get; protected set; }

    public Item() { }
    public Item(string name, string description, float price, int amount = 1)
    {
        Name = name;
        Description = description;
        Amount = amount;
        Price = price;
    }
    public Item(string name, string description, float price, Sprite imageUI, int amount = 1) : this(name, description, price, amount)
    {
        ImageUI = imageUI;
    }
}
