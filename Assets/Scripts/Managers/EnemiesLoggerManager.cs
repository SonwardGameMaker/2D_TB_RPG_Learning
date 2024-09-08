using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesLoggerManager : MonoBehaviour
{
    [SerializeField] private CharactersContainerOnScene _enemies;

    void Start()
    {
        List<CharacterInfo> tempEnemies = _enemies.GetCharacters().Select(cr => cr.Item2).ToList();

        foreach (CharacterInfo enemy in tempEnemies)
        {
            enemy.GetComponentInChildren<IDamagable>().CharacterHitted += EnemyHittedLogging;
        }
    }

    private void EnemyHittedLogging(CharacterInfo enemy, bool isSuccesful, float chance, Damage damage)
    {
        Debug.Log($"Target: {enemy.name}" +
            $"\nInflicting {damage.Amount} {damage.DamageType} damage" +
            $"\n{(isSuccesful ? "H" : "Don't h")}itted with chance {chance}%" +
            $"\n//----------------------------------------------------------------------------------------");
        //Debug.Log($"Inflicting {damage.Amount} {damage.DamageType} damage");
        //Debug.Log($"{(isSuccesful ? "H" : "Don't h")}itted with chance {chance}%");
        //Debug.Log("//----------------------------------------------------------------------------------------");
    }
}
