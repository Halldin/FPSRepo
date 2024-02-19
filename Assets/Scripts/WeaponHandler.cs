using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponState
{
    Pistol,
    Shotgun,
    AR,
    Sniper,
    RocketLauncher,
    Granade,
    Unarmed,
    Total
}
public class WeaponHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement myPlayerMovement = null;
    [SerializeField] Weapon[] availableWeapons;
    [SerializeField] Weapon currentWeapon = null;

    [SerializeField] float scrollWheelBreakpoint = 1.0f;
    private float scrollWheelDelta = 0.0f;
    int currentWeaponIndex;

    
    public void Start()
    {  
    
    }
    public void Update()
    {
        HandleWeaponSwap();
        currentWeapon = availableWeapons[currentWeaponIndex];

        if (Input.GetMouseButtonUp(0) && currentWeapon != null)
        {
            currentWeapon.Shoot();
        }
    }

    private void HandleWeaponSwap()
    {
        scrollWheelDelta += Input.mouseScrollDelta.y;
        
        if (Mathf.Abs(scrollWheelDelta) > scrollWheelBreakpoint)
        {
            int swapDirection = (int)Mathf.Sign(scrollWheelDelta);
            scrollWheelDelta = 0.0f;

            currentWeaponIndex += swapDirection;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total - 1;
            }
            if (currentWeaponIndex >= (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
        }
    }
}