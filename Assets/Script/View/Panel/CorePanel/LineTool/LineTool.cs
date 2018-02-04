using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;
using UnityEngine.UI;
using BuildManager;

public class LineTool : BasePanel
{
    private GameObject fromBtn;
    private GameObject toBtn;
    private GameObject lineBtn;
    private InputField lengthText;

    private void Awake()
    {
        fromBtn = GetUI("from");
        toBtn = GetUI("to");
        lineBtn = GetUI("line");
        lengthText = GetUI("lengthText").GetComponent<InputField>();

        AddEventDown(fromBtn);
        AddEventDown(toBtn);
        AddEventDown(lineBtn);
        AddEventDown(lengthText.gameObject);

        lengthText.onEndEdit.AddListener(OnEndEditLength);
    }

    private void OnEndEditLength(string text)
    {
        transform.parent.GetComponent<BasePanel>().dispatchEvent(new LineToolEvent(LineToolEvent.LineTool_EndEdit_lengthText, float.Parse(text)));
        _editLengthText = false;
    }

    private Line2D _line;

    public Line2D line
    {
        set
        {
            _line = value;

            UpdateLine();
        }
        get { return _line; }
    }

    protected override void OnDown(GameObject obj)
    {
        if (obj.name == "from")
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new LineToolEvent(LineToolEvent.LineTool_From));
        }
        if (obj.name == "to")
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new LineToolEvent(LineToolEvent.LineTool_To));
        }
        if (obj.name == "line")
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new LineToolEvent(LineToolEvent.LineTool_Line));
        }
        if (obj.name == "lengthText")
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new LineToolEvent(LineToolEvent.LineTool_lengthText));
            _editLengthText = true;
        }
    }

    private bool _editLengthText;

    public void UpdateLine()
    {
        if (_line)
        {
            fromBtn.transform.localPosition = SceneManager.Instance.Camera2D.WorldToScreenPoint(_line.from);
            toBtn.transform.localPosition = SceneManager.Instance.Camera2D.WorldToScreenPoint(_line.to);

            Vector2 v = Vector2.Lerp(new Vector2(fromBtn.transform.localPosition.x, fromBtn.transform.localPosition.y), 
                new Vector2(toBtn.transform.localPosition.x, toBtn.transform.localPosition.y), .5f);
            lineBtn.transform.localPosition = new Vector3(v.x, v.y, 0);

            Vector2 p2 = new Vector2();
            if (_line.interiorAlong)
            {
                Vector2 tpi = Vector2.Lerp(_line.inversePoints[0], _line.inversePoints[1], .5f);
                Vector2 tpi0 = Vector2.Lerp(_line.inversePointsO[0], _line.inversePointsO[1], .5f);
                Vector2 tpi1 = PlaneUtils.AngleDistanceGetPoint(tpi, _line.angle + 90, .9f);
                Vector2 tpi2 = PlaneUtils.AngleDistanceGetPoint(tpi, _line.angle - 90, .9f);
                float di1 = Vector2.Distance(tpi1, tpi0);
                float di2 = Vector2.Distance(tpi2, tpi0);
                if (di1 < di2) p2 = tpi1; else p2 = tpi2;
            }
            else
            {
                Vector2 tpa = Vector2.Lerp(_line.alongPoints[0], _line.alongPoints[1], .5f);
                Vector2 tpa0 = Vector2.Lerp(_line.alongPointsO[0], _line.alongPointsO[1], .5f);
                Vector2 tpa1 = PlaneUtils.AngleDistanceGetPoint(tpa, _line.angle + 90, .9f);
                Vector2 tpa2 = PlaneUtils.AngleDistanceGetPoint(tpa, _line.angle - 90, .9f);
                float da1 = Vector2.Distance(tpa1, tpa0);
                float da2 = Vector2.Distance(tpa2, tpa0);
                if (da1 < da2) p2 = tpa1; else p2 = tpa2;
            }

            Vector3 vl3 = SceneManager.Instance.Camera2D.WorldToScreenPoint(p2);
            lengthText.transform.localPosition = new Vector3(vl3.x, vl3.y, 0);
            lengthText.transform.localRotation = Quaternion.Euler(0, 0, _line.angle);

            if (!_editLengthText) lengthText.text = _line.interiorLength.ToString() + "m";
        }
    }

    void Update()
    {

    }
}
