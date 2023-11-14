using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    public ParticleSystem HitParticle = null;
    protected new void Start()
    {
        HitParticle.gameObject.SetActive(false);
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
        HitParticle.gameObject.SetActive(false);

        Ray weaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new();

        if (Physics.Raycast(weaponRay, out hit, WeaponRange, ~IgnoreHitMask))
        {
            HitParticle.transform.position = hit.point;
            HitParticle.gameObject.SetActive(true);

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
