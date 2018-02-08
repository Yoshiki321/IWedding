using System;
using UnityEngine;

public class DrawLineCollider : MonoBehaviour
{
    public void AddCollider(Vector2 f, Vector2 t)
    {
        CapsuleCollider capsule = gameObject.AddComponent<CapsuleCollider>();
        capsule.direction = 2;
        capsule.radius = 7f;
        capsule.center = Vector3.zero;
        capsule.transform.localPosition = f + (t - f) / 2;
        capsule.transform.localRotation = Quaternion.Euler(0, 0, PlaneUtils.Angle(f, t));
        capsule.direction = 0;
        capsule.height = (t - f).magnitude;
    }

    private Action _downFun;
    private Action _upFun;
    private Action _overFun;
    private Action _outFun;
    private Action _rightClickFun;

    public void AddMouseDown(Action f)
    {
        _downFun = f;
    }

    public void AddMouseUp(Action f)
    {
        _upFun = f;
    }

    public void AddRightMouseUp(Action f)
    {
        _rightClickFun = f;
    }

    public void AddMouseOver(Action f)
    {
        _overFun = f;
    }

    public void AddMouseOut(Action f)
    {
        _outFun = f;
    }

    void OnMouseDown()
    {
        _downFun();
    }

    void OnMouseUp()
    {
        _upFun();
    }

    void OnMouseOver()
    {
        _overFun();

        if (Input.GetMouseButtonUp(1))
        {
            _rightClickFun();
        }
    }

    void OnMouseExit()
    {
        _outFun();
    }
}