using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverController : MonoBehaviour
{
    void Start()
    {
        x = 45;
        y = 35;

        Quaternion q = Quaternion.Euler(y, x, 0);
        Vector3 direction = q * Vector3.forward;
        this.transform.position = center - direction * distance;
        this.transform.LookAt(center);
    }

    private bool _Enable;

    public bool Enable
    {
        set { _Enable = value; }
        get { return _Enable; }
    }

    private float x = 0;
    private float y = 0;
    private float speed = 205;
    private float distance = 20;
    private Vector3 center = new Vector3();

    void Update()
    {
        //if (!_Enable) return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else
        {
        }

        float xx = 0;
        float yy = 0;

        if (Input.GetMouseButton(0))
        {
            xx = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
            yy = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
        }

        SetPoint(xx, yy);
    }

    private bool ca = true;
    private bool cs = true;

    void SetPoint(float xx, float yy)
    {
        x += xx;
        if (Can(xx, yy)) y -= yy;

        Quaternion q = Quaternion.Euler(y, x, 0);
        Vector3 direction = q * Vector3.forward;

        ca = distance > 25;
        cs = distance < 4;

        if (ca)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) distance -= Input.GetAxis("Mouse ScrollWheel") * 5;
        }
        else if (cs)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0) distance -= Input.GetAxis("Mouse ScrollWheel") * 5;
        }
        else
        {
            distance -= Input.GetAxis("Mouse ScrollWheel") * 5;
        }

        Vector3 v = center - direction * distance;

        this.transform.position = v;
        this.transform.LookAt(center);
    }

    bool Can(float xx, float yy)
    {
        float xxx = x + xx;
        float yyy = y - yy;
        Quaternion q = Quaternion.Euler(yyy, xxx, 0);
        Vector3 direction = q * Vector3.forward;
        Vector3 v = center - direction * distance;

        if (v.y < 18 && v.y > 1)
        {
            return true;
        }
        return false;
    }

    public void MoveTo(Vector3 vecCenter, Vector3 vecPosition, float time)
    {
        //// 设置缺省中心点
        //center = vecCenter;
        //defaultCenter = vecCenter;
        //defaultPositon = vecPosition;
        //distance = Vector3.Distance(defaultCenter, defaultPositon);

        //// 移动位置（用了DOTween做平滑）
        //Sequence seq = DOTween.Sequence();
        //seq.Append(this.transform.DOMove(vecPosition, time).SetEase(Ease.Linear));
        //seq.Play().OnUpdate(delegate
        //{
        //	this.transform.LookAt(vecCenter);
        //});
    }
}
