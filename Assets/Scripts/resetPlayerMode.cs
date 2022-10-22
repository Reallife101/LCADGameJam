using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayerMode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.exitStickyMode();
            pm.exitPowerMode();
        }
    }
}
