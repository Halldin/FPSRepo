using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EnemyBase : Agent
{
    public PlayerMovement ActivePlayer = null;
    public float DetectionRange = 10.0f;
    bool MoveTowardTarget = false;

    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        MoveTowardTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveTowardTarget)
        {
            HandleFollowTarget();
        }
        TryHandleEngageTarget();
    }
    public void HandleDetectTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Ray(transform.position, (ActivePlayer.transform.position - transform.position).normalized), out hit, DetectionRange))
        {
            var PlayerMovement = hit.transform.gameObject.GetComponent<PlayerMovement>();
            if (PlayerMovement != null )
            {
                OnTargetDetected();
            }
        }
    }
    public void OnTargetDetected()
    {
        MoveTowardTarget = true;
    }

    public void HandleFollowTarget()
    {

    }

    public void TryHandleEngageTarget()
    {

    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }
    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }
}
