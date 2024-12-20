using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharactersContainerOnScene _charactersContainer;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private UiManager _uiManager;

    private UiInitManagerBase _uiInitManagerBase;
    private GridSystemTestingManager _gridSystemTestingManager;
    private List<ControllerManagerBase> _characters;

    // Temporary fields
    private PlayerInputManager _player;

    private void Start()
    {
        _gridManager.Setup();
        _uiManager.Setup();

        _characters = _charactersContainer.GetCharacters().Select(c=>c.Item2.GetComponent<ControllerManagerBase>()).ToList();
        
        // Temp
        _player = _characters.Find(c=>c is PlayerInputManager) as PlayerInputManager;

        _uiInitManagerBase = GetComponent<UiInitManagerBase>();
        _gridSystemTestingManager = GetComponent<GridSystemTestingManager>();

        _uiInitManagerBase.Setup();
        _gridSystemTestingManager.Setup();
    }

    public PlayerInputManager Player { get => _player; }

    public void TemporaryEndTurnMethod()
    {
        _player.NewTurn();
    }
}
