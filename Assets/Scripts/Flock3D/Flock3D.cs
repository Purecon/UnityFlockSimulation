using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock3D : MonoBehaviour
{
    public FlockAgent3D agentPrefab;
    List<FlockAgent3D> agents = new List<FlockAgent3D>();
    public FlockBehavior3D behavior;

    //TODO density adjustment
    //Flock density
    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    //Drive factor
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    //Flock speed
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    //Avoidance range
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    //Rotation
    public bool noRotationDefault = false;

    //Rally point
    public Transform rallypoint;

    //Spawn point
    public Transform spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    
        if(spawnpoint == null)
        {
            spawnpoint = transform;
        }

        for (int i = 0; i < startingCount; i++)
        {
            float rotationValue;
            if (noRotationDefault)
            {
                rotationValue = 0f;
            }
            else
            {
                rotationValue = Random.Range(0f, 360f);
            }
            /*
            FlockAgent3D newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward* rotationValue),
                transform
                );
            */
            FlockAgent3D newAgent = Instantiate(
                agentPrefab,
                spawnpoint.position + new Vector3(Random.Range(0,5)*AgentDensity, Random.Range(0, 5) * AgentDensity, Random.Range(0, 5) * AgentDensity),
                Quaternion.Euler(Vector3.forward * rotationValue),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }

        if (rallypoint == null)
        {
            rallypoint = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent3D agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //Debug with color
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count/60f);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);

            //Default rotation
            /*
            if (noRotationDefault)
            {
                GameObject spriteChild = agent.gameObject.transform.GetChild(0).gameObject;
                spriteChild.transform.rotation = Quaternion.identity;
                SpriteRenderer rendererChild = spriteChild.GetComponent<SpriteRenderer>();
                //flip if positive
                if (move.x > 0)
                {
                    rendererChild.flipX = true;
                }
                else
                {
                    rendererChild.flipX = false;
                }
            }
            */
        }
    }

    //Get nearby objects
    List<Transform> GetNearbyObjects(FlockAgent3D agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position,neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
