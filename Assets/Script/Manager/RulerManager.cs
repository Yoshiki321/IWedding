using UnityEngine;
using System.Collections;
using BuildManager;
using System;
using Build3D;
using RTEditor;

public class RulerManager : MonoBehaviour
{
    private TextMesh lineForwardText;
    private TextMesh lineBackText;
    private TextMesh lineLeftText;
    private TextMesh lineRightText;

    private void Start()
    {
        lineForwardText = CreateLineText(new GameObject());
        lineBackText = CreateLineText(new GameObject());
        lineLeftText = CreateLineText(new GameObject());
        lineRightText = CreateLineText(new GameObject());
    }

    private bool _lineActive;

    public void SetActive(bool value)
    {
        _lineActive = value;
        lineForwardText.gameObject.SetActive(value);
        lineBackText.gameObject.SetActive(value);
        lineLeftText.gameObject.SetActive(value);
        lineRightText.gameObject.SetActive(value);
    }

    TextMesh CreateLineText(GameObject line)
    {
        line.transform.parent = transform;
        line.name = "LineText";
        line.layer = gameObject.layer;
        line.transform.localScale = new Vector3(-.02f, .02f, .02f);
        TextMesh lineText = line.AddComponent<TextMesh>();
        lineText.fontSize = 60;
        lineText.anchor = TextAnchor.UpperCenter;
        return lineText;
    }

    void Update()
    {
        return;

        if (_item == null)
        {
            SetActive(false);
            return;
        }
        else
        {
            SetActive(true);
        }

        RaycastHit hitInfo;
        LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");

        Vector3 v = _item.transform.position + (Vector3.forward * _item.sizeX / 2);
        Vector3 v0 = new Vector3(v.x, v.y + _item.sizeY / 2, v.z);

        if (Physics.Raycast(v0, Vector3.forward, out hitInfo, 99999, layer))
        {
            Vector3 v1 = Vector3.Lerp(v, hitInfo.point, .5f);
            v1.y += _item.sizeY / 7;
            lineForwardText.transform.position = new Vector3(v1.x, v1.y, v1.z);
            lineForwardText.transform.LookAt(SceneManager.Instance.EditorCamera.transform);
            lineForwardText.text = Math.Round(Vector3.Distance(hitInfo.point, _item.transform.position), 2).ToString();

            forwardStartPoint = v0;
            forwardEndPoint = hitInfo.point;
        }

        v = _item.transform.position + (Vector3.back * _item.sizeX / 2);
        v0 = new Vector3(v.x, v.y + _item.sizeY / 2, v.z);

        if (Physics.Raycast(v0, Vector3.back, out hitInfo, 99999, layer))
        {
            Vector3 v1 = Vector3.Lerp(v, hitInfo.point, .5f);
            v1.y += _item.sizeY / 7;
            lineBackText.transform.position = new Vector3(v1.x, v1.y, v1.z);
            lineBackText.transform.LookAt(SceneManager.Instance.EditorCamera.transform);
            lineBackText.text = Math.Round(Vector3.Distance(hitInfo.point, _item.transform.position), 2).ToString();

            backStartPoint = v0;
            backEndPoint = hitInfo.point;
        }

        v = _item.transform.position + (Vector3.left * _item.sizeX / 2);
        v0 = new Vector3(v.x, v.y + _item.sizeY / 2, v.z);

        if (Physics.Raycast(v0, Vector3.left, out hitInfo, 99999, layer))
        {

            Vector3 v1 = Vector3.Lerp(v, hitInfo.point, .5f);
            v1.y += _item.sizeY / 7;
            lineLeftText.transform.position = new Vector3(v1.x, v1.y, v1.z);
            lineLeftText.transform.LookAt(SceneManager.Instance.EditorCamera.transform);
            lineLeftText.text = Math.Round(Vector3.Distance(hitInfo.point, _item.transform.position), 2).ToString();

            leftStartPoint = v0;
            leftEndPoint = hitInfo.point;
        }

        v = _item.transform.position + (Vector3.right * _item.sizeX / 2);
        v0 = new Vector3(v.x, v.y + _item.sizeY / 2, v.z);

        if (Physics.Raycast(v0, Vector3.right, out hitInfo, 99999, layer))
        {
            Vector3 v1 = Vector3.Lerp(v, hitInfo.point, .5f);
            v1.y += _item.sizeY / 7;
            lineRightText.transform.position = new Vector3(v1.x, v1.y, v1.z);
            lineRightText.transform.LookAt(SceneManager.Instance.EditorCamera.transform);
            lineRightText.text = Math.Round(Vector3.Distance(hitInfo.point, _item.transform.position), 2).ToString();

            rightStartPoint = v0;
            rightEndPoint = hitInfo.point;
        }
    }

    private Vector3 forwardStartPoint;
    private Vector3 forwardEndPoint;
    private Vector3 backStartPoint;
    private Vector3 backEndPoint;
    private Vector3 leftStartPoint;
    private Vector3 leftEndPoint;
    private Vector3 rightStartPoint;
    private Vector3 rightEndPoint;

    void OnPostRender()
    {
        if (!_lineActive) return;

        GLPrimitives.Draw3DLine(rightStartPoint, rightEndPoint, Color.green, MaterialPool.Instance.GizmoLine);
        GLPrimitives.Draw3DLine(leftStartPoint, leftEndPoint, Color.green, MaterialPool.Instance.GizmoLine);
        GLPrimitives.Draw3DLine(forwardStartPoint, forwardEndPoint, Color.green, MaterialPool.Instance.GizmoLine);
        GLPrimitives.Draw3DLine(backStartPoint, backEndPoint, Color.green, MaterialPool.Instance.GizmoLine);
    }

    private Item3D _item;

    public Item3D item
    {
        set { _item = value; }
        get { return _item; }
    }
}
