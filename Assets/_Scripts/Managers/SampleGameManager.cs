using UnityEngine;

public class SampleGameManager : MonoBehaviour
{
    [SerializeField] private PlayerIngameController _player;
        
    private GameObject _characters;

    private void Awake()
    {
        _characters = GameObject.Find("Characters");

        GameObject enemyDummy = _characters.transform.Find("Enemies").GetChild(0).gameObject;
        enemyDummy.GetComponentInChildren<IDamagable>().CharacterHitted += DummyHitted;
        enemyDummy.GetComponent<CharacterInfo>().CharDeath += DummyDeath;
    }

    private void Update()
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

    protected virtual void TryHitEnemy()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Characters")
            {
                _player.GetComponent<CharacterIngameController>().Attack(hit.collider.GetComponentInChildren<IDamagable>());
            }
        }
    }
    protected virtual void DamageEnemy()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Inflicting 20 mechanical damage");
                hit.collider.gameObject.GetComponentInChildren<IDamagable>().TakeDamage(new Damage(20, DamageType.Mechanical));
            }
        }
    }
    protected virtual void GetCharacterInfo()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.GetComponent<CharacterInfo>().GetBaseInfoString());
        }
    }

    private void DummyHitted(CharacterInfo dummy, bool isSuccesful, float chance, Damage damage)
    {
        Debug.Log($"Inflicting {damage.Amount} {damage.DamageType} damage");
        Debug.Log($"{(isSuccesful ? "H" : "Don't h")}itted with chance {chance}%");
        Debug.Log("//----------------------------------------------------------------------------------------");
    }

    private void DummyDeath(GameObject character)
    {
        Destroy(character);
        Debug.Log($"{character.GetComponent<CharacterBlank>().Name} is dead");
    }
}
