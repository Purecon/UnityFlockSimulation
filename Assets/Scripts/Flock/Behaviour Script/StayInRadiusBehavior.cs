using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public float radius = 15f;

    //Menuju center
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Transform di pusat
        Transform centerTransform = flock.rallypoint;
        Vector2 center = new Vector2(centerTransform.transform.position.x, centerTransform.transform.position.y);
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        //t antara 0 dari 1 untuk jarak ke pusat
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset*t*t;
    }
}
