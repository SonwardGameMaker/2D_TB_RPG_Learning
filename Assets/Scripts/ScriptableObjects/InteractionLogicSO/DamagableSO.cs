using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Interactions/DamagableSO")]
internal class DamagableSO : DamagableBaseSO
{
    public override (bool, float) TakeHit(CharacterBlank character, HitDataContainer hit)
    {
        // TODO make dependency from equipment weapon
        float attackValue = Random.Range(0, 100);
        float chanceToHit = CalculateChanceToHit(character.Stats.Dodge.CurrentValue, hit.WeaponSkill);
        bool hitted = attackValue < chanceToHit;
        if (hitted)
        {
            TakeDamage(character, hit.Damage);
        }
        return (hitted, chanceToHit);
    }
    public override void TakeDamage(CharacterBlank character, Damage damage)
    {
        character.Health.ChangeHp(-CalculateDamageTaken(character.IngameParameters, damage));
    }

    public override void TakeHealing(CharacterBlank character, float amount)
    {
        character.Health.ChangeHp(amount);
    }

    private float CalculateChanceToHit(float dodgingSkill, float attackingSkill)
    {
        if (dodgingSkill <= 0) return 95;
        if (attackingSkill <= 0) return 0;

        float coef = attackingSkill * dodgingSkill;
        float result = Mathf.Round(50f + 100f * (coef - 1));

        return result > 95? result : 95; 
    }
}
