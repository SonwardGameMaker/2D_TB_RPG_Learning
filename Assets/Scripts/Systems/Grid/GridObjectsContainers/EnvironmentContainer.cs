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
            (Vector3, Environment) environment = new(child.position + transform.position, child.GetComponent<Environment>());
            if (environment.Item2 != null)
                result.Add(environment);
        }

        return result;
    }

}
