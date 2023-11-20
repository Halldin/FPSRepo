using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokemonStatus
{
    Poison,
    Burn,
    Paralyze,
    Sleep,
    Freeze,
    Total
}
public class HitScanExample : MonoBehaviour
{
    public WeaponState State = WeaponState.Total;
    public static Camera myCamera = null;
    public int aNumber = 0;
    public LayerMask ExampleRayMask = 0;
    public List<bool> PokemonStatusEffects = new List<bool>();
    // Start is called before the first frame update
    public void Awake()
    {
        HitScanExample.myCamera = Camera.main;
    }
    void Start()
    {
        PokemonStatusEffects[(int)PokemonStatus.Paralyze] = false;
    }

    // Update is called once per frame
    void Update()
    {
        ExampleRayMask = 0;
        ExampleRayMask = ~(ExampleRayMask + LayerMask.NameToLayer("Ground"));

        Ray mouseRay = HitScanExample.myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(mouseRay,out hit,100.0f, ExampleRayMask, QueryTriggerInteraction.Collide))
        {
            var hitScanOnTarget = hit.transform.gameObject.GetComponent<HitScanExample>();
        }
    }

    

}
