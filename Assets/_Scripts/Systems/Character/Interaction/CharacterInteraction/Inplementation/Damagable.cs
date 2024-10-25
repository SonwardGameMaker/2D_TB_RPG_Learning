using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Damagable : MonoBehaviour, IDamagable
{
    private CharacterBlank _thisCharacter;

    void Start()
    {
        _thisCharacter = GetComponentInParent<CharacterBlank>();
    }

    public event Action<CharacterInfo, bool, float, Damage> CharacterHitted;

    #region external interactions
    public bool TakeHit(HitDataContainer hit)
    {
        // TODO make dependency from equipment weapon
        float attackValue = UnityEngine.Random.Range(0, 100);
        float chanceToHit = CalculateChanceToHit(_thisCharacter.Stats.Dodge.CurrentValue, hit.WeaponSkill);
        bool hitted = attackValue < chanceToHit;
        Damage damageTaken = new Damage(0, hit.Damage.DamageType);
        if (hitted)
        {
            damageTaken = TakeDamage(hit.Damage);
        }
        CharacterHitted?.Invoke(GetComponentInParent<CharacterInfo>(), hitted, chanceToHit, damageTaken);
        return hitted;
    }
    public Damage TakeDamage(Damage damage)
    {
        Damage damageTaken = new Damage(CalculateDamageTaken(damage), damage.DamageType);
        _thisCharacter.Health.ChangeHp(-damageTaken.Amount);
        return damageTaken;
    }
    public void TakeHealing(float amount)
    {
        _thisCharacter.Health.ChangeHp(amount);
    }
    #endregion

    #region internal calculations
    private float CalculateChanceToHit(float dodgingSkill, float attackingSkill)
    {
        if (dodgingSkill <= 0) return 95;
        if (attackingSkill <= 0) return 0;

        float coef = attackingSkill / dodgingSkill;
        float result = Mathf.Round(50.0f + 100.0f * (coef - 1.0f));

        return result > 95 ? 95 : result;
    }
    protected float CalculateDamageTaken(Damage damage)
    {
        DamageResistance currentResistance = _thisCharacter.IngameParameters.GetDamageResistanceByType(damage.DamageType);

        float trashholdedDamage = damage.Amount - currentResistance.Trashhold;
        trashholdedDamage = trashholdedDamage >= 0 ? trashholdedDamage : 0;

        float mitigatedDamage = damage.Amount - damage.Amount * currentResistance.Mitigation;
        mitigatedDamage = mitigatedDamage >= 0 ? mitigatedDamage : 0;

        return mitigatedDamage < trashholdedDamage ? mitigatedDamage : trashholdedDamage;
    }
    #endregion
}
