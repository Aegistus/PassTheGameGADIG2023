using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlag : MonoBehaviour
{
    // How many deliveries have been made. Tracked through OnTriggerEnter and OnTriggerExit.
    // This can be tweaked by e.g. removing the OnTriggerExit logic.
    int currentDeliveryCount = 0;

    // Just to avoid typos
    const string NEED_TO_DELIVER_TAG = "NeedToDeliver";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(NEED_TO_DELIVER_TAG))
        {
            // Mark the object as delivered.
            NeedToDeliver d = collision.GetComponent<NeedToDeliver>();
            d.DeliveryAccepted();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(NEED_TO_DELIVER_TAG))
        {
            // Un-mark the object as delivered.
            // Remove this if you want to make the game easier.
            NeedToDeliver d = collision.GetComponent<NeedToDeliver>();
            d.DeliveryUnaccepted();
        }
    }
}
