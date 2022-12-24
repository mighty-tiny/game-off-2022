using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    /* SERIALIZED */
    [SerializeField] private int pointValue;
    [SerializeField] private int id;

    /* 
     * When the player collects a pickup,
     * pass the point to the GameEvents.
     */
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.OnPickupCollected(pointValue);
        gameObject.SetActive(false);
    }
   
}
