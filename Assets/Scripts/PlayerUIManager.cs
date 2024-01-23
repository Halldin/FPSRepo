using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI LastObjectHitText = null;
    public PlayerDataExample myPlayerData = null;
    void Update()
    {
        //BÅDA RADER BETYDER SAMMA SAK!
        //LastObjectHitText.text = myPlayerData.LastObjectHit;
        LastObjectHitText.text = PlayerMovement.GlobalPlayerData.LastObjectHit;
    }
}
