using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public Projectile SpawnProjectile = null;
    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return false;
        }
        
        Projectile firedProjectile = GameObject.Instantiate(SpawnProjectile);

        firedProjectile.Init(transform.position, 
            Camera.main.transform.forward.normalized*1000.0f + Camera.main.transform.position);

        return true;
    }
}
