using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponState
{
    Unarmed,
    HitScan,
    Projectile, 
    Total
}
public class WeaponHandler : MonoBehaviour
{ 
    [Header("Unarmed = Element 0 \n" +
        "Hitscan = Element 1 \n" +
        "Projectile = Element 2")]
    public Weapon[] AvailableWeapons = new Weapon[(int)WeaponState.Total];
    public Weapon CurrentWeapon = null;

    public float ScrollWheelBreakpoint = 1.0f;
    private float ScrollWheelDelta = 0.0f;
    public void Update()
    {
        HandleWeaponSwap();
    }

    private void HandleWeaponSwap()
    { 

        ScrollWheelDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(ScrollWheelDelta) > ScrollWheelBreakpoint)
        {
            int swapDirection = (int)Mathf.Sign(ScrollWheelDelta);
            ScrollWheelDelta -= swapDirection * ScrollWheelBreakpoint;

            int currentWeaponIndex = (int)CurrentWeapon.WeaponType;
            currentWeaponIndex += swapDirection;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + currentWeaponIndex;
            }
            if (currentWeaponIndex >= (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
            CurrentWeapon = AvailableWeapons[currentWeaponIndex];
        }
    }
}



//MouseAxisDelta -= swapDirectionNumber * MouseAxisWeaponSwapBreakpoint;
//int currentWeaponIndex = (int)CurrentWeaponState + swapDirectionNumber;
//if (currentWeaponIndex >= (int)WeaponState.Total)
//{
//    currentWeaponIndex = 0;
//}
//else if (currentWeaponIndex < 0)
//{
//    currentWeaponIndex = (int)WeaponState.Total - 1;
//}
//WeaponSwapAnimation(currentWeaponIndex);
