using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public static Color SelectedColor { get; private set; }

    [SerializeField]
    private Renderer selectedColorPreview;

    private void Start() {
        SelectedColor = Color.white;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var picker = hit.collider.GetComponent<ColorPicker>();
                if (picker != null)
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    SelectedColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

                    selectedColorPreview.material.color = SelectedColor;
                }
            }
        }
    }
}