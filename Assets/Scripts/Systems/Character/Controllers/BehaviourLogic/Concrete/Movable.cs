using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour, IMovable
{
    public float Speed;

    private CharacterInfo _characterInfo;

    private Coroutine _coroutine;

    public void Setup(CharacterInfo characterInfo)
    {
        _characterInfo = characterInfo; 
    }

    public void Move(List<TileNode> path, Action onEndCoroutineAction)
    {
        _coroutine = StartCoroutine(MovePathCoroutine(path, onEndCoroutineAction));
    }

    public void Rotate(Vector3 targetPosition, Action onEndCoroutineAction)
    {
        _coroutine = StartCoroutine(ChangeRotationCorutine(targetPosition, onEndCoroutineAction));
    }

    #region internal operations
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    #endregion

    #region coroutines
    private IEnumerator MovePathCoroutine(List<TileNode> path, Action action)
    {
        float step = Speed * Time.deltaTime;
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
    #endregion
}
