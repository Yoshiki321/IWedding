using UnityEngine;
using System.Collections;
using Build3D;
using System.IO;
using BuildManager;
using System.Collections.Generic;

public class FrameComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<FrameVO>() == null)
        {
            _item.VO.AddComponentVO<FrameVO>();
        }
    }

    public List<GameObject> pictures;

    private string _url = "";

    private FrameVO _vo;

    private Texture2D texture2;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<FrameVO>();

            url = _vo.url;
            _vo.texture2 = texture2;
        }
        get { return _vo; }
    }

    public string url
    {
        set
        {
            _url = value;

            if (_url != null && _url != "")
            {
                Texture2D tex;
                if (_url.Contains(":\\"))
                {
                    WWW www = new WWW("file:///" + _url);
                    tex = www.texture;
                    string name = NumberUtils.GetGuid() + ".jpg";
                    string urlName = SceneManager.ProjectPictureURL + "\\" + name;
                    byte[] bytes = tex.EncodeToPNG();
                    File.WriteAllBytes(urlName, bytes);
                    _vo.url = name;
                }
                else
                {
                    WWW www = new WWW("file:///" + SceneManager.ProjectPictureURL + "\\" + _url);
                    tex = www.texture;
                }

                texture2 = tex;
                _vo.texture2 = tex;

                foreach (GameObject p in pictures)
                {
                    p.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
                }

                //LoadTexture("file:///" + _url, delegate (Texture2D tex)
                //{
                //    string name = NumberUtils.GetGuid() + ".jpg";
                //    string urlName = SceneManager.ProjectPictureURL + "\\" + name;
                //    byte[] bytes = tex.EncodeToPNG();
                //    File.WriteAllBytes(urlName, bytes);

                //    _vo.url = name;

                //    texture2 = tex;
                //    _vo.texture2 = tex;
                //    picture.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
                //});
            }
        }
    }
}
