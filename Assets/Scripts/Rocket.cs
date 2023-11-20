using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    public override void Init(Vector3 aSpawnPosition, Vector3 aAimPosition)
    {
        base.Init(aSpawnPosition, aAimPosition);
    }

    public override void Update()
    {
        base.Update();
        if(DetonationTime > 0)
        {
            return;
        }
        transform.position += AimDirection.normalized * MovementSpeed * Time.deltaTime;
    
    }
}
