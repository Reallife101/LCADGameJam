using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.enterPowerMode();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.exitPowerMode();
        }
    }
}
