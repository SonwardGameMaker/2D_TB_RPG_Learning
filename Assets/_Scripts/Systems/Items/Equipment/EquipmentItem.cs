using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class EquipmentItem : Item, IDurable
{
    protected List<ItemStatusEffect> _itemStatusEffects;

    protected List<ItemBehaviourBase> _defaultItemBehaviours;
    protected List<ItemBehaviourBase> _itemBehaviours;

    // parameters
    protected bool _isEquipped;
    protected List<ParInteractionCreator> _itemEffectCreators;
    protected List<ParInteraction> _itemEffects;
    protected CharResource _durability;
    protected bool _isBroken;

    #region events
    public event Action ItemBroken;
    public event Action ItemNoMoreBroken;
    #endregion

    #region init
    public EquipmentItem(string name, string description, float price) : base(name, description, price) { }
    public EquipmentItem(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public EquipmentItem(EquipmentItemSO itemSO) : base(itemSO)
    {
        _itemEffectCreators = itemSO.GetParInteractionCreators();
        _durability = itemSO.CreateDurabilityResource();
    }

    ~EquipmentItem()
    {
        _durability.CurrentValChanged -= BrokenStateObserver;
    }

    protected override void Init()
    {
        _itemStatusEffects = new List<ItemStatusEffect>();

        _defaultItemBehaviours = new List<ItemBehaviourBase>();
        _defaultItemBehaviours.Add(new ChangeDurabilityMethodStandart());
        _defaultItemBehaviours.Add(new SetDurabilityMethodStandart());
        _defaultItemBehaviours.Add(new DisplayDurabilityMethodStandart());

        _itemBehaviours = new List<ItemBehaviourBase>();
        ResetItemBehaviours();

        _durability.CurrentValChanged += BrokenStateObserver;
    }
    #endregion

    #region properties
    public CharResourceInfo Durability { get => new CharResourceInfo(_durability); }
    public bool IsBroken { get => _isBroken; }
    #endregion

    #region external interactions
    public void AddItemStatusEffect(ItemStatusEffect effect)
    {
        effect.Setup(this);
        _itemStatusEffects.Add(effect);
        effect.ApplyEffects();
    }

    public void RemoveStatusEffect(ItemStatusEffect effect)
    {
        effect.RemoveEffects();
        _itemStatusEffects?.Remove(effect);
    }

    public virtual void Equip(CharacterBlank bearer)
    {
        if (_isEquipped)
            Unequip();

        _itemEffects.AddRange(_itemEffectCreators.Select(pic => pic.CreateParInteraction(bearer)));
        _isEquipped = true;
    }

    public virtual void Unequip()
    {
        _itemEffects.Clear();

        _isEquipped = false;
    }

    public void ChangeDurability(int amount)
        => (_itemBehaviours.Find(itBeh => itBeh is IChangeDurabilityMethod) as IChangeDurabilityMethod).ChangeDurabilityRealization(_durability, amount);

    public void SetDurability(int amount)
        => (_itemBehaviours.Find(itBeh => itBeh is ISetDurabilityMethod) as ISetDurabilityMethod).SetDurabilityRealization(_durability, amount);
    public string DisplayDurability()
        => (_itemBehaviours.Find(itBeh => itBeh is IDisplayDurabilityMethod) as IDisplayDurabilityMethod).DisplayDurabilityRealization(_durability);

    #endregion

    #region internal operations
    protected void ResetItemBehaviours()
    {
        _itemBehaviours.Clear();
        _itemBehaviours.AddRange(_defaultItemBehaviours);
    }

    protected virtual void AplyBrokeEfects() { }

    protected virtual void ResetBrokeEffects() { }

    protected void BrokenStateObserver()
    {
        if (_durability.CurrentValue <= _durability.MinValue)
        {
            _durability.CurrentValue = _durability.MinValue;
            if (!_isBroken)
            {
                _isBroken = true;
                AplyBrokeEfects();
                ItemBroken?.Invoke();
            } 
        }
        else
        {
            if (_durability.CurrentValue >= _durability.MaxValue)
                _durability.CurrentValue = _durability.MaxValue;
            if (_isBroken)
            {
                _isBroken = false;
                ResetBrokeEffects();
                ItemNoMoreBroken?.Invoke();
            } 
        }
    }
    #endregion
}
