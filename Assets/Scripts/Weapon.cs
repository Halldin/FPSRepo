using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponState WeaponType = WeaponState.Total;
    public int Ammunition = 1337;
   
   
    public virtual bool Fire()
    {
        if (Ammunition < 1)
        {
            return false;
        }
        Ammunition--;
        return true;
    }
}
//public LayerMask WeaponRayMask = 0;
//public float WeaponRange = 100.0f;
