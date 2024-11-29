using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentItemSO : ItemSO
{
    [SerializeField] protected int _currentDurability;
    [SerializeField] protected int _maxDurability;
    [SerializeField] protected int _minDurability = 0;

    [SerializeField] protected List<StatEffectCreator> _effectsOnStats;
    [SerializeField] protected List<ResourceAffectorCreator> _effectsOnResourses;

    public EquipmentItemSO() { }
    public EquipmentItemSO(int currentDurability, int maxDurability, int minDurability, List<StatEffectCreator> effectsOnStats, List<ResourceAffectorCreator> effectsOnResourses)
    {
        _currentDurability = currentDurability;
        _maxDurability = maxDurability;
        _minDurability = minDurability;
        _effectsOnStats = effectsOnStats;
        _effectsOnResourses = effectsOnResourses;
    }


    #region properties
    public int CurrentDurability { get => _currentDurability; }
    public int MaxDurability { get => _maxDurability; }
    public IReadOnlyList<StatEffectCreator> EffectsOnStats { get => _effectsOnStats; }
    public IReadOnlyList<ResourceAffectorCreator> EffectsOnResourses { get => _effectsOnResourses; }
    #endregion

    #region external interactions
    public virtual List<ParInteractionCreator> GetParInteractionCreators()
    {
        List<ParInteractionCreator> result = new List<ParInteractionCreator>();
        if (_effectsOnStats != null && _effectsOnStats.Count > 0)
            result.AddRange(_effectsOnStats.Select(sec => new StatEffectCreator(sec)));
        if (_effectsOnResourses != null && _effectsOnResourses.Count > 0)
            result.AddRange(_effectsOnResourses.Select(rec => new ResourceAffectorCreator(rec)));
        return result;
    }

    public CharResource CreateDurabilityResource()
        => new CharResource("Durability", _maxDurability, _minDurability, _currentDurability);

    public override Item CreateItem() => new EquipmentItem(this);
    #endregion
}
