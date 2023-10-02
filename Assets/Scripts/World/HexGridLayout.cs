using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    public Vector2Int gridSize;

    public Material material;
    [SerializeField] private float innerSize = 0.8f;
    [SerializeField] private float outerSize = 1;
    [SerializeField] private float height = 0f;
    [SerializeField] private bool isFlatTop = true;

    private Dictionary<int, List<HexRenderer>> hexRows = new();

    private void Awake()
    {
        LayoutGrid();

        //place grid in center of screen
        HexRenderer hexInCenterOfGrid = GetHex(gridSize.x / 2, gridSize.y / 2);
        transform.position = new Vector2(-hexInCenterOfGrid.transform.position.x, -hexInCenterOfGrid.transform.position.y);
    }
    private void LayoutGrid()
    {
        for(int y = 0; y < gridSize.y; y++)
        {
            List<HexRenderer> row = new List<HexRenderer>();
            for (int x = 0; x < gridSize.x; x++)
            {

                GameObject tile = new GameObject($"Hex {x},{y}", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.isFlatTop = isFlatTop;
                hexRenderer.innerSize = innerSize;
                hexRenderer.outerSize = outerSize;
                hexRenderer.height = height;
                hexRenderer.SetMaterial(material);
                hexRenderer.DrawMesh();

                tile.transform.SetParent(transform, true);
                row.Add(hexRenderer);
            }
            hexRows.Add(y, row);
        }
    }

    private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTop)
        {
            shouldOffset = row % 2 == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f/ 4f);

            offset = shouldOffset ? horizontalDistance / 2f : 0f;

            xPosition = (column * horizontalDistance) + offset;
            yPosition = row * verticalDistance;
        }
        else
        {
            shouldOffset = column % 2 == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3) * size;

            horizontalDistance = width * (3f / 4f);
            verticalDistance = height;

            offset = shouldOffset ? verticalDistance / 2f : 0f;
            xPosition = (column * horizontalDistance);
            yPosition = (row * verticalDistance) - offset;
        }

        return new Vector3(xPosition, -yPosition, 0);
    }

    public Dictionary<int, List<HexRenderer>> GetHexRows()
    {
        return hexRows;
    }

    public Vector3 GetCenterOfGrid()
    {
        HexRenderer hexInCenterOfGrid = GetHex(gridSize.x / 2, gridSize.y / 2);
        return hexInCenterOfGrid.GetFace(1).GetMiddlePointLeft();
    }

    public HexRenderer GetHex(int x, int y)
    {
        if (hexRows.TryGetValue(y, out List<HexRenderer> row))
        {
            return row[x];
        }
        else
        {
            return null;
        }
    }   

}
