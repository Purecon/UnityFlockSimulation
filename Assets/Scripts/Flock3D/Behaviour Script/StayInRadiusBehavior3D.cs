using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius 3D")]
public class StayInRadiusBehavior3D : FlockBehavior3D
{
    public float radius = 15f;

    //Menuju center
    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        //Transform di pusat
        Transform centerTransform = flock.rallypoint;
        Vector3 center = new Vector3(centerTransform.transform.position.x, centerTransform.transform.position.y, centerTransform.transform.position.z);
        Vector3 centerOffset = center - (Vector3)agent.transform.position;
        //t antara 0 dari 1 untuk jarak ke pusat
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffset*t*t;
    }
}
