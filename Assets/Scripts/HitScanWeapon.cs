using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    public ParticleSystem HitParticle = null;
    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return false;
        }

        HitScanFire();
        return true;
    }

    private void HitScanFire()
    {
        Ray weaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(weaponRay, out hit, WeaponRange, HitMask))
        {
            HitParticle.transform.position = hit.point;
            HitParticle.Play();
            //DOHitStuff()
        }
    }
}

//HitScanFire();
//public void HitScanFire()
//{


//}