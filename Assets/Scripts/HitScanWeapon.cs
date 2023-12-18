using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    public ParticleSystem HitParticle = null;
    public ParticleSystem MuzzleFlash = null;

    protected new void Start()
    {
  
        base.Start();
    }
    public override bool Fire()
    {

        if (base.Fire() == false)
        {
            return false;
        }
        Debug.Log("");
        HitScanFire();

        return true;
    }

    private void HitScanFire()
    {
        MuzzleFlash.Play();
   
        Ray weaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new();

        if (Physics.Raycast(weaponRay, out hit, WeaponRange, ~IgnoreHitMask))
        {
            HitParticle.Play();
            HitParticle.transform.SetParent(null);

            HitParticle.transform.position = hit.point;
            HitParticle.transform.forward = hit.normal;
            HitParticle.transform.Translate(hit.normal.normalized * 0.1f);
       

            HandleEntityHit(hit);
        }
    }

    private static void HandleEntityHit(RaycastHit hit)
    {
        var PlayerHit = hit.transform.gameObject.GetComponent<PlayerMovement>();
        if (PlayerHit != null)
        {
            //DOHitStuff()
        }
    }

}
