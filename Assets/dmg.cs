using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmg : MonoBehaviour, IDamagable
{
    public void Damaged(float value)
    {
        print(value);
    }
}
