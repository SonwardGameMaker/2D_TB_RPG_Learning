using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Movable : BaseControllerLogic, IMovable
{
    public float Speed;

    private CharacterInfo _characterInfo;
    private ApMpSystem _apMpSystem;
    private Animator _animator;

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        _characterInfo = characterInfo; 
        _apMpSystem = characterInfo.GetComponent<CharacterBlank>().ApMpSystem;
        _animator = animator;
    }

    #region external interactions
    public (bool, string) Move(List<PathfinderNodeBase> path, Action onEndCoroutineAction)
    {
        //if (EnoughtMoveResources(PathCost(path)))
        //{
        //    _coroutine = StartCoroutine(MovePathCoroutine(path.Select(pn => pn.TargetNode).ToList(),
        //        _animator, onEndCoroutineAction));
        //    return new(true, "The path was completed successfully");
        //}
        //else
        //{
        //    onEndCoroutineAction?.Invoke();
        //    return new(false, "Not enought movement points");
        //}
        _coroutine = StartCoroutine(MovePathCoroutine(path.Select(pn => pn.TargetNode).ToList(),
                _animator, onEndCoroutineAction));
        return new(true, "The path was completed successfully");
    }

    public (bool, string) Rotate(Vector3 targetPosition, Action onEndCoroutineAction)
    {
        _coroutine = StartCoroutine(ChangeRotationCorutine(targetPosition, onEndCoroutineAction));
        // TODO make rotation to consume movement points
        return (true, "Rotation was successful");
    }
    #endregion

    #region internal operations
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.parent.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    private int PathCost(List<PathfinderNodeBase> path)
        => path.Sum(pn => pn.CameFromCost);
    private bool EnoughtMoveResources(int comparable)
        => _apMpSystem.MovementPoints.CurrentValue
            + _apMpSystem.ActionPoints.CurrentValue >= comparable;
    #endregion

    #region coroutines
    private IEnumerator MovePathCoroutine(List<TileNode> path, Animator animator, Action action)
    {
        float step = Speed * Time.fixedDeltaTime;
        CharacterInfo character = _characterInfo;
        if (character == null) throw new ArgumentNullException("Character");

        animator.SetBool(AnimationConstants.WalkingBool, true);

        for (int i = 1; i < path.Count; i++)
        {
            if (path[i].TrySetCharacter(character))
            {
                path[i - 1].TryRemoveCharacter();

                //float distance = (path[i].X != path[i - 1].X && path[i].Y != path[i - 1].Y) ? 1.4f : 1.0f;

                Vector3 targetPosition = path[i].WorldPositionOfCenter;
                RotateTowards(targetPosition);
                while (!character.transform.position.Equals(targetPosition))
                {
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
