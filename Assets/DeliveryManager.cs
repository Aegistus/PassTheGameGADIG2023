using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track of how many objects need to be delivered.
// The current behavior is that EVERY object tagged NeedToDeliver MUST be delivered to
// a flag pole.
public class DeliveryManager : MonoBehaviour
{
    private static DeliveryManager _instance = null;

    int deliveriesNeeded = 1;
    public int DeliveriesMade = 0;

    private GameObject goodJobText;
    private TMPro.TMP_Text deliverTrackText;

    float youWinTimer = -1.0f; // Used to delay the next-scene call after you win
    const float YOU_WIN_TIMER_LENGTH = 1.0f; // Length of that delay

    // This is so we can turn off the "deliveries completed" text in the tutorial level,
    // where it clashes with the other text.
    public bool EnableDeliveryTracker = true;

    public static DeliveryManager Instance
    {
        get
        {
            return _instance;
        }
    }

    // Updates the text to match the delivery counter.
    void UpdateDeliverTrackText()
    {
        deliverTrackText.text = "deliveries completed: " + DeliveriesMade + " / " + deliveriesNeeded;
    }

    private void Awake()
    {
        _instance = this;

        deliveriesNeeded = GameObject.FindGameObjectsWithTag("NeedToDeliver").Length;

        goodJobText = transform.Find("GoodJobText").gameObject;
        goodJobText.SetActive(false); // hide the good job text

        deliverTrackText = transform.Find("DeliverTrackText").GetComponent<TMPro.TMP_Text>();
        UpdateDeliverTrackText();

        if(!EnableDeliveryTracker)
        {
            deliverTrackText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(youWinTimer > 0.0) // Only tick the timer once you win.
        {
            youWinTimer -= Time.deltaTime;
            if(youWinTimer < 0.0)
            {
                GameManager.Instance.LoadNextGameLevel();
            }
        }
    }

    private void YouWin()
    {
        if (youWinTimer > 0) return;

        goodJobText.SetActive(true); // Show the good job text then move on to next level
        youWinTimer = YOU_WIN_TIMER_LENGTH;
    }

    public void AddDelivery()
    {
        DeliveriesMade += 1;
        if(DeliveriesMade >= deliveriesNeeded)
        {
            YouWin();
        }

        // Must always update this
        UpdateDeliverTrackText();
    }

    public void MinusDelivery()
    {
        DeliveriesMade -= 1;

        UpdateDeliverTrackText();
    }
}
