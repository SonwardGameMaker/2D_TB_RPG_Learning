using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterBlank : MonoBehaviour
{
    [SerializeField] private StatsInitSO StatsInitSO;

    [SerializeField] private string _name;
    [SerializeField] private CharacterStatsSystem _stats;
    [SerializeField] private CharHealthSystem _health;
    [SerializeField] private ApMpSystem _apMpSystem;
    [SerializeField] private OtherParameters _ingameParameters;

    //private CharacterInventory _inventory;

    private List<ParInteraction> _interactions;

    public string Name { get => _name; }
    public CharacterStatsSystem Stats { get => _stats; }
    public CharHealthSystem Health { get => _health; }
    public ApMpSystem ApMpSystem { get => _apMpSystem; }
    public OtherParameters IngameParameters { get => _ingameParameters; }

    #region constructor
    public void Awake()
    {
        _stats = new CharacterStatsSystem(StatsInitSO);
        _health = new CharHealthSystem();
        _apMpSystem = new ApMpSystem();
        _ingameParameters = new OtherParameters();

        _interactions = new List<ParInteraction>();

        _interactions.Add(_health.CreateHealthPointsEffect(_stats.LevelConstAffectHelath()));
        _interactions.Add(_apMpSystem.CreateMpEffect(_stats.AgilityAffectMovementPoints()));
        _interactions.Add(_ingameParameters.CreateMeleeDamageCoefEffect(_stats.StrengthAffectMeleeDamage()));
        _interactions.Add(_ingameParameters.CreateLightMeleeCriticalChanceCoefEffect(_stats.DexterityAffectLightMeleeCritChance()));
        _interactions.Add(_ingameParameters.CreateFirearmCriticalChanceCoefEffect(_stats.PerceptionAffectFirearmCritChance()));


        GetComponent<CharacterInfo>().SetUp(this);

        // Debug
        if (TryGetComponent(out CharacterStateChangingDebug stateDebug))
            stateDebug.Setup(this);
    }
    #endregion
}
