using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentSlots
{
    public Weapon MainHand;
    public Weapon SecondHand;

    public Armor HeadEquipment;
    public Armor BodyEquipment;
    public Armor FeetEquipment;

    public void Init(CharacterBlank bearer)
    {
        MainHand.Init(bearer);
        SecondHand.Init(bearer);

        HeadEquipment.Init(bearer);
        BodyEquipment.Init(bearer);
        FeetEquipment.Init(bearer);
    }
}
