using System;
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
        if (Input.GetMouseButtonDown(1))
        {
            FindPathOnGrid();
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
                IngameGrid grid = hit.collider.GetComponent<IngameGrid>();
                TileNode nodeSelected = hit.collider.GetComponent<IngameGrid>().Grid.GetNode(mousePosition);
                Vector2 nodeCoordinates = nodeSelected.Coordinates;
                Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}; Is {(nodeSelected.IsWalkable? "":"un" )}walkable" +
                    $"{(nodeSelected.CharacterOnTile != null? "\nCharacter on tile: " + nodeSelected.CharacterOnTile.name: "")}");

            }
            if (hit.collider.gameObject.tag == "Untagged")
            {
                Vector2 nodeCoordinates = hit.collider.GetComponent<ItemGrid>().GetTileGridPosition(mousePosition);
                Debug.Log($"Pointer on UI: {nodeCoordinates.x}, {nodeCoordinates.y}");
            }
        }
    }

    private void FindPathOnGrid()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //Debug.Log($"Mouse position: {mousePosition.x}, {mousePosition.y}");

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                IngameGrid grid = hit.collider.GetComponent<IngameGrid>();
                Vector2 nodeCoordinates = hit.collider.GetComponent<IngameGrid>().Grid.GetNode(mousePosition).Coordinates;
                Vector2 startCoordinates = new Vector2(0, 0);
                grid.FindPath(startCoordinates, nodeCoordinates);
            }
        }
    }
}
