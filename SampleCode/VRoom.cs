using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRoom : MonoBehaviour {
    private bool docked = false;

    public void SetDocked(bool _docked)//Set by portal if teleporting to a docking station
    {
        docked = _docked;
    }   

    public bool GetDocked()
    {
        return docked;
    }

}
