using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string id;
    public string thumbnail = "";
    public string classify = "";
    public string type = "";
    public string topImg = "";
    public string model = "";

    public List<ComponentVO> componentVOs = new List<ComponentVO>();
    public List<ItemMaterialData> itemMaterials = new List<ItemMaterialData>();
}
