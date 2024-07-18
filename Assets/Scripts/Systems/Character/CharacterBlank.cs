using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlank : MonoBehaviour
{
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private CharHealth _health;

    private List<ParInteraction> _interactions;

    public CharacterStats Stats { get { return _stats; } }
    public CharHealth Health { get { return _health; } }

    public void Awake()
    {
        _stats = new CharacterStats();
        _health = new CharHealth();
        _interactions = new List<ParInteraction>();

        _interactions.Add(_health.LevelConstAffectHp(_stats.Level, _stats.Constitution));
    }

}
