using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * https://forum.unity.com/threads/how-does-onvalidate-work.616372/ 
 * 
 * Private vars aren't serialized without [SerializeField]
 * 
 * keypoints:
 * - Serialization occurs on play mode start.
 * - Serialization occurs when scripts are reloaded.
 * - Serialization (OnValidate) occurs when any of the fields are modified via inspector.
 */

[ExecuteInEditMode]
public class GradientRawImage : MonoBehaviour
{
    public RawImage rawImage;
    public Color color1; // Lerping both image
    public Color color2;

    [SerializeField] // Warning !
    Texture2D backgroundTexture;

    private void Awake()
    {
        backgroundTexture = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        SetColor(color1, color2);
    }

    private void SetColor(Color color1, Color color2)
    {
        backgroundTexture.SetPixels(new Color[] { color2, color1 });
        backgroundTexture.Apply();
        rawImage.texture = backgroundTexture;
    }

    private void OnValidate() // Checking script is loaded or a value is changed in the Inspector
    {
        SetColor(color1, color2);
    }
}
