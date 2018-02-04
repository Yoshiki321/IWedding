using BuildManager;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CodeParser
{
    #region Version 0.0.1

    public static string Code001()
    {
        string code = "<Code>";

        code += SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>().VO.Code.OuterXml;

        code += "<Build>";
        foreach (LineData lineData in BuilderModel.Instance.lineDatas)
        {
            code += lineData.vo.Code.OuterXml;
        }
        foreach (SurfaceData surfaceData in BuilderModel.Instance.surfaceDatas)
        {
            code += surfaceData.vo.Code.OuterXml;
        }
        code += "</Build>";

        code += "<Assets>";
        foreach (ObjectData objectData in AssetsModel.Instance.objectDatas)
        {
            code += objectData.vo.Code.OuterXml;
        }
        code += "</Assets>";

        code += "</Code>";

        return code;
    }

    public static void LoadCode001(string url, bool resources = false)
    {
        if (url == "") return;

        BuilderModel.Instance.Clear();
        AssetsModel.Instance.Clear();

        XmlDocument xml = new XmlDocument();

        if (resources)
        {
            xml.LoadXml(Resources.Load(url).ToString());
        }
        else
        {
            XmlReaderSettings set = new XmlReaderSettings();
            set.IgnoreComments = true;
            xml.Load(XmlReader.Create(url, set));
        }

        XmlNode xmlNode = xml.SelectSingleNode("Code");
        XmlNode buildNode = xmlNode.SelectSingleNode("Build");
        XmlNode assetsNode = xmlNode.SelectSingleNode("Assets");

        XmlNodeList lineList = buildNode.SelectNodes("Line");
        XmlNodeList surfaceList = buildNode.SelectNodes("Surface");
        BuilderModel.Instance.CreateLineByCode(lineList);
        BuilderModel.Instance.CreateSurfaceByCode(surfaceList);

        XmlNode editorCameraNode = xmlNode.SelectSingleNode("Camera");
        if (editorCameraNode != null)
        {
            SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>().VO.Code = editorCameraNode;
            SceneManager.Instance.EditorCamera.GetComponent<EditorCameraComponent>().VO = SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>().VO;
        }

        XmlNodeList itemList = assetsNode.SelectNodes("Item");
        AssetsModel.Instance.CreateItem(itemList);

        XmlNodeList nestedList = assetsNode.SelectNodes("Nested");
        AssetsModel.Instance.CreateNested(nestedList);
    }

    #endregion
}
