using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBelt : MonoBehaviour
{
    //Spacetop:
    public Transform spaceTop;
    private Transform spaceTopInstance;
    public float spaceTopDistance = 2.7f;
    public float spaceTopHeight = 1.4f;

    //Map:
    public Transform map;
    private Transform mapInstance;
    public float mapDistance = 1.4f;
    public float mapHeight = 1.4f;

    //Player - lineofSightScript
    private Transform player;
    private Transform playerCam;
    private LineOfSight lineOfSightScript;


    void Awake()
    {
        player = transform;
        if(player == null)
        {
            Debug.LogError("Player not found!");
        }

        playerCam = player.Find("Camera");
        if (playerCam == null)
        {
            Debug.LogError("PlayerCam not found!");
        }

        lineOfSightScript = playerCam.GetComponent<LineOfSight>();
        if(lineOfSightScript == null)
        {
            Debug.LogError("LineOfSigthScript not found!");
        }
    }

    void Update()
    {
        //SpaceTop: Input = Space-Bar, but gesture tracking = snap of the fingers!
        if (Input.GetButtonDown("SpaceTop"))
        {
            //Destroys any pre-existing SpaceTop 
            if (spaceTopInstance != null)
            {
                Destroy(spaceTopInstance.gameObject);
            }

            //Finds the point along the player's line of sight 
            Vector3 _spaceTopInstancePosition = lineOfSightScript.GetLineOfSightPoint(spaceTopDistance);       
             //Alter the height
            _spaceTopInstancePosition = new Vector3(_spaceTopInstancePosition.x, player.position.y + spaceTopHeight, _spaceTopInstancePosition.z);
            //Create the SpaceTop  
            spaceTopInstance = Instantiate(spaceTop, _spaceTopInstancePosition, spaceTop.rotation);
            //Rotate the SpaceTop based on the player's y-rotation
            spaceTopInstance.LookAt(player);
        }

        //Map 
        if (Input.GetButtonDown("Map"))// == "M" key
        {
            //Destroys any pre-existing map
            if (mapInstance != null)
            {
                Destroy(mapInstance.gameObject);
            }

            //Finds the point along the player's line of sight
            Vector3 _mapInstancePosition = lineOfSightScript.GetLineOfSightPoint(mapDistance);
            //Alter the height
            _mapInstancePosition = new Vector3(_mapInstancePosition.x, player.position.y + mapHeight, _mapInstancePosition.z);
            //Create the SpaceTop
            mapInstance = Instantiate(map, _mapInstancePosition, map.rotation);
            //Rotate the map based on the player's y-rotation   
            mapInstance.LookAt(player);
        }
    }
}
