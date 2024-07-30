using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    public CharacterBlank Bearer { get; }

    public void Equip(CharacterBlank character);
    public void Unequip();
}
