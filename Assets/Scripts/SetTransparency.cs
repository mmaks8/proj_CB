using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransparency : MonoBehaviour
{
    public Color transparentColor;
    private Color initialColor;

    void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
    }

    public void SetTransparent()
    {
        GetComponent<Renderer>().material.color = transparentColor;
    }

    public void SetToNormal()
    {
        GetComponent<Renderer>().material.color = initialColor;
    }
}
