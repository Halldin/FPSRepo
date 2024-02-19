using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    public ParticleSystem MuzzleFlash = null;
    public ProjectileBase ProjectileToSpawn = null;
    //public bool Fire()
    //{
        //if (base.Fire() == false)
        //{
        //    return false;
        //}
        //MuzzleFlash.Play();
//
        //ProjectileBase spawnedProjectile = Instantiate(ProjectileToSpawn);
        //
        //spawnedProjectile.Creator = this;
//
        //ProjectileToSpawn.transform.position = transform.position;
        //spawnedProjectile.Init(transform.position,
        //    Camera.main.transform.forward.normalized * 1000.0f 
        //    + Camera.main.transform.position);
//
        //return true;
    //}
}


//public Projectile SpawnProjectile = null;

//Projectile firedProjectile = GameObject.Instantiate(SpawnProjectile);

//firedProjectile.Init(transform.position, 
//    Camera.main.transform.forward.normalized*1000.0f + Camera.main.transform.position);
