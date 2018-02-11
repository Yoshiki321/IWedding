using UnityEngine;
using System.Collections;
using Build3D;
using System.IO;
using BuildManager;
using System.Xml;

public class ThickIrregularComponent : SceneComponent
{
    ItemVO _itemVO;
    Item3D item;

    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<ThickIrregularVO>() == null)
        {
            _item.VO.AddComponentVO<ThickIrregularVO>();
        }

        item = _item as Item3D;
        _itemVO = _item.VO as ItemVO;
    }

    private string _url = "";

    private ThickIrregularVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<ThickIrregularVO>();

            url = _vo.url;
        }
        get { return _vo; }
    }

    public string url
    {
        set
        {
            if (_url == value) return;
            _url = value;

            if (_url != null && _url != "")
            {
                WWW www = new WWW("file:///" + SceneManager.ProjectModelURL + "\\" + _url);
                if (string.IsNullOrEmpty(www.error))
                {
                    string str = System.Text.Encoding.Default.GetString(www.bytes);
                    www.Dispose();

                    if (str == "") return;

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(DES_zaowu.Decder(str));

                    ThickIrregularPlane3D t3 = item.model.GetComponent<ThickIrregularPlane3D>();
                    _vo.xml = xml;

                    if (t3 == null)
                    {
                        GameObject drawPanel = new GameObject("DrawPanel");
                        drawPanel.AddComponent<ThickIrregularPlane3D>().Code = xml;

                        _itemVO.model = drawPanel;
                        _itemVO.topImgTexture = (Texture2D)Resources.Load("TopImg/Stage");

                        item.model = _itemVO.model;
                    }
                    else
                    {
                        t3.Code = xml;

                        CurvyColumn curvyColumn = _itemVO.model.AddComponent<CurvyColumn>();
                        curvyColumn.points(t3.upPoints,
                            t3.upObj.transform);
                    }
                }
            }
        }
    }
}
