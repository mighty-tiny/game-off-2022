using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    /* 
     * SINGLETON REFERENCE TO GAMEEVENTS
     * There will only be a *single* referene
     * to the GameEvents script. :) 
     */
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    /* 
     * DOORWAY TRIGGER ENTER START
     * When a user walks into a door trigger, 
     * this function will be called.
     */
    public event Action<int> onPickupCollected;
    public void OnPickupCollected(int pointValue)
    {
        if (onPickupCollected != null)
        {
            onPickupCollected(pointValue);
        }
    } // PICKUP TRIGGER ENTER - END

    /* 
     * DOORWAY TRIGGER ENTER START
     * When a user walks into a door trigger, 
     * this function will be called.
     */
    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if(onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    } // DOORWAY TRIGGER ENTER - END

    /* DOORWAY TRIGGER EXIT START
     * When the player exits the door trigger,
     * this function will be called.
     */
    public event Action<int> onDoorwayTriggerExit; 
    public void DoorwayTriggerExit(int id)
    {
        if(onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit(id);
        }
    } // DOORWAY TRIGGER EXIT - END
}
