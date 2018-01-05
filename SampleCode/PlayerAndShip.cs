using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndShip : MonoBehaviour
{
    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player == null)
        {
            Debug.LogError("Ship could not find the player");
        }
    }

    public void OrientShip(Vector3 _newShipPosition)//When reorienting the rotation  
    {                                               //ship gravity may be necessary?
        transform.position = _newShipPosition;
    }
}
/*
 * void update()
 *  if(input.r && scroll(up == grow & down == shrink)){ //Motion Controller Input = Both arms extended out to the sides
 *    PlayerAndShip.scale += V3 scaleRate * -+scrollDirection;              scale while InputDown
 *                                                      //To shrink: both arms criss-crossed as if hugging self
 *              
 * }
*/



