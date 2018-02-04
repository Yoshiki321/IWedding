using Build3D;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSpotlightComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        _item.VO.AddComponentVO<MultipleSpotlightVO>();
    }

    public GameObject spotlight;

    public int upSpotCount;
    public int downSpotCount;

    public float upSpacing;
    public float downSpacing;
    public float verticalSpacing;

    public float upFromY;
    public float upToY;
    public float upFromX;
    public float upToX;
    public float upTimeY;
    public float upTimeX;

    public float downFromY;
    public float downToY;
    public float downFromX;
    public float downToX;
    public float downTimeY;
    public float downTimeX;

    private MultipleSpotlightVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<MultipleSpotlightVO>();

            spotlight = _vo.spotlight;

            upSpotCount = _vo.upSpotCount;
            downSpotCount = _vo.downSpotCount;

            upSpacing = _vo.upSpacing;
            downSpacing = _vo.downSpacing;
            verticalSpacing = _vo.verticalSpacing;

            upFromY = _vo.upFromY;
            upToY = _vo.upToY;
            upFromX = _vo.upFromX;
            upToX = _vo.upToX;
            upTimeY = _vo.upTimeY;
            upTimeX = _vo.upTimeX;

            downFromY = _vo.downFromY;
            downToY = _vo.downToY;
            downFromX = _vo.downFromX;
            downToX = _vo.downToX;
            downTimeY = _vo.downTimeY;
            downTimeX = _vo.downTimeX;

            UpdateSpot();
        }
        get { return _vo; }
    }

    private List<GameObject> upSpotlights = new List<GameObject>();
    private List<GameObject> downSpotlights = new List<GameObject>();

    public void UpdateSpot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        float up = upSpacing * (upSpotCount - 1);
        float down = downSpacing * (downSpotCount - 1);

        for (int i = 0; i < upSpotCount; i++)
        {
            GameObject o = Instantiate(spotlight, transform);
            SpotlightComponent yoyo = o.GetComponent<SpotlightComponent>();
            yoyo.fromX = upFromX;
            yoyo.fromY = upFromY;
            yoyo.toX = upToX;
            yoyo.toY = upToY;
            yoyo.timeX = upTimeX;
            yoyo.timeY = upTimeY;

            if (_vo.startRotate)
            {
                yoyo.StartRotate();
            }
            else
            {
                yoyo.StopRotate();
            }

            o.transform.localPosition = new Vector3(i * upSpacing - (up / 2), verticalSpacing / 2, 0);
        }

        for (int i = 0; i < downSpotCount; i++)
        {
            GameObject o = Instantiate(spotlight, transform);
            SpotlightComponent yoyo = o.GetComponent<SpotlightComponent>();
            yoyo.fromX = downFromX;
            yoyo.fromY = downFromY;
            yoyo.toX = downToX;
            yoyo.toY = downToY;
            yoyo.timeX = downTimeX;
            yoyo.timeY = downTimeY;

            if (_vo.startRotate)
            {
                yoyo.StartRotate();
            }
            else
            {
                yoyo.StopRotate();
            }

            o.transform.localPosition = new Vector3(i * downSpacing - (down / 2), -verticalSpacing / 2, 0);
        }
    }
}
