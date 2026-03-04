using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Texture2D _baseCursor;
    [SerializeField] private Texture2D _attackCursor;

    private Vector2 _cursorHotspot;

    public void Setup()
    {
        SetBaseCursor();
    }

    public void SetBaseCursor()
    {
        _cursorHotspot = Vector2.zero;
        SetCursor(_baseCursor);
    }

    public void SetAttackCursor()
    {
        _cursorHotspot = new Vector2(_attackCursor.width / 2, _attackCursor.height / 2);
        SetCursor(_attackCursor);
    }

    private void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, _cursorHotspot, CursorMode.Auto);
    }
}
