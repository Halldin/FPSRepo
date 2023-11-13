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
        //MouseAxisDelta += Input.mouseScrollDelta.y;
        //if (Mathf.Abs(MouseAxisDelta) > MouseAxisWeaponSwapBreakpoint)
        //{
        //    //int swapDirectionNumber = (int)Mathf.Sign(MouseAxisDelta);
        //    //MouseAxisDelta -= swapDirectionNumber * MouseAxisWeaponSwapBreakpoint;
        //    //int currentWeaponIndex = (int)CurrentWeaponState + swapDirectionNumber;
        //    //if (currentWeaponIndex >= (int)WeaponState.Total)
        //    //{
        //    //    currentWeaponIndex = 0;
        //    //}
        //    //else if (currentWeaponIndex < 0)
        //    //{
        //    //    currentWeaponIndex = (int)WeaponState.Total - 1;
        //    //}
        //    //WeaponSwapAnimation(currentWeaponIndex);

        //}

        if (Input.GetMouseButtonDown(0))
        {
            FireHeldWeapon();
        }
    }

    //public void Awake()
    //{
    //    PlayerMovement.myCamera = Camera.main;
    //}
    private void WeaponSwapAnimation(int currentWeaponIndex)
    {
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeaponState = (WeaponState)currentWeaponIndex;
        CurrentWeapon = AllHeldWeapons[currentWeaponIndex];
        CurrentWeapon.gameObject.SetActive(true);
    }

    public void FireHeldWeapon()
    {
       if(CurrentWeapon != null)
       {
            CurrentWeapon.Fire();
       }
    }
}