using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate List<ParInteraction> AffectCharParameters(CharacterBlank characterBlank);
public class Equipment : Item, IDurable
{
    private CharResource _durability;
    private bool _isBroken;

    private AffectCharParameters _affectionLogic;
    private List<ParInteraction> _parInteractions;
    private CharacterBlank _bearer;

    #region constructors
    public Equipment(
        string name,
        string description,
        float price,
        float maxDurabilty,
        AffectCharParameters AffectionLogic,
        Sprite spriteUI) : base(name, description, price, spriteUI)
    {
        _durability = new CharResource("Durability", maxDurabilty);
        _isBroken = false;
        _affectionLogic = AffectionLogic;
        _parInteractions = new List<ParInteraction>();
    }
    public Equipment(
    string name,
    string description,
    float price,
    float maxDurabilty,
    AffectCharParameters AffectionLogic) 
        : this(name, description, price, maxDurabilty, AffectionLogic, null) 
    { }
    #endregion

    #region properties
    public float MaxDurability { get => _durability.MaxValue; }
    public float CurrentDurability { get => _durability.CurrentValue; }
    public bool IsBroken { get => _isBroken; }
    #endregion

    #region events
    public event Action Brokes;
    public event Action Repairs;
    #endregion

    #region external interaction
    public void ChangeDurability(float amount)
    {
        _durability.CurrentValue += amount;

        if (_isBroken = CheckIfBroken())
            _durability.CurrentValue = 0;
    }
    public void Equip(CharacterBlank character)
    {
        if (_bearer != null || _bearer != character)
            Unequip();
        
        _bearer = character;
        _parInteractions = _affectionLogic(_bearer);
        character.AddParInteractionRange(_parInteractions);
    }
    public void Unequip()
    {
        foreach (ParInteraction interaction in _parInteractions)
            _bearer.RemoveParInteraction(interaction);
        _parInteractions.Clear();
        _bearer = null;
    }
    #endregion

    #region calculation methods
    private bool CheckIfBroken()
    {
        if (_durability.CurrentValue <= 0)
        {
            if (!_isBroken)
                Brokes?.Invoke();
            return true;
        }
        else
        {
            if (_isBroken)
                Repairs?.Invoke();
            return false;
        }
    }
    #endregion
}
