using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingResult : MonoBehaviour
{
    // ���� ������ �׷ȴ� �׸��� ĵ������ ��Ÿ���ϴ�.
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = PaintCanvas.Texture;
    }
}
