using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class General
{
    public static int CalculateDistance(int x1, int y1, int x2, int y2)
    {
        int xDistacne = Mathf.Abs(x1 - x2);
        int yDistance = Mathf.Abs(y1 - y2);

        return (int)Mathf.Sqrt(Mathf.Pow(xDistacne, 2) + Mathf.Pow(yDistance, 2));
    }

    public static List<Vector2Int> GetLineCells(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        int x1 = start.x;
        int y1 = start.y;
        int x2 = end.x;
        int y2 = end.y;

        int dx = Mathf.Abs(x2 - x1);
        int dy = Mathf.Abs(y2 - y1);

        int sx = x1 < x2 ? 1 : -1;
        int sy = y1 < y2 ? 1 : -1;

        int err = dx - dy;

        while (true)
        {
            result.Add(new Vector2Int(x1, y1));

            if (x1 == x2 && y1 == y2) break;

            int e2 = 2 * err;

            if (e2 > -dy)
            {
                err -= dy;
                x1 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y1 += sy;
            }
        }

        return result;
    }
}
