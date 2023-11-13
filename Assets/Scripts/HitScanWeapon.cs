using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return false;
        }
        return true;
    }

}
//public ParticleSystem HitParticle = null;
//HitScanFire();
//public void HitScanFire()
//{
//    Ray weaponRay = new Ray(PlayerMovement.myCamera.transform.position, PlayerMovement.myCamera.transform.forward);
//    RaycastHit hit = new RaycastHit();
//    if (Physics.Raycast(weaponRay, out hit, WeaponRange, WeaponRayMask))
//    {
//        HitParticle.transform.position = hit.point;
//        HitParticle.Play();
//        //DOHitStuff()
//    }
//}