using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment 3D")]
public class AlignmentBehavior3D : FilteredFlockBehavior3D
{
    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //if no neighbors, maintain current alignment
        if (filteredContext.Count == 0)
        {
            return agent.transform.up;
        }

        //add all points together and average
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector3)item.transform.up;
        }
        //alignmentMove = alignmentMove.normalized;
        alignmentMove /= filteredContext.Count;

        return alignmentMove;
    }
}
