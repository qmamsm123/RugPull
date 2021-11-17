using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBrush : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var pallet = hit.collider.GetComponent<PaintCanvas>();
                if (pallet != null)
                {
                    Debug.Log(hit.textureCoord);
                    Debug.Log(hit.point);

                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    BrushAreaWithColor(pixelUV, ColorPicker.SelectedColor, BrushSizeSlider.BrushSize);
                }
            }
        }
    }

    private void BrushAreaWithColor(Vector2 pixelUV, Color color, int size)
    {
        for (int x = -size; x < size; x++)
        {
            for (int y = -size; y < size; y++)
            {
                if ((int)pixelUV.x + x < 0 || (int)pixelUV.y + y < 0) continue;
                PaintCanvas.Texture.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, color);
            }
        }

        PaintCanvas.Texture.Apply();
    }
}