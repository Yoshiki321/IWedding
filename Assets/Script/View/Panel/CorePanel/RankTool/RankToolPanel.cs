using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankToolPanel : BasePanel
{
    public GameObject positionX;
    public GameObject positionY;
    public GameObject positionZ;
    public GameObject scaleX;
    public GameObject scaleY;
    public GameObject scaleZ;
    public GameObject rotationX;
    public GameObject rotationY;
    public GameObject rotationZ;
    public GameObject rayDown;
    public GameObject randomRotate;

    void Awake()
    {
        positionX = GetUI("PositionX");
        positionY = GetUI("PositionY");
        positionZ = GetUI("PositionZ");
        scaleX = GetUI("ScaleX");
        scaleY = GetUI("ScaleY");
        scaleZ = GetUI("ScaleZ");
        rotationX = GetUI("RotationX");
        rotationY = GetUI("RotationY");
        rotationZ = GetUI("RotationZ");
        rayDown = GetUI("RayDown");
        randomRotate = GetUI("RandomRotate");

        AddEventClick(positionX);
        AddEventClick(positionY);
        AddEventClick(positionZ);
        AddEventClick(scaleX);
        AddEventClick(scaleY);
        AddEventClick(scaleZ);
        AddEventClick(rotationX);
        AddEventClick(rotationY);
        AddEventClick(rotationZ);
        AddEventClick(rayDown);
        AddEventClick(randomRotate);
    }

    public override void Open()
    {
        base.Open();
    }

    protected override void OnClick(GameObject obj)
    {
        transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.CLOSE));
        if (obj == positionX) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.POSITION_X));
        if (obj == positionY) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.POSITION_Y));
        if (obj == positionZ) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.POSITION_Z));
        if (obj == scaleX) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.SCALE_X));
        if (obj == scaleY) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.SCALE_Y));
        if (obj == scaleZ) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.SCALE_Z));
        if (obj == rotationX) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.ROTATION_X));
        if (obj == rotationY) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.ROTATION_Y));
        if (obj == rotationZ) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.ROTATION_Z));
        if (obj == rayDown) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.RAYDOWN));
        if (obj == randomRotate) transform.parent.GetComponent<BasePanel>().dispatchEvent(new RankToolPanelEvent(RankToolPanelEvent.RANDOMROTATE));
    }
}
