//TODO: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRoomTeleporter : MonoBehaviour {
    public bool yAxis = true;//TODO: make this a drop down list in inspector called "Enabled Axes" including x & z axes too.

    private LineOfSight lineOfSightScript;
    private Vector3 lineOfSightEndPoint;

    private Ray vRoomTeleRay;

    private Vector3 telePoint;

    private float scrolledDistance;
    public float scrollDistanceRate = 2.7f;

    [SerializeField]
    private Transform vRoomPreview;
    private Transform vRoomPreviewInstance;

    private Transform vRoomAndPlayer;
        
    void Start()
    {
        lineOfSightScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LineOfSight>();

        if(lineOfSightScript == null)
        {
            Debug.LogError("Could not find LineOfSightScript");
        }

        vRoomAndPlayer = GameObject.Find("VRoomAndPlayer").transform;
        if(vRoomAndPlayer == null)
        {
            Debug.LogError("Could not find VRoomAndPlayer!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //Destroys last frame's VRoom
            if (vRoomPreviewInstance != null)
            {
                Destroy(vRoomPreviewInstance.gameObject);
            }

            //Scrolling controls VRoom's teleportation distance
            if (Input.mouseScrollDelta != null)
            {
                scrolledDistance += Input.mouseScrollDelta.y * scrollDistanceRate;
            }

            telePoint = lineOfSightScript.GetVRoomTelepoint(scrolledDistance);

            if (!yAxis)//Don't move from current y-position
            {
                telePoint.y = vRoomAndPlayer.position.y;
            }

            //Preview location of VRoom
            vRoomPreviewInstance = Instantiate(vRoomPreview, telePoint, vRoomPreview.transform.rotation);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (vRoomPreviewInstance != null)
            {
                Destroy(vRoomPreviewInstance.gameObject);
            }

            vRoomAndPlayer.transform.position = telePoint;//Teleport vRoomAndPlayer

            scrolledDistance = 0f;//Reset
        }
    }
}
