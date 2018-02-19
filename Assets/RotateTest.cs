using UnityEngine;
using System.Collections;

public class RotateTest : MonoBehaviour
{
    public LineRenderer line2;
    public float angle = 30f;

    public GameObject cube;
    public GameObject cube1;

    private Vector3 v0;
    private Vector3 v1;
    private Vector3 v2;
    private Vector3 v3;
    private Vector3 v4;
    private Vector3 vCenter;
    private Vector3 m;
    private Vector3 m1;
    private Vector3 m2;
    private Vector3 m3;
    void Start()
    {
        v0 = new Vector3(3f, 0f, 1f);
        v1 = new Vector3(1f, 0f, 3f);
        v2 = new Vector3(999f, 999f, 999f);
        v3 = new Vector3(6f, 0f, 4f);
        vCenter = new Vector3(2f, 0f, 2f);

        m = gameObject.transform.position - cube.transform.position;
        m1 = gameObject.transform.position - v2;
        m2 = Vector3.Cross(m, m1);
        m3 = Vector3.MoveTowards(gameObject.transform.position, gameObject.transform.position + m2, 1);
    }

    void Update()
    {
        cube1.transform.position = MathUtils3D.RotateRound(
            m3,
            cube.transform.position,
            m, angle);
        angle++;
    }
}