using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingResult : MonoBehaviour
{
    // 이전 씬에서 그렸던 그림이 캔버스에 나타납니다.
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = PaintCanvas.Texture;
    }
}
