using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsInfo
{
    public CharacterStatsInfo(CharacterStatsSystem stats)
    {
        Level = new StatInfo(stats.Level);

        // Attributes
        Strength = new StatInfo(stats.Strength);
        Dexterity = new StatInfo(stats.Dexterity);
        Agility = new StatInfo(stats.Agility);
        Constitution = new StatInfo(stats.Constitution);
        Perception = new StatInfo(stats.Perception);
        Charisma = new StatInfo(stats.Charisma);
        Intelligence = new StatInfo(stats.Intelligence);

        // Skills
        LightFirearm = new StatInfo(stats.LightFirearm);
        Firearm = new StatInfo(stats.Firearm);
        Melee = new StatInfo(stats.Melee);
        HeavyMelee = new StatInfo(stats.HeavyMelee);
        Dodge = new StatInfo(stats.Dodge);
        Stealth = new StatInfo(stats.Stealth);
        Hacking = new StatInfo(stats.Hacking);
        Lockpicking = new StatInfo(stats.Lockpicking);
        Pickpocketing = new StatInfo(stats.Pickpocketing);
        Persuasion = new StatInfo(stats.Persuasion);
        Intimidation = new StatInfo(stats.Intimidation);
        Mercantile = new StatInfo(stats.Mercantile);
    }

    #region properties
    public StatInfo Level { get; private set; }

    // Attributes
    public StatInfo Strength { get; private set; }
    public StatInfo Dexterity { get; private set; }
    public StatInfo Agility { get; private set; }
    public StatInfo Constitution { get; private set; }
    public StatInfo Perception { get; private set; }
    public StatInfo Charisma { get; private set; }
    public StatInfo Intelligence { get; private set; }

    // Skills
    public StatInfo LightFirearm { get; private set; }
    public StatInfo Firearm { get; private set; }
    public StatInfo Melee { get; private set; }
    public StatInfo HeavyMelee { get; private set; }
    public StatInfo Dodge { get; private set; }
    public StatInfo Stealth { get; private set; }
    public StatInfo Hacking { get; private set; }
    public StatInfo Lockpicking { get; private set; }
    public StatInfo Pickpocketing { get; private set; }
    public StatInfo Persuasion { get; private set; }
    public StatInfo Intimidation { get; private set; }
    public StatInfo Mercantile { get; private set; }
    #endregion
}
