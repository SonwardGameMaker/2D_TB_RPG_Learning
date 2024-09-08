using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    //[SerializeField] MovingLogicSO MovingLogicSO;
    [SerializeField] private float speed = 5f;

    internal CharacterBlank _character;
    protected CharacterInfo _characterInfo;
    protected Coroutine _coroutine;

    public void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _characterInfo = GetComponent<CharacterInfo>();
    }

    public void TryHit(IDamagable target)
    {
        HitDataContainer hitData = _characterInfo.CalculateHitData();

        target.TakeHit(hitData);
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void Walk(List<PathfinderNodeBase> path)
    {
        if (path == null) throw new Exception("Path is null");

        if (_coroutine != null)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        List<TileNode> nodePath = path.Select(pnb => pnb.TargetNode).ToList();
        if (nodePath == null) throw new Exception("NodePath is null");

        _coroutine = StartCoroutine(MovePathCoroutine(nodePath, () => _coroutine = null));
    }

    public void WalkAndAttack(List<PathfinderNodeBase> path, IDamagable target)
    {
        if (path == null) throw new Exception("Path is null");

        if (_coroutine != null)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        List<TileNode> nodePath = path.Select(pnb => pnb.TargetNode).ToList();
        if (nodePath == null) throw new Exception("NodePath is null");

        _coroutine = StartCoroutine(MovePathCoroutine(nodePath, 
            () => _coroutine = StartCoroutine(AttackCorutine(target, () => _coroutine = null))));
    }

    #region coroutines
    private IEnumerator MovePathCoroutine(List<TileNode> path, Action action)
    {
        float step = speed * Time.deltaTime;
        CharacterInfo character = _characterInfo;
        if (character == null) throw new ArgumentNullException("Character");

        for (int i = 1; i < path.Count; i++)
        {
            if (path[i].TrySetCharacter(character))
            {
                path[i - 1].TryRemoveCharacter();
                Vector3 targetPosition = path[i].WorldPositionOfCenter;
                while (!character.transform.position.Equals(targetPosition))
                {
                    RotateTowards(targetPosition);
                    character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, step);
                    yield return null;
                }
            }
            else
            {
                action?.Invoke();
                yield break;
            }
        }
        action?.Invoke();
    }

    private IEnumerator ChangeRotationCorutine(Vector3 targetPosition, Action action)
    {
        RotateTowards(targetPosition);
        action?.Invoke();
        yield return null;
    }

    private IEnumerator AttackCorutine(IDamagable target, Action action)
    {
        TryHit(target);
        action?.Invoke();
        yield break; ;
    }
    #endregion
}
