
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] protected EnvironmentSO _environmentSO;

    protected bool _isWalkable;

    private void Start()
    {
        if (_environmentSO != null)
        {
            _isWalkable = _environmentSO.IsWalkable;
        }
        else
        {
            _isWalkable = false;
            Debug.LogError("EnvironmentSO is null. Assigned defaults value");
        }
    }

    public bool IsWalkable { get => _isWalkable;}
}
