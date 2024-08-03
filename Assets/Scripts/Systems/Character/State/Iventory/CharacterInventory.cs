using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public Weapon MainHand;
    public Weapon SecondHand;

    public Armor HeadEquipment;
    public Armor BodyEquipment;
    public Armor FeetEquipment;

    public void Awake()
    {
        HeadEquipment.Init();
        BodyEquipment.Init();
        FeetEquipment.Init();
    }

    [SerializeField] private CharacterInventorySystem _inventory;
}
