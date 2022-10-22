using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement pm = collision.GetComponent<playerMovement>();

        if (pm)
        {
            pm.enterStickyMode();
        }
    }
}
