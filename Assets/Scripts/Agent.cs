using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public virtual void OnCollisionEnter(Collision collision)
    {
        // Detta är base.OnCollisionEnter();
        Debug.Log("I - " + gameObject.name + " was hit!");
    }
    public virtual void OnCollisionExit(Collision collision)
    {

    }

    public virtual void OnCollisionStay(Collision collision)
    {

    }
    public virtual void OnTriggerEnter(Collider other)
    {

    }
    public virtual void OnTriggerExit(Collider other)
    {

    }
    public virtual void OnTriggerStay(Collider other)
    {

    }

    //public override void OnCollisionEnter(Collision collision)
    //{
    //    base.OnCollisionEnter(collision);
    //}
    //public override void OnCollisionExit(Collision collision)
    //{
    //    base.OnCollisionExit(collision);
    //}
    //public override void OnCollisionStay(Collision collision)
    //{
    //    base.OnCollisionStay(collision);
    //}
    //public override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}
    //public override void OnTriggerExit(Collider other)
    //{
    //    base.OnTriggerExit(other);
    //}
    //public override void OnTriggerStay(Collider other)
    //{
    //    base.OnTriggerStay(other);
    //}
}
