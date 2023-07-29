using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIMovement2 : CharacterMovement
{
    public GUIStyle gui;
    [SerializeField] Vector3 direction;

    [SerializeField] Transform goal;
    [SerializeField] float speed = 50f;
    [SerializeField] float accuracy = 0.01f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;
    //public WPManager WPManager;

    [SerializeField] GameObject target;

    void OnDrawGizmos()
    {
        //Handles.Label(transform.position, currentWP.ToString(), gui);
        if (goal != null)
            Handles.Label(transform.position, goal.gameObject.name, gui);
    }
    private void Start()
    {
        //wps = new GameObject[WPManager.waypoints.Length];
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[20];
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        currentNode = g.getPathPoint(currentWP);

        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }
        //Debug.Log("DISTANCE TO NEXT WAYPOINT IS " + Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position));\
        //if (goal != null)
        //Debug.Log("CURRENT WP IS " + goal.gameObject.name);
        if(currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y);
            direction = lookAtGoal - this.transform.position;
        }
    }
    private void FixedUpdate()
    {
        //rb.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = direction * speed * Time.deltaTime;
    }
    public void GoTo(GameObject target)
    {
        g.AStar(currentNode, target);
        currentWP = 0;
    }
    private void Update()
    {
        if (Input.GetButton("Pulse"))
        {
            GoTo(target);
            Debug.Log("PULSE PRESSED");
        }
    }
}
