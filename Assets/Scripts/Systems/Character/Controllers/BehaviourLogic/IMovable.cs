using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void Setup(CharacterInfo characterInfo, Animator animator);
    /// <summary>
    /// Method that make character to rotate to the set point
    /// </summary>
    /// <param name="targetPosition">
    /// The point to which the character will rotate
    /// </param>
    /// <param name="onEndCoroutineAction"></param>
    /// <returns>
    /// Bool returns tells if operation was successful. String returns is a log with more detailed information.
    /// </returns>
    public (bool, string) Rotate(Vector3 targetPosition, Action onEndCoroutineAction);
    /// <summary>
    /// Method that make character to move within game grid
    /// </summary>
    /// <param name="path">
    /// The path for character to move
    /// </param>
    /// <param name="onEndCoroutineAction">
    /// Method that will call in the end of excecution
    /// </param>
    /// <returns>
    /// Bool returns tells if operation was successful. String returns is a log with more detailed information.
    /// </returns>
    public (bool, string) Move(List<PathfinderNodeBase> path, Action onEndCoroutineAction);
}
