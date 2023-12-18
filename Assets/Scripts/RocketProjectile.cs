using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : ProjectileBase
{
    public override void Update()
    {
        base.Update();
        if (DetonationLifetime < 0)
        {
            transform.position += MovementSpeed * Time.deltaTime * AimDirection;
        }
   
    }
}
