using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    private int currentSpaceIndex;

    void Start()
    {
        LockCursors();
    }
    
    public void LockCursors()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetCurrentSpace(int _currentSpace) {
        currentSpaceIndex = _currentSpace;
    }

    public int GetCurrentSpace() {
        return currentSpaceIndex;
    }
}
