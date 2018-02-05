using RTEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleLine : MonoBehaviour
{
    LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Standard"));
        _lineRenderer.widthMultiplier = 0.07f;
    }

    void Update()
    {
        RaycastHit hitInfo;
        LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hitInfo, 999999))
        {
            GLPrimitives.Draw3DLine(gameObject.transform.position, hitInfo.point, Color.black, MaterialPool.Instance.GizmoLine);

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, gameObject.transform.position);
            _lineRenderer.SetPosition(1, hitInfo.point);
        }
    }
}
