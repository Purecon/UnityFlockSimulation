using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter==null) ? context: filter.Filter(agent, context);

        //if no neighbors, return no adjust
        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        foreach (Transform item in filteredContext)
        {
            //For collider not only center use closest point (Point fix)
            Vector3 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);

            if (Vector2.SqrMagnitude(closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                //avoidanceMove += (Vector2)(agent.transform.position-item.position);
                avoidanceMove += (Vector2)(agent.transform.position - closestPoint);
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
