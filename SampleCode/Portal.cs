using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private Transform vRoomAndPlayer;
    private Transform vRoom;
    private VRoom vRoomScript;

    private Transform exit;

    public bool dockingStation = false;//Portals leading to a docking station disable VRoom movement mechanics

    void Awake()
    { 
        vRoomAndPlayer = GameObject.FindGameObjectWithTag("VRoomAndPlayer").transform;
        if(vRoomAndPlayer == null)
        {
            Debug.LogError("VRoomAndPlayer not found!");
        }

        vRoom = vRoomAndPlayer.Find("VRoom");
        if(vRoom == null)
        {
            Debug.LogError("VRoom not found!");
        }

        vRoomScript = vRoom.GetComponent<VRoom>();
        if (vRoomScript == null)
        {
            Debug.LogError("vRoomScript not found!");
        }

        exit = FindExit();
    }

    public Transform FindExit()
    {
        //Finds the corresponding Exit's name
        string _exitName = name + "Exit";
        //And then the exit's transform
        Transform _foundExit = GameObject.Find(_exitName).transform;

        if (_foundExit == null)
        {
            Debug.LogError("There is no corresponding exit for this portal. Make sure the exit and portal have similar names, except the exit's name must have 'Exit' appended to it.");
        }

        return _foundExit;
    }

    //Just sets position and rotation of player to that of the exit's
    //This is activated through the LineOfSight script
    public void TeleportVRoomAndPlayer()
    {
        vRoomAndPlayer.transform.position = exit.position;
        vRoomAndPlayer.transform.rotation = exit.rotation;//TODO: set so player is always rotated to exit's forward and VRoom is oriented properly
        
        //TODO: clean docking feature up

        //???: is get check any better for performance
        bool _docked = vRoom.GetComponent<VRoom>().GetDocked();//Check whether the VRoom is already docked

        if (_docked && !dockingStation)//Portals leading to a docking station disable VRoom movement mechanics
        {
            ReleaseVRoom();
        }

        else if(!_docked && dockingStation)
        {
            DockVRoom();
        }
    }

    //Disables VRoom's movement scripts for this location
    private void DockVRoom()
    {
        vRoomScript.SetDocked(true);

        vRoom.GetComponent<VRoomRotator>().enabled = false;
        vRoom.GetComponent<VRoomTeleporter>().enabled = false;
    }

    //After moving to a non docking station area re-enables VRoom's movement scripts
    private void ReleaseVRoom()
    {
        vRoomScript.SetDocked(false);

        vRoom.GetComponent<VRoomRotator>().enabled = true;
        vRoom.GetComponent<VRoomTeleporter>().enabled = true;
    }
}
