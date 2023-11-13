using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return false;
        }
        //FIRE ROCKET()
        return true;
    }
}
