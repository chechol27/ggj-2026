using System;
using UnityEngine;

public class ScreenResolutionFilter : MonoBehaviour
{
    [SerializeField] public Material mat;

    private void Awake()
    {
        mat.SetVector("_ScreenResolution", new Vector4(Mathf.CeilToInt(Screen.width * 0.4f), Mathf.CeilToInt(Screen.height * 0.4f), 0,0));
    }
}
