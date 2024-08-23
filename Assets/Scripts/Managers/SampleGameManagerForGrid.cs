using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGameManagerForGrid : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickOnGrid();
        }
    }

    private void ClickOnGrid()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //Debug.Log($"Mouse position: {mousePosition.x}, {mousePosition.y}");

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                Vector2 nodeCoordinates = hit.collider.GetComponent<IngameGrid>().Grid.GetNode(mousePosition).Coordinates;
                Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}");
            }
            if (hit.collider.gameObject.tag == "Untagged")
            {
                Vector2 nodeCoordinates = hit.collider.GetComponent<ItemGrid>().GetTileGridPosition(mousePosition);
                Debug.Log($"Pointer on UI: {nodeCoordinates.x}, {nodeCoordinates.y}");
            }
        }
    }
}
