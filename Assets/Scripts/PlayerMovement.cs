using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Camera myCamera = null;
   

    public void Update()
    {
      

    }

    public void Awake()
    {
        PlayerMovement.myCamera = Camera.main;
    }


  
}