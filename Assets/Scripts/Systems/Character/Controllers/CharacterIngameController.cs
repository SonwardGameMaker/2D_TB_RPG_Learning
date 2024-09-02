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
    protected Coroutine _walkCoroutine;

    public void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _characterInfo = GetComponent<CharacterInfo>();
    }

    public void Hit(IDamagable target)
    {
        target.TakeHit(_characterInfo.CalculateHitData());
    }

    public void Walk(List<PathfinderNodeBase> path)
    {
        if (path == null) throw new Exception("Path is null");

        if (_walkCoroutine != null)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        List<TileNode> nodePath = path.Select(pnb => pnb.TargetNode).ToList();
        if (nodePath == null) throw new Exception("NodePath is null");

        _walkCoroutine = StartCoroutine(MovePathCorutine(nodePath, () => _walkCoroutine = null));
    }

    #region internal calculations
    private IEnumerator MovePathCorutine(List<TileNode> path, Action action)
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
                    character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, step);
                    yield return null;
                }
            }
            else
            {
                //_walkCoroutine = null;
                action?.Invoke();
                yield break;
            }
        }
        action?.Invoke();
        //_walkCoroutine = null;
    }
    #endregion
}
