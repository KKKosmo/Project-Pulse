using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : CharacterMovement
{
    [SerializeField] public float moveSpeed;
    
    [SerializeField] protected Transform player;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float distToPlayer;
    protected RaycastHit2D[] steeringRays = new RaycastHit2D[7];
    [SerializeField] float steeringRayDistance;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player").transform;
    }
    public virtual void FixedUpdate()
    {
        SteeringRays();
    }
    void SteeringRays()
    {
        Vector3 direction;
        int k = 0;
        for (int i = 1; i > -2; i--)
        {
            for (int j = -1; j < 2; j++)
            {
                direction = (new Vector3(i, j, 0)).normalized;
                steeringRays[k] = Physics2D.Raycast(transform.position, (direction).normalized, steeringRayDistance, groundLayer);
                if (steeringRays[k])
                {
                    Debug.DrawLine(transform.position, steeringRays[k].point, Color.green);
                }
                else
                {
                    Debug.DrawLine(transform.position, transform.position + (direction).normalized * steeringRayDistance, Color.red);
                }
                k++;
            }
            k++;
            i--;
        }
        direction = new Vector3(0, 1, 0);
        steeringRays[3] = Physics2D.Raycast(transform.position, direction.normalized, steeringRayDistance, groundLayer);
        if (steeringRays[3])
        {
            Debug.DrawLine(transform.position, steeringRays[3].point, Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + direction.normalized * steeringRayDistance, Color.red);
        }
    }
    public override void Flip()
    {
        base.Flip();
        moveSpeed *= -1;
    }
}//class
