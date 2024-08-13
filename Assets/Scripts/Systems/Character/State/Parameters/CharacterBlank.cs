using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterBlank : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private CharacterStatsSystem _stats;
    [SerializeField] private CharHealth _health;
    [SerializeField] private ApMpSystem _apMpSystem;
    [SerializeField] private OtherParameters _ingameParameters;

    //private CharacterInventory _inventory;

    private List<ParInteraction> _interactions;
    private CharacterCombatStats _combatStats;

    public string Name { get => _name; }
    public CharacterStatsSystem Stats { get => _stats; }
    public CharHealth Health { get => _health; }
    public ApMpSystem ApMpSystem { get => _apMpSystem; }
    public OtherParameters IngameParameters { get => _ingameParameters; }

    #region constructor
    public void Awake()
    {
        _stats = new CharacterStatsSystem();
        _health = new CharHealth();
        _apMpSystem = new ApMpSystem();
        _ingameParameters = new OtherParameters();

        //_inventory = GetComponent<CharacterInventory>();

        _interactions = new List<ParInteraction>();
        _combatStats = new CharacterCombatStats(this);

        _interactions.Add(_health.CreateHealthPointsEffect(_stats.LevelConstAffectHelath()));
        _interactions.Add(_apMpSystem.CreateMpEffect(_stats.AgilityAffectMovementPoints()));
        _interactions.Add(_ingameParameters.CreateMeleeDamageCoefEffect(_stats.StrengthAffectMeleeDamage()));
        _interactions.Add(_ingameParameters.CreateLightMeleeCriticalChanceCoefEffect(_stats.DexterityAffectLightMeleeCritChance()));
        _interactions.Add(_ingameParameters.CreateFirearmCriticalChanceCoefEffect(_stats.PerceptionAffectFirearmCritChance()));
    }
    #endregion

    #region properties
    public CharacterCombatStats CombatStats { get => _combatStats; }
    #endregion

    //public void AddParInteraction(ParInteraction interaction)
    //    => _interactions.Add(interaction);
    //public void AddParInteractionRange(List<ParInteraction> interactions)
    //    => _interactions.AddRange(interactions);
    //public void RemoveParInteraction(ParInteraction interaction)
    //    => _interactions.Remove(interaction);

    public List<string> GetInfo()
    {
        List<string> info = new List<string>();

        info.Add("Name " + _name);
        info.Add($"Health: {_health.CurrentHp}/{_health.MaxHp}");

        return info;
    }
}
