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
    [SerializeField] Weapon currentWeapon = null;
    [SerializeField] Weapon[] availableWeapons;
    int[] ammo = new int[(int)WeaponState.Total];

    [SerializeField] float scrollWheelBreakpoint = 1.0f;
    private float scrollWheelDelta = 0.0f;
    int currentWeaponIndex;
    float lastShot;

    
    public void Start()
    {  
        for(int i = 0; i < ammo.Length; i++){
            ammo[i] = availableWeapons[currentWeaponIndex].ammunition;
        }
    }
    public void Update()
    {
        HandleWeaponSwap();
        currentWeapon = availableWeapons[currentWeaponIndex];

        if(currentWeapon.fireRate < Time.time && ammo[currentWeaponIndex] > 0){
            StartCoroutine(FireRoutine());
        }
    }

    IEnumerator FireRoutine(){
        switch(currentWeapon.fireMode){
            case FireMode.Single:
                if(Input.GetButtonDown("Fire1")){
                    FireWeapon();
                }
                break;

            case FireMode.Auto:
                if(Input.GetButton("Fire1")){
                    FireWeapon();
                }
                break;
            case FireMode.Burst:
                if(Input.GetButtonDown("Fire1")){
                    for(int i = 0; i < currentWeapon.burstAmmount; i++){
                        FireWeapon();
                        yield return new WaitForSeconds(currentWeapon.burstIntervall);
                    }
                }
                break;
        }
        yield return null;
    }

    void FireWeapon(){
        lastShot = Time.time;
        currentWeapon.Shoot();
        ammo[currentWeaponIndex]--;
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