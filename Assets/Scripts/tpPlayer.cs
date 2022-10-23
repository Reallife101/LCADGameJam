using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform checkpoint;
    public pauseMenu pause;
    
    public void tpPlay()
    {
        player.transform.position = checkpoint.position;
        playerMovement pm = player.GetComponent<playerMovement>();

        if (pm)
        {
            pm.exitPowerMode();
            pm.exitStickyMode();
            pause.togglePause();
            pause.togglePause();
        }
    }
}
