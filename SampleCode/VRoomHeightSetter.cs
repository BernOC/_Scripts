using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRoomHeightSetter : MonoBehaviour {
    private Transform player;

    private float cachedYPlayerPosition;
    private float currentYPlayerPosition;

    public float offset = 0.1f;//For comparing cached to current in Update()

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.LogError("Could not find player!");
        }

        cachedYPlayerPosition = player.position.y;
    }

    void FixedUpdate()
    {
        currentYPlayerPosition = player.position.y;//Track player's y movement

        if(cachedYPlayerPosition <= currentYPlayerPosition - offset || cachedYPlayerPosition >= currentYPlayerPosition + offset)//has the player moved above or below range
        {
            print("PlayerY: " + currentYPlayerPosition);

            Vector3 newVRoomPosition = new Vector3(transform.position.x, currentYPlayerPosition, transform.position.z);
            transform.position = newVRoomPosition; 

            cachedYPlayerPosition = currentYPlayerPosition;//Cache new player's height
        }
    }

}
