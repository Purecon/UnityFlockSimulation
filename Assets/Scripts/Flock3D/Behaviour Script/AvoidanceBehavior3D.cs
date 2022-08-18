using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance 3D")]
public class AvoidanceBehavior3D : FilteredFlockBehavior3D
{
    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        List<Transform> filteredContext = (filter==null) ? context: filter.Filter(agent, context);

        //if no neighbors, return no adjust
        if (filteredContext.Count == 0)
        {
            return Vector3.zero;
        }

        //add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        foreach (Transform item in filteredContext)
        {
            //For collider not only center use closest point (Point fix)
            Collider collider = item.gameObject.GetComponent<Collider>();
            Vector3 closestPoint;
            if (collider.GetType() == typeof(MeshCollider))
            {
                MeshCollider meshCollider = (MeshCollider)collider;
                meshCollider.convex = true;
                closestPoint = collider.ClosestPoint(agent.transform.position);
            }
            else
            {
                closestPoint = collider.ClosestPoint(agent.transform.position);
            }
            

            if (Vector3.SqrMagnitude(closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                //avoidanceMove += (Vector2)(agent.transform.position-item.position);
                avoidanceMove += (Vector3)(agent.transform.position - closestPoint);
            }
        }
        if(nAvoid > 0)
        {
            //avoidanceMove = avoidanceMove.normalized;
            avoidanceMove /= nAvoid;
        }
        return avoidanceMove;
    }
}
