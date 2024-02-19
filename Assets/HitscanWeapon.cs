using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Hitscan")]
public class HitscanWeapon : WeaponBase
{
    public float range;
    public AnimationCurve falloff;
    public float bulletRadius;
}
