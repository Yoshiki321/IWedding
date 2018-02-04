using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryLine : MonoBehaviour
{
    private Material mat;

    private Vector2 startPoint;

    private Vector2 entPoint;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Update()
    {
    }
}
