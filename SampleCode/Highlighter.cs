using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    bool enlarged = false;

    private Vector3 originalScale;
    private Vector3 enlargedScale;

    public float enlargeRate = 1.35f;
    
    void Awake()
    {
        originalScale = transform.localScale;
        
    }

    void Start()
    {
        enlargedScale = new Vector3(originalScale.x * enlargeRate, originalScale.y * enlargeRate, originalScale.z * enlargeRate);
    }

    public void StartHighlighting()
    {
        if (enlarged == false)
        {
            enlarged = true;
            transform.localScale = enlargedScale;
        }
    }

    public void StopHighlighting()
    {
        transform.localScale = originalScale;
        enlarged = false;
    }
}
