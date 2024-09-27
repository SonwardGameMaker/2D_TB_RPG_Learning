using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : BaseControllerLogic, IMovable
{
    public float Speed;

    private CharacterInfo _characterInfo;
    private Animator _animator;

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        _characterInfo = characterInfo; 
        _animator = animator;
    }

    public void Move(List<TileNode> path, Action onEndCoroutineAction)
    {
        _coroutine = StartCoroutine(MovePathCoroutine(path, _animator, onEndCoroutineAction));
    }

    public void Rotate(Vector3 targetPosition, Action onEndCoroutineAction)
    {
        _coroutine = StartCoroutine(ChangeRotationCorutine(targetPosition, onEndCoroutineAction));
    }

    #region internal operations
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.parent.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    #endregion

    #region coroutines
    private IEnumerator MovePathCoroutine(List<TileNode> path, Animator animator, Action action)
    {
        float step = Speed * Time.fixedDeltaTime;
        CharacterInfo character = _characterInfo;
        if (character == null) throw new ArgumentNullException("Character");

        animator.SetBool("Walking", true);

        for (int i = 1; i < path.Count; i++)
        {
            if (path[i].TrySetCharacter(character))
            {
                path[i - 1].TryRemoveCharacter();

                //float distance = (path[i].X != path[i - 1].X && path[i].Y != path[i - 1].Y) ? 1.4f : 1.0f;

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
                animator.SetBool(AnimationConstants.WalkingBool, false);
                action?.Invoke();
                yield break;
            }
        }
        animator.SetBool(AnimationConstants.WalkingBool, false);
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
