using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControllerLogic : MonoBehaviour
{
    protected Coroutine _coroutine;

    public void Stop()
        => StopCoroutine(_coroutine);
}
