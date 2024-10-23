using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGrid : MonoBehaviour, IPointerUpHandler
{
    private const float TILE_SIZE_WIDTH = 32;
    private const float TILE_SIZE_HEIGHT = 32;

    //[SerializeField] DropzoneFunctionality _dropzone;
    private RectTransform _rectTransform;
    private Vector2 _positionOnTheGrid;
    private List<ItemUI> _items;

    public void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public Vector2Int GetTileGridPosition(Vector3 mousePosition)
    {
        float gridPositionX = mousePosition.x - _rectTransform.position.x;
        float gridPositionY = _rectTransform.position.y - mousePosition.y;

        int tilePositionX = (int)(gridPositionX / TILE_SIZE_WIDTH);
        int tilePositionY = (int)(gridPositionY / TILE_SIZE_HEIGHT);

        return new Vector2Int(tilePositionX, tilePositionY);
    }

    //void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    //{
    //    //_dropzone.gameObject.SetActive(true);
    //    Vector2Int pointerOnGrid = GetTileGridPosition(eventData.position);
    //    //Debug.Log($"Mouse possition is {GetMousePosition().x}, {GetMousePosition().y}");
    //    //Debug.Log($"Event data possition is {eventData.position.x}, {eventData.position.y}");
    //    Debug.Log($"Pointer down in {pointerOnGrid.x}, {pointerOnGrid.y}");
    //}

    public void OnPointerUp(PointerEventData eventData)
    {
        //_dropzone.gameObject.SetActive(false);
    }

    private Vector3 GetMousePosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

}
