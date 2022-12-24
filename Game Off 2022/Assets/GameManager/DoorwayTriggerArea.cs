using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayTriggerArea : MonoBehaviour
{
    // ID for Game Events. This will be passed to the GameEvents.
    public int id;

    /* 
     * When the player enters a doorway trigger,
     * pass the id to the GameEvents
     */
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.DoorwayTriggerEnter(id);
    }
    /*
     * When th eplayer exits a doorway trigger,
     * pass the id to the GameEvents
     */
    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.DoorwayTriggerExit(id);
    }
}
