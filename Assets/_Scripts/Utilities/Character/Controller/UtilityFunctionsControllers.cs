using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UtilityFunctionsControllers
{
    public static int CalculatePathCost(List<PathfinderNodeBase> path, float mpApModifier)
        => Convert.ToInt32(path.Sum(pn => pn.CameFromCost) * mpApModifier);
}
