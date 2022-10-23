using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midAirJump : MonoBehaviour
{
    private audioManager am;
    private void Start()
    {
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.exitStickyMode();
            pm.exitPowerMode();
            pm.setCanJump(true);
            am.playPowerUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.setCanJump(false);
            am.playPowerDown();
        }
    }
}
