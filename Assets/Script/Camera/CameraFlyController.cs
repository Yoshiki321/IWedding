using UnityEngine;
using System.Collections;

public class CameraFlyController : MonoBehaviour
{
    private float speed = 6f;

    private Transform tr;

    private Vector3 mpStart;
    private Vector3 originalRotation;

    private float t = 0f;

    void Awake()
    {
        tr = GetComponent<Transform>();
        t = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float forward = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { forward += 1.5f; }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { forward -= 1.5f; }

        float right = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { right += 1.5f; }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { right -= 1.5f; }

        float up = 0f;
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)) { up += 1.5f; }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.C)) { up -= 1.5f; }

        float dT = Time.realtimeSinceStartup - t;
        t = Time.realtimeSinceStartup;

        tr.position += tr.TransformDirection(new Vector3(right, up, forward) * speed * (Input.GetKey(KeyCode.LeftShift) ? 2f : 1f) * dT);

        Vector3 mpEnd = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            originalRotation = tr.localEulerAngles;
            mpStart = mpEnd;
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 offs = new Vector2((mpEnd.x - mpStart.x) / Screen.width, (mpStart.y - mpEnd.y) / Screen.height);
            tr.localEulerAngles = originalRotation + new Vector3(offs.y * 360f, offs.x * 360f, 0f);
        }
    }
}
