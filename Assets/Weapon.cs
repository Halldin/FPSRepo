using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public WeaponState weaponType;
    public float damagePerPellet;
    public int pelletsPerShot;
    public int ammunition;
    public float fireRate;
    public float reloadTime;
    public float accuracy;
    public FireMode fireMode;
    public float burstAmmount;
    public float burstIntervall;


    public virtual void Shoot(){}
}

public enum FireMode{
    Single, 
    Auto,
    Burst
}
