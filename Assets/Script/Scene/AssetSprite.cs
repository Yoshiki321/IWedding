using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSprite : ObjectSprite
{
    public virtual void Instantiate(ObjectVO vo)
    {
        VO = vo;
    }

    protected ObjectVO _vo;

    public override AssetVO VO
    {
        get { return _vo; }
        set
        {
            _vo = value as ObjectVO;
            FillObjectVO(_vo);
        }
    }

    public virtual Transform parent
    {
        set
        {
            transform.parent = value;

            transform.localPosition = Vector3.zero;
        }
        get { return transform.parent; }
    }

    protected virtual void FillObjectVO(ObjectVO vo)
    {
        id = vo.id;

        TransformVO transformVO = VO.GetComponentVO<TransformVO>();
        if (transformVO != null)
        {
            x = transformVO.x;
            y = transformVO.y;
            z = transformVO.z;

            scaleX = transformVO.scaleX;
            scaleY = transformVO.scaleY;
            scaleZ = transformVO.scaleZ;

            rotationX = transformVO.rotateX;
            rotationY = transformVO.rotateY;
            rotationZ = transformVO.rotateZ;
        }
    }

    public virtual string layer
    {
        set
        {
            this.gameObject.layer = LayerMask.NameToLayer(value);

            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer = LayerMask.NameToLayer(value);
            }
        }
    }

    override public void UpdateVO()
    {
        TransformVO vo = VO.GetComponentVO<TransformVO>();

        vo.x = x;
        vo.y = y;
        vo.z = z;

        vo.scaleX = scaleX;
        vo.scaleY = scaleY;
        vo.scaleZ = scaleZ;

        vo.rotateX = rotationX;
        vo.rotateY = rotationY;
        vo.rotateZ = rotationZ;
    }

    protected virtual void UpdatePosition()
    {

    }

    protected virtual void UpdateScale()
    {

    }

    protected virtual void UpdateRotation()
    {

    }

    protected virtual void UpdateRotate(Vector3 rotationAxis, float angleInDegrees, Vector3 pivotPoint)
    {

    }

    public virtual float x
    {
        set { transform.position = new Vector3(value, transform.position.y, transform.position.z); UpdatePosition(); }
        get { return transform.position.x; }
    }

    public virtual float y
    {
        set { transform.position = new Vector3(transform.position.x, value, transform.position.z); UpdatePosition(); }
        get { return transform.position.y; }
    }

    public virtual float z
    {
        set { transform.position = new Vector3(transform.position.x, transform.position.y, value); UpdatePosition(); }
        get { return transform.position.z; }
    }

    public virtual float scaleX
    {
        set { transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z); UpdateScale(); }
        get { return transform.localScale.x; }
    }

    public virtual float scaleY
    {
        set { transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z); UpdateScale(); }
        get { return transform.localScale.y; }
    }

    public virtual float scaleZ
    {
        set { transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value); UpdateScale(); }
        get { return transform.localScale.z; }
    }

    public virtual float rotationX
    {
        set { transform.localRotation = Quaternion.Euler(value, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z); UpdateRotation(); }
        get { return transform.localRotation.eulerAngles.x; }
    }

    public virtual float rotationY
    {
        set { transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, value, transform.localRotation.eulerAngles.z); UpdateRotation(); }
        get { return transform.localRotation.eulerAngles.y; }
    }

    public virtual float rotationZ
    {
        set { transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, value); UpdateRotation(); }
        get { return transform.localRotation.eulerAngles.z; }
    }

    public virtual void Rotate(Vector3 rotationAxis, float angleInDegrees, Vector3 pivotPoint)
    {
        Transform objectTransform = gameObject.transform;
        Vector3 fromPivotToPosition = objectTransform.position - pivotPoint;
        Quaternion rotationQuaternion = Quaternion.AngleAxis(angleInDegrees, rotationAxis);
        fromPivotToPosition = rotationQuaternion * fromPivotToPosition;
        objectTransform.Rotate(rotationAxis, angleInDegrees, Space.World);
        objectTransform.position = pivotPoint + fromPivotToPosition;

        UpdateRotate(rotationAxis, angleInDegrees, pivotPoint);
    }
}
