using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlank : MonoBehaviour
{
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private CharHealth _health;
    [SerializeField] private ApMpSystem _apMpSystem;

    private List<ParInteraction> _interactions;

    public CharacterStats Stats { get { return _stats; } }
    public CharHealth Health { get { return _health; } }
    public ApMpSystem ApMpSystem { get { return _apMpSystem; } }

    public void Awake()
    {
        _stats = new CharacterStats();
        _health = new CharHealth();
        _apMpSystem = new ApMpSystem();
        _interactions = new List<ParInteraction>();

        //_interactions.Add(_health.LevelConstAffectHp(_stats.Level, _stats.Constitution));
        _interactions.Add(_health.CreateHealthPointsEffect(
            new List<CharParameterBase> { _stats.Level, _stats.Constitution },
            _stats.LevelConstAffectHelath));
        _interactions.Add(_apMpSystem.CreateMpEffect(_stats.Agility, _stats.AgilityAffectMovementPoints));
    }

}
