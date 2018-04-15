using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using Build3D;
using System;
using System.IO;
using System.Data;
using Excel;
using UnityEngine.UI;

public class ItemManager
{
    public ItemManager()
    {
        //加载ItemXml配置文件
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/Item/Item").ToString());
        //解析到XML-第1标签-ItemCode
        XmlNode xmlNode = xml.SelectSingleNode("ItemCode");
        //实例化列表用来置放子物体
        ItemDataList = new List<ItemData>();

        foreach (XmlNode node in xmlNode)
        {
            ItemData data = new ItemData();
            data.id = node.Attributes["id"].Value;
            data.classify = node.Attributes["classify"].Value;
            data.type = node.Attributes["type"].Value;
            data.thumbnail = node.Attributes["thumbnail"].Value;
            data.topImg = node.Attributes["topimg"].Value;
            data.model = node.Attributes["model"].Value;

            XmlNodeList nodeList = node.ChildNodes;
            foreach (XmlNode xmlnode in nodeList)
            {
                switch (xmlnode.Name)
                {
                    case "Material":
                        ItemMaterialData itemMaterialData = new ItemMaterialData();
                        itemMaterialData.id = xmlnode.Attributes["id"].Value;
                        itemMaterialData.order = xmlnode.Attributes["order"].Value;
                        itemMaterialData.replace = (xmlnode.Attributes["replace"].Value as string).Split(',');
                        data.itemMaterials.Add(itemMaterialData);
                        break;
                    case "PointLight":
                        PointLightVO vo = new PointLightVO();
                        vo.Code = xmlnode;
                        data.componentVOs.Add(vo);
                        break;
                }
            }

            ItemDataList.Add(data);
        }

        ItemDataList = new List<ItemData>();

        FileStream stream = File.Open(Application.dataPath + "\\Data\\" + "test.xlsx",
            FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet resultds = excelReader.AsDataSet();
        excelReader.Close();

        int column = resultds.Tables[0].Columns.Count;
        int row = resultds.Tables[0].Rows.Count;
        Debug.LogWarning(column + "  " + row);
        for (int i = 1; i < row; i++)
        {
            ItemData data = new ItemData();
            data.id = resultds.Tables[0].Rows[i][0].ToString();
            data.thumbnail = resultds.Tables[0].Rows[i][1].ToString();
            ItemDataList.Add(data);
            GameObject.Find("Canvas").transform.Find("InputField").GetComponent<InputField>().text += data.thumbnail;
        }

    }

    public static List<ItemData> ItemDataList { get; private set; }

    public static ItemData GetItemData(string id)
    {
        foreach (ItemData data in ItemDataList)
        {
            if (data.id == id)
            {
                return data;
            }
        }
        return null;
    }

    public static Vector3 GetItemsCenter(List<Item3D> list)
    {
        Vector3 cpoint = Vector3.zero;
        foreach (Item3D item in list)
        {
            cpoint += item.transform.position;
        }
        cpoint = cpoint / list.Count;

        return cpoint;
    }
}
