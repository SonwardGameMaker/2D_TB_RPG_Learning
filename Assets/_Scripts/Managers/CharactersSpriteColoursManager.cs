using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CharacterColorDebug { White, Red, Green, Blue, Yellow, Cyan, Magenta }

public class CharactersSpriteColoursManager : MonoBehaviour
{
    [SerializeField] private CharactersContainerOnScene _neutralsContainer;
    [SerializeField] private CharacterColorDebug _color;

    private Color _chosenColor;
    private List<CharacterInfo> _neutrals;

    // Start is called before the first frame update
    public void Setup()
    {
        _neutrals = _neutralsContainer.GetCharacters().Select(cr => cr.Item2).ToList();

        SetManagerColorSwitch();
        SetCharacterColor();
    }

    private void Update()
    {
        // it's very bad method but the most simple in creation
        SetManagerColorSwitch();
        SetCharacterColor();
    }

    private void SetManagerColorSwitch()
    {
        switch (_color)
        {
            case CharacterColorDebug.White:
                _chosenColor = Color.white;
                break;
            case CharacterColorDebug.Red:
                _chosenColor = Color.red;
                break;
            case CharacterColorDebug.Green:
                _chosenColor = Color.green;
                break;
            case CharacterColorDebug.Blue:
                _chosenColor = Color.blue;
                break;
            case CharacterColorDebug.Yellow:
                _chosenColor = Color.yellow;
                break;
            case CharacterColorDebug.Cyan:
                _chosenColor = Color.cyan;
                break;
            case CharacterColorDebug.Magenta:
                _chosenColor = Color.magenta;
                break;
            default:
                _chosenColor= Color.white;
                break;
        }
    }
    private void SetCharacterColor()
    {
        foreach (CharacterInfo c in _neutrals)
            c.GetComponentInChildren<SpriteRenderer>().color = _chosenColor;
    }
}
