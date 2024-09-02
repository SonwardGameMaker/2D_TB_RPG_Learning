using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class General
{
    public static int CalculateDistance(int x1, int y1, int x2, int y2)
    {
        int xDistacne = Mathf.Abs(x1 - x2);
        int yDistance = Mathf.Abs(y1 - y2);

        return (int)Mathf.Sqrt(Mathf.Pow(xDistacne, 2) + Mathf.Pow(yDistance, 2));
    }
}
