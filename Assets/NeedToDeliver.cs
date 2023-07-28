using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to any object that should be delivered to the goal flag to win.
// Also, attach this to anything marked as NeedToDeliver, as otherwise a bug
// will occur if it touches the flag.
//
// This behavior can of course be changed.
//
// Also, each NeedToDeliver should have a CheckMark child with the check sprite.
// (unless that behavior is changed, of course).
//
// See the Box prefab to see what this should look like.
public class NeedToDeliver : MonoBehaviour
{
    GameObject checkMark;

    private void Start()
    {
        // Get the check sprite & hide it
        checkMark = transform.Find("CheckMark").gameObject;
        checkMark.SetActive(false);
    }

    private void Update()
    {
        // Make the checkmark always face down
        checkMark.transform.eulerAngles = Vector3.zero;
    }

    // Called when this delivery touches the flag and is therefore complete.
    public void DeliveryAccepted()
    {
        checkMark.SetActive(true); // Show the check mark when we are delivered

        // Track all deliveries in one place, the DeliveryManager
        DeliveryManager.Instance.AddDelivery();
        SoundManager.PlayAtPosition("flag_enter", transform.position);
    }

    // Called if this delivery had been winning but now isn't.
    public void DeliveryUnaccepted()
    {
        checkMark.SetActive(false); // Show the check mark when we are un-delivered
        DeliveryManager.Instance.MinusDelivery();
        SoundManager.PlayAtPosition("flag_exit", transform.position);
    }
}

