using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SampleGameManager : MonoBehaviour
{
    [SerializeField] private CharacterInventory _player;
        
    private GameObject _characters;

    private void Awake()
    {
        _characters = GameObject.Find("Characters");

        GameObject enemyDummy = _characters.transform.Find("Enemies").GetChild(0).gameObject;
        enemyDummy.GetComponent<HumanCharacterInteractable>().CharDeath += DummyDeath;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryHitEnemy();
        }
        if (Input.GetMouseButtonDown(1))
        {
            GetCharacterInfo();
        }
    }

    private void TryHitEnemy()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                HitDataContainer characterHit = _player.EquipmentSlots.MainHand.CalculateHitData;
                (bool, float) hittedWithChance = hit.collider.gameObject
                    .GetComponent<HumanCharacterInteractable>().TakeHit(characterHit);
                Debug.Log($"Inflicting {characterHit.Damage.Amount} {characterHit.Damage.DamageType} damage");
                Debug.Log($"{(hittedWithChance.Item1? "H" : "Don't h")}itted with chance {hittedWithChance.Item2}%");
                Debug.Log("//----------------------------------------------------------------------------------------");
            }
        }
    }
    private void DamageEnemy()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Inflicting 20 mechanical damage");
                hit.collider.gameObject.GetComponent<HumanCharacterInteractable>().TakeDamage(new Damage(20, DamageType.Mechanical));
            }
        }
    }
    private void GetCharacterInfo()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            foreach(string str in hit.collider.GetComponent<CharacterBlank>().GetInfo())
            {
                Debug.Log(str);
            }
        }
    }

    private void DummyDeath(GameObject character)
    {
        Destroy(character);
        Debug.Log($"{character.GetComponent<CharacterBlank>().Name} is dead");
    }
}
