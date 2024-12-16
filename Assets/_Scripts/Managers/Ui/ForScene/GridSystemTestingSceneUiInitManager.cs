using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemTestingSceneUiInitManager : UiInitManagerBase
{
    [SerializeField] ApMpController _mpController;
    [SerializeField] ApMpController _apController;
    [SerializeField] HealthController _healthController;
    [SerializeField] WeaponStatsBox _weaponStatsBox;

    public override void Setup()
    {
        base.Setup();

        _mpController.Setup();
        _apController.Setup();
        _healthController.Setup();
        _weaponStatsBox.Setup();
    }
}
