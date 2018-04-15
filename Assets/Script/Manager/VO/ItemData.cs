using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string id;
    //名称
    public string name;
    //描述
    public string describe;
    //缩略图路径
    public string thumbnail = "";
    //顶视图路径
    public string topImg = "";
    //模型路径
    public string model = "";
    //道具类型
    public string classify = "";
    //放置类型
    public string type = "";
    //风格
    public string style = "";
    //价格
    public string price = "";

    public List<ComponentVO> componentVOs = new List<ComponentVO>();
    public List<ItemMaterialData> itemMaterials = new List<ItemMaterialData>();
}
