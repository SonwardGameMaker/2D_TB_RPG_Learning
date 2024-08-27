using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentContainer : MonoBehaviour
{
    public List<(Vector3, Environment)> GetEnvironments()
    {
        List<(Vector3, Environment)> result = new List<(Vector3, Environment)>();

        foreach (Transform child in transform)
        {
            (Vector3, Environment) environment = new(child.position, child.GetComponent<Environment>());
            if (environment.Item2 != null)
                result.Add(environment);
        }

        return result;
    }
    //public List<(Vector3, Environment)> GetEnvironments()
    //{
    //    List<(Vector3, Environment)> result = new List<(Vector3, Environment)>();

    //    foreach (Transform child in transform)
    //    {
    //        EnvironmentContainer container = child.GetComponent<EnvironmentContainer>();
    //        if (container != null)
    //        {
    //            List<(Vector3, Environment)> tempResult = container.GetEnvironments();
    //            for (int i = 0; i < tempResult.Count; i++)
    //            {
    //                var tuple = tempResult[i];
    //                tuple.Item1 += child.transform.position;
    //                tempResult[i] = tuple;
    //            }

    //            result.AddRange(tempResult);
    //        } 


    //        (Vector3, Environment) environment = new (child.position, child.GetComponent<Environment>());
    //        if (environment.Item2 != null)
    //            result.Add(environment);
    //    }

    //    return result;
    //}

}
