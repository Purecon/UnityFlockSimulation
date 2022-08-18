using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite 3D")]
public class CompositeBehavior3D : FlockBehavior3D
{
    //Array of flock
    public FlockBehavior3D[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent3D agent, List<Transform> context, Flock3D flock)
    {
        //handle data mismatch
        if (behaviors.Length != weights.Length)
        {
            Debug.LogError("Data mismatch in" + name, this);
            return Vector3.zero;
        }
        
        //set up move
        Vector3 move = Vector3.zero;

        //iterate through the behaviors
        for(int i = 0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if(partialMove != Vector3.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move+=partialMove;
            }
        }
        return move;
    }
}
