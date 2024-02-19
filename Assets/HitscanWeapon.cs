using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Hitscan")]
public class HitscanWeapon : Weapon
{
    [SerializeField] float range;
    [SerializeField] AnimationCurve falloffCurve;
    [SerializeField] float bulletRadius;
    [SerializeField] LayerMask collisionLayers;

    public override void Shoot(){
        Transform cameraTransform = Camera.main.transform;
        for(int i = 0; i < pelletsPerShot; i++){
            Physics.SphereCast(cameraTransform.position, bulletRadius, cameraTransform.forward, out RaycastHit hitInfo, range, collisionLayers);

            if(hitInfo.collider != null){
                float falloff = Mathf.InverseLerp(0, range, hitInfo.distance);
                float damage = falloffCurve.Evaluate(falloff) * damagePerPellet;
                hitInfo.collider.gameObject.GetComponent<IDamagable>()?.Damaged(damage);
                Debug.Log("hit");
            }
        }
    }
}
