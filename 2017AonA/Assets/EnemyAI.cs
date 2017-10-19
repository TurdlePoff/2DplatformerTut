using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D)) ]
[RequireComponent(typeof(Seeker)) ]


public class EnemyAI : MonoBehaviour {

    //What to chase
    public Transform target;

    //How many times each second we will update our path
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //Calculated path
    public Path path;

    //AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode; //Controls way forces are applied to our rigidBody //Enum

    [HideInInspector]
    public bool pathIsEnded = false;

    //The max distance from an AI to a waypoint for it to continue to the next wayPoint
    public float nextWayPointDist = 3f;

    //Waypoint we are currently moving towards
    private int currentWayPoint = 0;
    
    void Start ()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if(target == null)
        {
            Debug.LogError("No Player found PANIC");
            return;
        }

        StartCoroutine(UpdatePath());
	}

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            //TODO: Insert player search here
            yield return false ;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1/updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path. Did it show an error? " + p.error);
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }

        //TODO: Always look at player?
        if(path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count) //If final way point reached
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached"); 
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Check direction to next wayPoint
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        //Move AI
        rb.AddForce(dir, fMode);

        if(dist < nextWayPointDist)
        {
            currentWayPoint++;
            return;
        }
    }
}
