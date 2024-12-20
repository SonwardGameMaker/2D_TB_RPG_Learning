using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemTestingSceneUiInitManager : UiInitManagerBase
{
    [SerializeField] private MainCameraManager _mainCameraManager;
    [SerializeField] private ApMpController _mpController;
    [SerializeField] private ApMpController _apController;
    [SerializeField] private HealthController _healthController;
    [SerializeField] private WeaponStatsBox _weaponStatsBox;

    private PlayerInputManager _player;
    private IInputHandler _playerInput;

    public override void Setup()
    {
        base.Setup();
        _mainCameraManager.Setup();
        _mpController.Setup();
        _apController.Setup();
        _healthController.Setup();
        _weaponStatsBox.Setup();

        _player = GetComponent<GameManager>().Player;
        _playerInput = _player.GetComponentInChildren<IInputHandler>();

        _playerInput.MouseWheel_UpScroll += _mainCameraManager.ZoomInCamera;
        _playerInput.MouseWheel_DownScroll += _mainCameraManager.ZoomOutCamera;
        _playerInput.ChangeInventoryTabVisibility_Pressed += _weaponStatsBox.ChangeActive;
    }

    private void OnDestroy()
    {
        if (_player != null && _player.gameObject != null)
        {
            _playerInput.MouseWheel_UpScroll -= _mainCameraManager.ZoomInCamera;
            _playerInput.MouseWheel_DownScroll -= _mainCameraManager.ZoomOutCamera;
            _player.GetComponentInChildren<IInputHandler>().ChangeInventoryTabVisibility_Pressed -= _weaponStatsBox.ChangeActive;
        }       
    }
}
