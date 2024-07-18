using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlank : MonoBehaviour
{
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private CharHealth _health;

    public CharacterStats Stats { get { return _stats; } }
    public CharHealth Health { get { return _health; } }

    public void Awake()
    {
        _stats = new CharacterStats();
        _health = new CharHealth();

        ParInteraction levelConstToHp = new ParInteraction(
            new List<CharParameterBase> { _stats.Level, _stats.Constitution },
            _health,
            _health.LevelConstAffectHp);
        _stats.AddAffector(levelConstToHp);
    }

}
