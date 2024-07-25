using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlank : MonoBehaviour
{
    [SerializeField] private CharacterStatsSystem _stats;
    [SerializeField] private CharHealth _health;
    [SerializeField] private ApMpSystem _apMpSystem;
    [SerializeField] private CharacterIngameParameters _ingameParameters;

    private List<ParInteraction> _interactions;

    public CharacterStatsSystem Stats { get => _stats; }
    public CharHealth Health { get => _health; }
    public ApMpSystem ApMpSystem { get => _apMpSystem;  }
    public CharacterIngameParameters CharacterIngameParameters { get => _ingameParameters; }

    public void Awake()
    {
        _stats = new CharacterStatsSystem();
        _health = new CharHealth();
        _apMpSystem = new ApMpSystem();
        _ingameParameters = new CharacterIngameParameters();

        _interactions = new List<ParInteraction>();

        _interactions.Add(_health.CreateHealthPointsEffect(_stats.LevelConstAffectHelath()));
        _interactions.Add(_apMpSystem.CreateMpEffect(_stats.AgilityAffectMovementPoints()));
        _interactions.Add(_ingameParameters.CreateMeleeDamageCoefEffect(_stats.StrengthAffectMeleeDamage()));
        _interactions.Add(_ingameParameters.CreateLightMeleeCriticalChanceCoefEffect(_stats.DexterityAffectLightMeleeCritChance()));
        _interactions.Add(_ingameParameters.CreateFirearmCriticalChanceCoefEffect(_stats.PerceptionAffectFirearmCritChance()));
    }

}
