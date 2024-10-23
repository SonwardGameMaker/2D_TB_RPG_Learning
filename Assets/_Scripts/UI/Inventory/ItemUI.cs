using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IDragHandler
{
    [SerializeField] SpriteRenderer _itemSprite;
    [SerializeField] Item _item;

    private RectTransform _rectTransform;
    private Canvas _canvas;

    public void Init(Item item, Canvas canvas)
    {
        _item = item;
        _canvas = canvas;
        _rectTransform = GetComponent<RectTransform>();
        _itemSprite = GetComponent<SpriteRenderer>();
        _itemSprite.sprite = _item.ImageUI;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
}
