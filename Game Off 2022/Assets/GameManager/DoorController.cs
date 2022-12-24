using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // ID for Game Events;
    public int id;
    
    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayExit;
    }

    private void OnDoorwayOpen(int id)
    {
        if(id == this.id)
        {
            Debug.Log("Doorway trigger entered | ID: " + id);
        }
    }

    private void OnDoorwayExit(int id)
    {
        if(id == this.id)
        {
            Debug.Log("Doorway trigger exited | ID: " + id); 
        }
    }
}
