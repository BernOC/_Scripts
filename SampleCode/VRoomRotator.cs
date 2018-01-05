//Technical issues with this feature. Player can easily mess with rotation of HMD so as physical and virtual room are not alligned.
//And slight innacurracies will occur. So can a hack still be found so as this works with only the HMD or is a body/room tracker device needed?
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRoomRotator : MonoBehaviour
{
    private Transform vRoom;

    //Player and scripts
    private Transform player;
    private PlayerMotorOld playerMotorScript;
    private PlayerControllerOld playerControllerScript;

    void Awake()
    {
        vRoom = transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMotorScript = player.GetComponent<PlayerMotorOld>();
        playerControllerScript = player.GetComponent<PlayerControllerOld>();
    }

    void Update()
    {
            //Start yawing (yawing = rotating around y-axis
            if (Input.GetButtonDown("RotateRoom"))//= "R" key
            {
                playerControllerScript.SetvRoomYawing(true);//Stops rotating the player's camera
            }
        
            //Stop yawing
            if (Input.GetButtonUp("RotateRoom"))
            {
                playerControllerScript.SetvRoomYawing(false);//Re-enables the player's camera rotation
            }
    }

    void FixedUpdate()
    {
        //Mouse rotation is meant to emulate the y-rotation of a player's headset.

        //Yaw = Y Axes
        if (Input.GetButton("RotateRoom"))
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                //Rotate the VRoom around the player
                vRoom.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X"));
            }
        }
    }
}
