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
    private List<ControllerManagerBase> _characters;

    // Temporary fields
    private InputHandlerManager _player;

    private void Start()
    {
        _gridManager.Setup();
        _uiManager.Setup();

        _characters = _charactersContainer.GetCharacters().Select(c=>c.Item2.GetComponent<ControllerManagerBase>()).ToList();
        
        // Temp
        _player = _characters.Find(c=>c is InputHandlerManager) as InputHandlerManager;

        _uiInitManagerBase = GetComponent<UiInitManagerBase>();
        _uiInitManagerBase.Setup();
    }

    public void TemporaryEndTurnMethod()
    {
        _player.NewTurn();
    }
}
