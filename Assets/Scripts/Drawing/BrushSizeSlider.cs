using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushSizeSlider : MonoBehaviour
{
    private Slider slider;

    public static int BrushSize { get; private set; }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        BrushSize = (int)slider.value;
    }
}