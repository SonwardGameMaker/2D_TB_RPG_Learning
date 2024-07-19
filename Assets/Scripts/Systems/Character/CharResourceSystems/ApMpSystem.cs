using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApMpSystem
{
    private const string DEFAULT_AP_NAME = "Action Points";
    private const string DEFAULT_MP_NAME = "Movement Points";
    private const float DEFAULT_AP_MAX_VALUE = 50.0f;
    private const float DEFAULT_MP_MAX_VALUE = 20.0f;

    [SerializeField] private CharResource _actionPoints;
    [SerializeField] private CharResource _movementPoints;

    #region constructors and destructor
    public ApMpSystem()
    {
        _actionPoints = new CharResource(DEFAULT_AP_NAME, DEFAULT_AP_MAX_VALUE);
        _movementPoints = new CharResource(DEFAULT_MP_NAME, DEFAULT_MP_MAX_VALUE);

        _actionPoints.MaxValChanged += HandleApEvents;
        _actionPoints.MinValChanged += HandleApEvents;
        _actionPoints.CurrentValChanged += HandleApEvents;

        _movementPoints.MaxValChanged += HandleMpEvents;
        _movementPoints.MinValChanged += HandleMpEvents;
        _movementPoints.CurrentValChanged += HandleMpEvents;
    }
    ~ApMpSystem()
    {
        _actionPoints.MaxValChanged -= HandleApEvents;
        _actionPoints.MinValChanged -= HandleApEvents;
        _actionPoints.CurrentValChanged -= HandleApEvents;

        _movementPoints.MaxValChanged -= HandleMpEvents;
        _movementPoints.MinValChanged -= HandleMpEvents;
        _movementPoints.CurrentValChanged -= HandleMpEvents;
    }
    #endregion

    #region properties
    public float MaxAp { get => _actionPoints.MaxValue; }
    public float MinAp { get => _actionPoints.MinValue; }
    public float CurrentAp { get => _actionPoints.CurrentValue; }

    public float MaxMp {  get => _movementPoints.MaxValue; }
    public float MinMp { get => _movementPoints.MinValue; }
    public float CurrentMp { get => _movementPoints.CurrentValue; }
    #endregion

    #region event handlers
    private void HandleApEvents()
    {
        
    }
    private void HandleMpEvents()
    {

    }
    #endregion

    #region extrnal interation
    public bool TryChangeCurrAp(float amount)
    {
        if (_actionPoints.CurrentValue - amount < 0) return false;
        else
        {
            _actionPoints.CurrentValue -= amount;
            return true;
        }
    }
    public bool TryChangeCurrMp(float amount)
    {
        if (_movementPoints.CurrentValue - amount < 0) return false;
        else
        {
            _movementPoints.CurrentValue -= amount;
            return true;
        }
    }
    public void ResetAp() => _actionPoints.CurrentValue = _actionPoints.MaxValue;
    public void ResetMp() => _movementPoints.CurrentValue = _movementPoints.MaxValue;
    public void ResetAll()
    {
        ResetAp();
        ResetMp();
    }
    public ParInteraction CreateApEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _actionPoints, CalculateLogic);
    public ParInteraction CreateMpEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _movementPoints, CalculateLogic);
    public ParInteraction MpMaxValAffection(CharParameterBase affector, ModifierBaseCreation ModifierCreator)
    {
        void CalculateLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
            => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
                ref affectors,
                ref targets,
                UtilityFunctionsParam.GetCurrentValFloat,
                UtilityFunctionsParam.GetMaxValueMod,
                ModifierCreator);
        return new ParInteraction(affector, _movementPoints, CalculateLogic);
    }
    public ParInteraction ApMaxValAffection(CharParameterBase affector)
        => MpMaxValAffection(affector, AgilityAffectsMp);
    #endregion


    #region calculation methods
    private (float, ModifierType) AgilityAffectsMp(CharParameterBase agility)
    {
        float agilMod = UtilityFunctionsParam.GetCurrentValFloat(agility) - 5;
        float mod = 10;
        float result = mod * 10;
        return new(result, ModifierType.Flat);
    }
    #endregion
}
