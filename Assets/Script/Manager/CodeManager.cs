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
    public static string GetBuildCode()
    {
        string code = "<Code>";

        code += "<Build>";
        foreach (LineStruct lineData in BuilderModel.Instance.lineDatas)
        {
            code += lineData.vo.Code.OuterXml;
        }
        foreach (SurfaceStruct surfaceData in BuilderModel.Instance.surfaceDatas)
        {
            code += surfaceData.vo.Code.OuterXml;
        }
        foreach (NestedStruct nestedData in BuilderModel.Instance.nestedDatas)
        {
            code += nestedData.vo.Code.OuterXml;
        }
        code += "</Build>";

        code += "</Code>";

        return code;
    }

    public static string GetAssetsCode()
    {
        string code = "<Code>";

        code += SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>().VO.Code.OuterXml;

        code += "<Assets>";
        foreach (ItemStruct objectData in AssetsModel.Instance.itemDatas)
        {
            if (objectData.vo is ItemVO)
            {
                code += objectData.vo.Code.OuterXml;
            }
        }
        code += "</Assets>";

        code += "</Code>";

        return code;
    }

    public static void SaveCode()
    {
        SaveCode(SceneManager.ProjectURL + "/" + SceneManager.ProjectName + "_Build", GetBuildCode());
        SaveCode(SceneManager.ProjectURL + "/" + SceneManager.ProjectName + "_Assets", GetAssetsCode());
    }

    public static void SaveCode(string url, string code)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(code);
        xml.Save(url);
    }

    public static void LoadBuildCode(string url, bool resources = false)
    {
        if (url == "") return;

        BuilderModel.Instance.Clear();

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

        XmlNodeList lineList = buildNode.SelectNodes("Line");
        XmlNodeList surfaceList = buildNode.SelectNodes("Surface");
        BuilderModel.Instance.CreateLineByCode(lineList);
        BuilderModel.Instance.CreateSurfaceByCode(surfaceList);

        XmlNodeList nestedList = buildNode.SelectNodes("Nested");
        BuilderModel.Instance.CreateNested(nestedList);

        foreach (NestedStruct data in BuilderModel.Instance.nestedDatas)
        {
            SceneManager.Instance.controlManager.UpdateNestedOnLine(data.nested2 as Nested2D);
        }
    }

    public static void LoadAssetsCode(string url)
    {
        if (url == "") return;

        AssetsModel.Instance.Clear();

        XmlDocument xml = new XmlDocument();

        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;
        xml.Load(XmlReader.Create(url, set));

        XmlNode xmlNode = xml.SelectSingleNode("Code");
        XmlNode assetsNode = xmlNode.SelectSingleNode("Assets");

        XmlNode editorCameraNode = xmlNode.SelectSingleNode("Camera");
        if (editorCameraNode != null)
        {
            SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>().VO.Code = editorCameraNode;
            SceneManager.Instance.EditorCamera.GetComponent<EditorCameraComponent>().VO = SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>().VO;
        }

        XmlNodeList itemList = assetsNode.SelectNodes("Item");
        AssetsModel.Instance.CreateItem(itemList);

        UIManager.OpenUI(UI.ProgressPanel);
    }

    public static List<AssetVO> LoadCombinationCode(string url)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load(url).ToString());

        XmlNode xmlNode = xml.SelectSingleNode("Combination");
        XmlNodeList itemList = xmlNode.SelectNodes("Item");
        return AssetsModel.Instance.CreateItemVO(itemList, NumberUtils.GetGuid());
    }

    public static List<AssetVO> LoadCombinationCode(XmlNode xml)
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
