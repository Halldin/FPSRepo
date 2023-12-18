using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Agent Holder = null;
    public WeaponState WeaponType = WeaponState.Total;
    public int Ammunition = 1337;

    public float WeaponRange = 13337.0f;
    public LayerMask IgnoreHitMask = 0;

    protected void Start()
    {
     
    }
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


