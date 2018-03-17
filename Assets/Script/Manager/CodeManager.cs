using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using BuildManager;
using System;
using Build3D;
using Build2D;

public class CodeManager
{
    public static string GetCode()
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

    public static void SaveCode()
    {
        SaveCode(SceneManager.ProjectURL + "/" + SceneManager.ProjectName, GetCode());
    }

    public static void SaveCode(string url, string code)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(code);
        xml.Save(url);
    }

    public static void LoadCode(string url, bool resources = false)
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

        foreach (ObjectData data in AssetsModel.Instance.nestedDatas)
        {
            SceneManager.Instance.controlManager.UpdateNestedOnLine(data.object2 as Nested2D);
        }

        UIManager.OpenUI(UI.ProgressPanel);
    }

    public static List<AssetVO> LoadCombination(string url)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load(url).ToString());

        XmlNode xmlNode = xml.SelectSingleNode("Combination");
        XmlNodeList itemList = xmlNode.SelectNodes("Item");
        return AssetsModel.Instance.CreateItemVO(itemList, NumberUtils.GetGuid());
    }

    public static List<AssetVO> LoadCombination(XmlNode xml)
    {
        XmlNode xmlNode = xml.SelectSingleNode("Combination");
        XmlNodeList itemList = xmlNode.SelectNodes("Item");
        return AssetsModel.Instance.CreateItemVO(itemList, NumberUtils.GetGuid());
    }

    public static string GetCombinationCode(List<Item3D> list)
    {
        Vector3 cpoint = ItemManager.GetItemsCenter(list);

        string code = "<Combination>";
        foreach (Item3D item in list)
        {
            ItemVO vo = item.VO.Clone() as ItemVO;
            TransformVO tvo = vo.GetComponentVO<TransformVO>();
            tvo.x -= cpoint.x;
            tvo.y -= cpoint.y;
            tvo.z -= cpoint.z;
            code += vo.Code.OuterXml;
        }
        code += "</Combination>";

        return code;
    }

    public static XmlDocument GetGroupCatalogXml()
    {
        XmlDocument xml = new XmlDocument();
        string code = "<GroupCatalog>";
        code += "</GroupCatalog>";
        xml.LoadXml(code);
        return xml;
    }
}
