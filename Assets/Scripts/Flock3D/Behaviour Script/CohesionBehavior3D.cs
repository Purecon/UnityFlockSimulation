using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion 3D")]
public class CohesionBehavior3D : FilteredFlockBehavior3D
{
    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //if no neighbors, return no adjust
        if (filteredContext.Count == 0)
        {
            return Vector3.zero;
        }

        //add all points together and average
        Vector3 cohesionMove = Vector3.zero;
        foreach(Transform item in filteredContext)
        {
            cohesionMove += (Vector3)item.position;
        }
        //cohesionMove = cohesionMove.normalized;
        cohesionMove /= filteredContext.Count;

        //Create offset from agent position
        cohesionMove -= (Vector3)agent.transform.position;
        return cohesionMove;
    }
}
