using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponStatsBox : MonoBehaviour
{
    private bool _isEnabled;

    [SerializeField] private CharacterInfo _characterInfo;

    [Header("Stats")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _minDamage;
    [SerializeField] private TMP_Text _maxDamage;
    [SerializeField] private TMP_Text _attackRange;
    [SerializeField] private TMP_Text _damageType;

    public void Setup()
    {
        _characterInfo.CharacterCombatInfo.WeaponChanged += SetText;
        SetText();
    }
    private void OnDestroy()
    {
        _characterInfo.CharacterCombatInfo.WeaponChanged -= SetText;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            if (_isEnabled)
            {
                foreach (Transform t in transform)
                    t.gameObject.SetActive(false);

                _isEnabled = false;
            }
            else
            {
                foreach (Transform t in transform)
                    t.gameObject.SetActive(true);

                _isEnabled = true;
            }
        }
    }

    private void SetText()
    {
        _name.text = _characterInfo.CharacterCombatInfo.CurrentWeapon.Name;
        _minDamage.text = _characterInfo.CharacterCombatInfo.CurrentWeapon.MinDamage.ToString();
        _maxDamage.text = _characterInfo.CharacterCombatInfo.CurrentWeapon.MaxDamage.ToString();
        _attackRange.text = _characterInfo.CharacterCombatInfo.WeaponRange.ToString();
        _damageType.text = _characterInfo.CharacterCombatInfo.CurrentWeapon.DamageType.ToString();
    }
}
