using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaHighlighter : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private Color originalColor;

    public float highlightIntensity = 50f;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.GetColor("_TintColor");
    }

    //Highlights object by maxing alpha while its being looked at 
    public void Highlight()
    {
        Color _newTintColor = new Vector4(originalColor.r, originalColor.g, originalColor.b, highlightIntensity);

        meshRenderer.material.SetColor("_TintColor", _newTintColor);
    }

    public void StopHighlighting()
    {
        meshRenderer.material.SetColor("_TintColor", originalColor);
    }
}
