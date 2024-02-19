using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public float damagePerPellet;
    public int pelletsPerShot;
    public int ammunition;
    public float fireRate;
    public float reloadTime;
    public float accuracy;

    public void OnEnable(){
        Reload();
    }

    public void Reload(){
        
    }

    public virtual void Shoot(){}
}
