using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class SceneAmbientLight : MonoBehaviour
{
    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        float t = 0;
        float r = 255 - ((255 - 64) / 12) * t;
        float g = 241 - ((241 - 60) / 12) * t;
        float b = 214 - ((214 - 53) / 12) * t;

        Color color = new Color();
        color.r = r / 255;
        color.g = g / 255;
        color.b = b / 255;
        color.a = 255;

        _light.color = color;
    }
}
