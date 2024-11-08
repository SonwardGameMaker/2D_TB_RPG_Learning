using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBaseSO : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _imageUI;
    [SerializeField] protected float _price;
    [SerializeField] protected bool _canBeSold;

    public string Name { get => _name; }
    public string Description { get => _description; }
    public Sprite ImageUI { get => _imageUI; }
    public float Price { get => _price; }
    public bool CanBeSold { get => _canBeSold; }
}
