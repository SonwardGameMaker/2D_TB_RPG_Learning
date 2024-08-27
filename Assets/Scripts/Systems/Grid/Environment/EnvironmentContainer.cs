using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentContainer : MonoBehaviour
{
    public List<Environment> GetEnvironments()
    {
        List<Environment> result = new List<Environment>();

        foreach (Transform child in transform)
        {
            EnvironmentContainer container = child.GetComponent<EnvironmentContainer>();
            if (container != null)
                result.AddRange(container.GetEnvironments());


            Environment environment = child.GetComponent<Environment>();
            if (environment != null)
                result.Add(environment);
        }

        return result;
    }
    
}
