using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemTestingManager : ConcreteSceneDetailsManagerBase
{
    [SerializeField] private CharactersSpriteColoursManager _charactersSpriteColoursManager;
    [SerializeField] private EnemiesLoggerManager _emiesLoggerManager;

    public override void Setup()
    {
        _charactersSpriteColoursManager.Setup();
        _emiesLoggerManager.Setup();
    }
}
