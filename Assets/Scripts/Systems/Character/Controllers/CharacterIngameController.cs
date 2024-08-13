using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    private CharacterInfo _character;

    private void Start()
    {
        _character = GetComponent<CharacterInfo>();
    }

    public void Hit(IDamagable target)
    {
        target.TakeHit(_character.CalculateHitData());
    }
}
