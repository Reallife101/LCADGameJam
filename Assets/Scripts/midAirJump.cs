using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midAirJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.exitStickyMode();
            pm.exitPowerMode();
            pm.setCanJump(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.setCanJump(false);
        }
    }
}
