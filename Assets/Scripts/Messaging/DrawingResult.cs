using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingResult : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = PaintCanvas.Texture;
    }
}
