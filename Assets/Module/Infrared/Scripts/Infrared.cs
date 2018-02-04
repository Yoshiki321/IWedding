using UnityEngine;
using BuildManager;

public class Infrared : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float pulseSpeed = 1.5f;
    public float noiseSize = 1.0f;
    public float maxWidth = 0.5f;
    public float minWidth = 0.5f;

    private float aniDir = 1.0f;

    private static RaycastHit _hitInfo;
    private LineRenderer _lRenderer;
    private GameObject _pointer;
    private GameObject _line;

    private string Layer = "Graphic3D";

    MeshRenderer mr;
    // Use this for initialization
    void Start()
    {
        _hitInfo = new RaycastHit();

        _pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _pointer.transform.localScale = new Vector3(.03f, .03f, .03f);
        Destroy(_pointer.GetComponent<SphereCollider>());
        mr = _pointer.GetComponent<MeshRenderer>();
        mr.material = Resources.Load("Infrared/Materials/InfraredPoint", typeof(Material)) as Material;
        //_pointer.transform.parent = transform;
        _pointer.layer = LayerMask.NameToLayer(Layer);
        _pointer.gameObject.name = "InfraredSphere";

        mr.material.color = Color.red;

        _line = new GameObject();
        _lRenderer = _line.AddComponent<LineRenderer>();
        Material[] ml = _lRenderer.materials;
        Material m = Resources.Load("Infrared/Materials/Laser", typeof(Material)) as Material;
        _lRenderer.material = m;
        _line.layer = LayerMask.NameToLayer(Layer);
        _line.gameObject.name = "InfraredLine";

        _pointer.transform.parent = SceneManager.Instance.FirstPersonCamera.gameObject.transform;
        _line.transform.parent = SceneManager.Instance.FirstPersonCamera.gameObject.transform;

        SetActive2(_active);

        //_pointer.SetActive(false);
        //_line.SetActive(false);
    }

    public void SetPointColor(Color c)
    {
        if (mr) mr.material.color = c;
    }

    private bool _active = true;

    public void SetActive2(bool value)
    {
        _active = value;

        gameObject.SetActive(value);

        if (_pointer != null)
        {
            _pointer.SetActive(value);
            _line.SetActive(value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out _hitInfo, Mathf.Infinity);
        //Physics.Raycast(transform.position, transform.forward, out _hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer(Layer));
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        _line.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(Time.deltaTime * aniDir * scrollSpeed, 0);
        _line.GetComponent<Renderer>().material.SetTextureOffset("_NoiseTex", new Vector2(-Time.time * aniDir * scrollSpeed, 0.0f));

        float aniFactor = maxWidth;
        //float aniFactor = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
        //aniFactor = Mathf.Max(minWidth, aniFactor) * maxWidth;
        _lRenderer.SetWidth(aniFactor, aniFactor);
        _lRenderer.SetPosition(0, transform.position);

        if (_hitInfo.transform)
        {
            _lRenderer.SetPosition(1, _hitInfo.point);
            _line.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1f * (_hitInfo.distance), _line.GetComponent<Renderer>().material.mainTextureScale.y);
            _line.GetComponent<Renderer>().material.SetTextureScale("_NoiseTex", new Vector2(0.1f * _hitInfo.distance * noiseSize, noiseSize));

            _pointer.GetComponent<Renderer>().enabled = true;
            _pointer.transform.position = _hitInfo.point;
            _pointer.transform.rotation = Quaternion.LookRotation(_hitInfo.normal, _line.transform.up);
            _pointer.transform.eulerAngles = new Vector3(90, _pointer.transform.eulerAngles.y, _pointer.transform.eulerAngles.z);
        }
        else
        {
            _pointer.GetComponent<Renderer>().enabled = false;

            float maxDist = 20000.0f;
            _lRenderer.SetPosition(1, (transform.forward * maxDist));
            _line.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1f * maxDist, _line.GetComponent<Renderer>().material.mainTextureScale.y);
            _line.GetComponent<Renderer>().material.SetTextureScale("_NoiseTex", new Vector2(0.1f * maxDist * noiseSize, noiseSize));
        }
    }

    public static RaycastHit CurrentInfo
    {
        get { return _hitInfo; }
    }
}
