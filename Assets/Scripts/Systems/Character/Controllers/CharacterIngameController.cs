using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    internal CharacterBlank _character;
    protected CharacterInfo _characterInfo;

    public void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _characterInfo = GetComponent<CharacterInfo>();
    }

    public void Hit(IDamagable target)
    {
        target.TakeHit(_characterInfo.CalculateHitData());
    }
}
