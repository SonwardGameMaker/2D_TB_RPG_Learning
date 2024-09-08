
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] protected EnvironmentSO _environmentSO;

    protected bool _isWalkable;
    protected bool _throughtShootable;

    private void Start()
    {
        if (_environmentSO != null)
        {
            _isWalkable = _environmentSO.IsWalkable;
            _throughtShootable = _environmentSO.throughtShootable;
        }
        else
        {
            _isWalkable = false;
            _throughtShootable = false;
            Debug.LogError("EnvironmentSO is null. Assigned default values");
        }
    }

    public bool IsWalkable { get => _isWalkable;}
    public bool ThroughtShootable { get => _throughtShootable; }
}
