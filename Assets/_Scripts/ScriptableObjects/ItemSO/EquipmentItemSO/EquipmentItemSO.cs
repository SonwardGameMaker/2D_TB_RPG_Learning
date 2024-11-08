using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentItemSO : ItemBaseSO
{
    [SerializeField] protected List<StatEffectCreator> _effectsOnStats;
    [SerializeField] protected List<ResourceAffectorCreator> _effectsOnResourses;

    public IReadOnlyList<StatEffectCreator> EffectsOnStats { get => _effectsOnStats; }
    public IReadOnlyList<ResourceAffectorCreator> EffectsOnResourses { get => _effectsOnResourses; }

    public virtual List<ParInteractionCreator> GetParInteractionCreators()
    {
        List<ParInteractionCreator> result = new List<ParInteractionCreator>();
        result.AddRange(_effectsOnStats.Select(sec => new StatEffectCreator(sec)));
        result.AddRange(_effectsOnResourses.Select(rec => new ResourceAffectorCreator(rec)));
        return result;
    }
}
