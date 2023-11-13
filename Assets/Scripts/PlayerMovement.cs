using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Camera myCamera = null;

    public List<Weapon> AllHeldWeapons = new List<Weapon>();
    public Weapon CurrentWeapon = null;

    private WeaponState CurrentWeaponState = WeaponState.Unarmed;

   

    public void Update()
    {
      

    }

    public void Awake()
    {
        PlayerMovement.myCamera = Camera.main;
    }


  
}