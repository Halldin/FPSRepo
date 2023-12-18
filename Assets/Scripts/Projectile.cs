using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Agent Holder = null;

    public GameObject ProjectileObject = null;
    public GameObject DetonationObject = null;

    public float DetionationLifetime = 1.0f;
    public float DetonationTime = -1.0f;
    
    protected Vector3 SpawnPosition = new Vector3();
    protected Vector3 AimPosition = new Vector3();
    protected Vector3 AimDirection = new Vector3();

    public float MovementSpeed = 10.0f;
    bool IsDelayedInit = false;
    public virtual void Start()
    {
        ProjectileObject.SetActive(true);
        DetonationObject.SetActive(false);
        if(IsDelayedInit)
        {
            DelayedInit();
        }
    }
    public virtual void DelayedInit()
    {
        gameObject.transform.position = SpawnPosition;
        gameObject.transform.LookAt(AimDirection, transform.up);
        IsDelayedInit = false;
    }
    public virtual void Init(Vector3 aSpawnPosition, Vector3 aAimPosition)
    {
        SpawnPosition = aSpawnPosition;
        AimPosition = aAimPosition;
        AimDirection = AimPosition - SpawnPosition;
        IsDelayedInit = true;
    }

    public virtual void Update()
    {
        if (IsDelayedInit)
        {
            DelayedInit();
        }
        if(DetonationTime > 0)
        {
            DetonationTime -= Time.deltaTime;
            if(DetonationTime < 0 )
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var hitAgent = collision.gameObject.GetComponent<Agent>();
        if (hitAgent != null)
        {
            if (Holder != hitAgent)
            {

            }
        }

        Debug.Log("Collision");
        if(collision.gameObject.tag != "Player")
        {
            ProjectileObject.SetActive(false);
            DetonationObject.SetActive(true);
            DetonationTime = DetionationLifetime;
        }
    }

}
