using UnityEngine;
using System.Collections;
using System.IO;
using Common;
using System.Security.AccessControl;
using Build3D;
using BuildManager;

public class SaveCodeCommand : Command
{
    public override void Execute(EventObject e)
    {
        //FileEvent fileEvent = e as FileEvent;

        //FileControllor f = new FileControllor();

        //SaveFileDlg pth = f.SaveProject(FileType.CODE);
        //if (pth == null) return;

        //string url = pth.file;
        //string urlFile = pth.file.Replace(pth.fileTitle, "");
        //string fileName = pth.fileTitle.Replace(".xml", "");
        //string urlFileResources = urlFile + fileName + "Resources";

        //Directory.CreateDirectory(urlFileResources);

        //foreach (ObjectData data in AssetsModel.Instance.itemDatas)
        //{
        //    FrameVO fvo = data.object3.VO.GetComponentVO<FrameVO>();
        //    if (fvo != null)
        //    {
        //        string name = NumberUtils.GetGuid() + ".jpg";
        //        string urlName = urlFileResources + "\\" + name;
        //        byte[] bytes = fvo.texture2.EncodeToPNG();
        //        File.WriteAllBytes(urlName, bytes);
        //        fvo.url = name;
        //    }

        //    ThickIrregularPlane3D thickIrregularPlane3D = data.object3.GetComponentInChildren<ThickIrregularPlane3D>();
        //    if (thickIrregularPlane3D != null)
        //    {
        //        string name = (data.object3 as Item3D).assetId + ".zwkjObj";
        //        string urlName = urlFileResources + "\\" + name;
        //        byte[] bytes = System.Text.Encoding.Default.GetBytes(thickIrregularPlane3D.Code);
        //        File.WriteAllBytes(urlName, bytes);
        //    }
        //}

        //CodeManager.SaveCode(url, CodeManager.GetCode());

        CodeManager.SaveCode();
        SceneManager.UpdateQuoteFileURL();
    }
}
