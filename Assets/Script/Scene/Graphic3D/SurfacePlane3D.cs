using UnityEngine;
using System.Collections;
using BuildManager;
using System.IO;

public class SurfacePlane3D : IrregularPlane3D
{
    //private HighlighterController highlighterController;

    //private void Start()
    //{
    //    highlighterController = gameObject.AddComponent<HighlighterController>();
    //}

    public void SetCollage(CollageStruct collageStruct)
    {
        Material m;

        if (collageStruct.url == "")
        {
            m = TexturesManager.CreateMaterials(collageStruct.id as string);
            m.color = collageStruct.color;
        }
        else
        {
            m = new Material(Shader.Find("Standard"));
            string url = collageStruct.url as string;
            m.color = collageStruct.color;

            Texture2D tex;
            if (url.Contains(":\\"))
            {
                WWW www = new WWW("file:///" + url);
                tex = www.texture;
                string name = NumberUtils.GetGuid() + ".jpg";
                string urlName = SceneManager.ProjectPictureURL + "\\" + name;
                byte[] bytes = tex.EncodeToPNG();
                File.WriteAllBytes(urlName, bytes);
                collageStruct.url = name;
            }
            else
            {
                WWW www = new WWW("file:///" + SceneManager.ProjectPictureURL + "\\" + url);
                tex = www.texture;
            }
            m.SetTexture("_MainTex", tex);
        }

        SetMaterial(m);
    }

    public void SetCollage(string id)
    {
        SetMaterial(TexturesManager.CreateMaterials(id));
    }

    //private bool _selected;

    //public virtual bool Selected
    //{
    //    set
    //    {
    //        if (_selected == value) return;
    //        _selected = value;
    //        UpdateHighlighter();
    //    }
    //    get { return _selected; }
    //}

    //protected void UpdateHighlighter()
    //{
    //    if (Selected)
    //    {
    //        highlighterController.highlighter.ConstantSwitch();
    //    }
    //    else
    //    {
    //        highlighterController.highlighter.ConstantSwitchImmediate();
    //    }
    //}
}
