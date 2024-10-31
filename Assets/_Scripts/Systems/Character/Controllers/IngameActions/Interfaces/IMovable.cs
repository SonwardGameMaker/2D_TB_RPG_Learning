using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void Setup(CharacterInfo characterInfo, Animator animator);
    public float MpApCostModifier { get; }
    public bool CheckIfEnoughtResources(List<PathfinderNodeBase> path);
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
    public void Rotate(Vector3 targetPosition, Action<bool, string> onEndCoroutineAction);
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
    public void Move(List<PathfinderNodeBase> path, Action<bool, string> onEndCoroutineAction);
}
