using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSprite2D : AssetSprite
{
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;

    protected GameObject _textureObject;

    override public void Instantiate(ObjectVO vo)
    {
        _textureObject = new GameObject();
        _textureObject.transform.parent = transform;

        spriteRenderer = _textureObject.AddComponent<SpriteRenderer>();

        layer = "Graphic2D";

        base.Instantiate(vo);
    }

    public override AssetVO VO
    {
        get
        {
            return base.VO;
        }
        set
        {
            base.VO = value;

            assetId = (value as ObjectVO).assetId as string;
        }
    }

    protected Texture2D _texture;

    private string _assetId;

    public string assetId
    {
        set
        {
            if (_assetId == value) return;

            _assetId = value;

            if (boxCollider != null)
            {
                Destroy(boxCollider);
            }

            _texture = (VO as ObjectVO).topImgTexture;
            if (_texture == null)
            {
                _texture = (Texture2D)Resources.Load("TopImg/Stage");
            }
            if (_texture != null)
                spriteRenderer.sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(.5f, .5f));
            boxCollider = _textureObject.AddComponent<BoxCollider2D>();

            UpdateSize();
        }
        get { return _assetId; }
    }

    public virtual void UpdateSize()
    {

    }

    public float width
    {
        get
        {
            if (boxCollider == null) return 0;
            return (boxCollider.bounds.max.x - boxCollider.bounds.min.x);
        }
    }

    public float height
    {
        get
        {
            if (boxCollider == null) return 0;
            return (boxCollider.bounds.max.y - boxCollider.bounds.min.y);
        }
    }

    override protected void FillObjectVO(ObjectVO vo)
    {
        id = vo.id;

        TransformVO tvo = vo.GetComponentVO<TransformVO>();

        x = tvo.x;
        y = tvo.z;

        //scaleX = tvo.scaleX;
        //scaleY = tvo.scaleY;
        //scaleZ = tvo.scaleZ;

        rotationX = tvo.rotateX;
        rotationZ = 360 - tvo.rotateY;

        _textureObject.transform.localPosition = Vector3.zero;
    }

    private void OnEnable()
    {
        UpdateSize();
    }

    override public void UpdateVO()
    {
        TransformVO vo = VO.GetComponentVO<TransformVO>();

        vo.x = x;
        vo.z = y;

        //vo.scaleX = scaleX;
        //vo.scaleY = scaleY;
        //vo.scaleZ = scaleZ;

        vo.rotateX = rotationX;
        vo.rotateY = 360 - rotationZ;
    }

    override protected void UpdatePosition()
    {
        _textureObject.transform.localPosition = Vector3.zero;
    }

    override protected void UpdateScale()
    {
        _textureObject.transform.localPosition = Vector3.zero;
    }

    override protected void UpdateRotation()
    {
        _textureObject.transform.localPosition = Vector3.zero;
    }
}
