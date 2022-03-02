using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowInteractable : GrabInteractable
{
    [SerializeField] private int velocityResolution = 10;
    [SerializeField] private float throwBoost = 100;


    private Queue<Vector3> velocities = new Queue<Vector3>();
    
    private Vector3 previousPosition;

    private Rigidbody body;

    public override void Start()
    {
        base.Start();

        previousPosition = transform.position;
        body = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate() // 0.02 seconds
    {
        var velocity = transform.position - previousPosition; // neglecting the time coz its a constant
        previousPosition = transform.position;

        velocities.Enqueue(velocity);

        if(velocities.Count > velocityResolution)
        {
            velocities.Dequeue();
        }


    }


    public override void OnRelease()
    {
        base.OnRelease();

        Vector3 velocity = Vector3.zero;

        foreach(var v in velocities)
        {
            velocity += v;
        }

        velocity /= velocities.Count; // get the average

        velocity *= throwBoost; // add the boost

        body.velocity = velocity; // apply the velocity

    }


}
