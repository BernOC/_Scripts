//TODO: Have objects detect they are being looked at
// rather than this script detecting each individual one.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {
    private Ray lineOfSight;
    private Vector3 lineOfSightEndPoint;
    public float rangeOfSight = 40f;

    public float enlargeScaleRate = 1.2f;
    
    private GameObject player;

    private Transform currentlyLookingAt;
    private Transform highlightedObject;

    //TODO: remove book from here
    public GameObject book;
    public float bookLoadDistance = 1f;

    //Highlight objects on the highlightable layer when they are being looked at
    //TODO: clean up Highlting()
    private Transform highlightInstance;
    private bool isHighlighting;

    //For Portals on maps
    private Transform alphaHighlightedObject;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        lineOfSight = GetLineOfSightRay();

        RaycastHit _hit;

        if (Physics.Raycast(lineOfSight, out _hit))
        {
            //What, if anything, is the player currently looking at
            currentlyLookingAt = _hit.transform;

            if (Input.GetButtonDown("Select"))
            {
                if (currentlyLookingAt.gameObject.layer == 11)//Selectable layer
                {
                    currentlyLookingAt.GetComponent<Selectable>().Selected();//Tells the object it's been selected
                }          
                    
            }

            //Scale Highlighting 
            if (highlightedObject != null)
            {
                if (currentlyLookingAt != highlightedObject)
                {
                    //Stop highlighting object if its no longer being looked at 
                    highlightedObject.GetComponent<Highlighter>().StopHighlighting();
                }
            }

            //Highlight objects on the highlightable layer
            if (_hit.transform.gameObject.layer == 8)
            {
                highlightedObject = _hit.transform;

                highlightedObject.GetComponent<Highlighter>().StartHighlighting();
            }

            //Alpha Highlighting
            if (alphaHighlightedObject != null)
            {
                //If no longer looking at the same object -TODO: or nothing at all?
                if (currentlyLookingAt != alphaHighlightedObject)
                {
                    alphaHighlightedObject.GetComponent<AlphaHighlighter>().StopHighlighting();
                }
            }

            //Increase the alpha of objects on this layer for highlighting
            if (_hit.transform.gameObject.layer == 9)
            {
                alphaHighlightedObject = _hit.transform;

                alphaHighlightedObject.GetComponent<AlphaHighlighter>().Highlight();
            }

            //App detection
            if (_hit.transform.tag == "App")
            {
                currentlyLookingAt = _hit.transform;

                currentlyLookingAt.root.GetComponent<DestroyIfNotLookedAt>().ResetTicker();//Tells spacetop @ root it's being looked at to keep it alive   

                if (Input.GetMouseButtonDown(0))
                {
                    //Load the app's space
                    currentlyLookingAt.GetComponent<App>().LoadAppScene();
                }
            }

            //Portals on map
            if (_hit.transform.tag == "Portal")
            {
                currentlyLookingAt = _hit.transform;

                currentlyLookingAt.root.GetComponent<DestroyIfNotLookedAt>().ResetTicker();//Tells map @ root it's being looked at to keep it alive   

                if (Input.GetMouseButtonDown(0))
                {
                    currentlyLookingAt = _hit.transform;

                    //Relocate ShipAndPlayer 
                    currentlyLookingAt.GetComponent<Portal>().TeleportVRoomAndPlayer();

                }
            }
        }
    }

    //Shoots and returns ray for the player's line of sight
    public Ray GetLineOfSightRay()
    {
        Ray _lineOfSight = new Ray(transform.position, transform.forward * rangeOfSight);
        Debug.DrawRay(transform.position, transform.forward * rangeOfSight, Color.green);

        return _lineOfSight;
    }

    //Returns the point in world space @ _distance units along the line of sight
    public Vector3 GetLineOfSightPoint(float _distance)
    {
        Ray _lineOfSight = GetLineOfSightRay();
        Vector3 _lineOfSightEndPoint = _lineOfSight.GetPoint(_distance);

        return _lineOfSightEndPoint;
    } 

    //Accessed by VRoom's teleporter
    public Vector3 GetVRoomTelepoint(float _scrolledDistance)
    {
        //Point at rangeOfSight and input _scrolledDistance along the lineOfSight
        Vector3 _telepoint = GetLineOfSightPoint(rangeOfSight + _scrolledDistance);

        return _telepoint;
    }

    //???: Which is better for visual feedback, enlarging the object or having its alpha glow.
    private IEnumerator EnlargeWhileLookingAt(Transform _lookedAt)
    {
        bool _enlarged = false;
        print("A");

        while(currentlyLookingAt == _lookedAt)
        {
            if (_enlarged == false)
            {
                _enlarged = true;
                _lookedAt.localScale += new Vector3(2f, 2f, 2f);
                yield return null;
            }
        }
        //Reset scale to (1,1,1)
        _lookedAt.localScale.Normalize();
        yield return null;
    }
}
