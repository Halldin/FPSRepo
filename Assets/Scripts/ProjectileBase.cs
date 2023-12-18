using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Agent Holder = null;
    [Header("Static Editor References")]
    public GameObject ProjectileObject = null;
    public GameObject DetonationObject = null;

    [Header("Static Editor Values")]
    public float MovementSpeed = 20.0f;
    protected float LifetimeMax = 20.0f;
    public float DetonationMaxLifetime = 1.0f;

    [Header("Dynamic Values")]
    protected float DetonationLifetime = -1337.0f;
    protected float Lifetime = 0.0f;
    protected Vector3 SpawnPosition = Vector3.zero;
    protected Vector3 AimPoint = Vector3.zero;
    protected Vector3 AimDirection = Vector3.zero;

    bool IsDelayedInit = false;
    public virtual void Update()
    {
        //if (IsDelayedInit)
        //{
        //    DelayedInit();
        //}
     
        gameObject.transform.LookAt(AimDirection + transform.position);
        HandleLifetime();
        TryHandleDetonationLifetime();
    }

    private void TryHandleDetonationLifetime()
    {
        if (DetonationObject != null && DetonationLifetime > 0.0f)
        {
            DetonationLifetime -= Time.deltaTime;
            if (DetonationLifetime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandleLifetime()
    {
        Lifetime += Time.deltaTime;
        if (Lifetime >= LifetimeMax)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        ProjectileObject.SetActive(true);

        if (DetonationObject != null)
        {
            DetonationObject.SetActive(false);
        }
        ProjectileObject.SetActive(true);
        DetonationObject.SetActive(false);
        //if (IsDelayedInit)
        //{
        //    DelayedInit();
        //}
    }
    public void OnCollisionEnter(Collision collision)
    {
      

        if(collision.gameObject.layer == gameObject.layer )
        {
            return;
        }
        var hitAgent = collision.gameObject.GetComponent<Agent>();
        if (hitAgent != null)
        {
            if (Holder != hitAgent)
            {

            }
        }
        ProjectileObject.SetActive(false);
        if(DetonationObject != null) 
        {
            DetonationLifetime = DetonationMaxLifetime;
            DetonationObject.SetActive(true);
        }
    }

  
    public virtual void Init(Vector3 aSpawnPosition, Vector3 aAimPoint)
    {
        SpawnPosition = aSpawnPosition;

        AimPoint = aAimPoint;
        AimDirection = (AimPoint - SpawnPosition).normalized;
        IsDelayedInit = true;
    }
    //public virtual void DelayedInit()
    //{
    //    gameObject.transform.position = SpawnPosition;
    //    gameObject.transform.LookAt(AimDirection+transform.position);
    //    IsDelayedInit = false;
    //}
}
