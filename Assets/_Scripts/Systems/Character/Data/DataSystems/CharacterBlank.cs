using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlank : MonoBehaviour
{
    [SerializeField] private StatsInitSO StatsInitSO;

    [SerializeField] private string _name;
    [SerializeField] private CharacterStatsSystem _stats;
    [SerializeField] private CharHealthSystem _health;
    [SerializeField] private ApMpSystem _apMpSystem;
    [SerializeField] private OtherParameters _ingameParameters;

    private List<ParInteraction> _interactions;

    public string Name { get => _name; }
    public CharacterStatsSystem Stats { get => _stats; }
    public CharHealthSystem Health { get => _health; }
    public ApMpSystem ApMpSystem { get => _apMpSystem; }
    public OtherParameters IngameParameters { get => _ingameParameters; }

    #region constructor
    public void Setup()
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

        GetComponent<CharacterInventory>().Setup();

        if (TryGetComponent(out CharacterStateChangingDebug stateDebug))
            stateDebug.Setup(this);
        else
            GetComponent<CharacterIngameController>().Setup();

        GetComponent<ControllerManagerBase>().Setup();

        foreach (BehaviourScriptBase charbehaviour in GetComponentInChildren<BehaviourLogicManager>().GetBehaviours())
            charbehaviour.Setup(this);
        foreach (CharInteractionBase charInteractionBase in GetComponentInChildren<InteractionBehaviourManager>().GetInteractionLogics())
            charInteractionBase.Setup(this);
    }
    #endregion
}
