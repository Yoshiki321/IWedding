using UnityEngine;
using System.Collections;
using System.IO;
using Common;
using System.Security.AccessControl;
using Build3D;
using BuildManager;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

public class SaveCombinationCommand : Command
{
    public override void Execute(EventObject e)
    {
        string code = CodeManager.GetCombinationCode(Mouse3Manager.selectionItem);
        string name = NumberUtils.GetGuid();
        string urlName = SceneManager.ProjectCombinationURL + "\\" + name;
        byte[] bytes = System.Text.Encoding.Default.GetBytes(code);
        File.WriteAllBytes(urlName, bytes);

        FileControllor f = new FileControllor();
        SaveFileDlg pth = f.SaveProject(FileType.COMBINATION);
        if (pth == null) return;
        string url = pth.file;

        List<string> list = new List<string>();
        list.Add(SceneManager.ProjectCombinationURL + "\\" + name);

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            FrameVO framevo = item.VO.GetComponentVO<FrameVO>();
            if (framevo != null)
            {
                list.Add(SceneManager.ProjectPictureURL + "/" + framevo.url);
            }

            CollageVO collagevo = item.VO.GetComponentVO<CollageVO>();
            if (collagevo != null)
            {
                foreach (CollageStruct cs in collagevo.collages)
                {
                    if (cs.url != "")
                    {
                        list.Add(SceneManager.ProjectPictureURL + "/" + cs.url);
                    }
                }
            }

            ThickIrregularVO thickIrregularvo = item.VO.GetComponentVO<ThickIrregularVO>();
            if (thickIrregularvo != null)
            {
                list.Add(SceneManager.ProjectModelURL + "/" + thickIrregularvo.url);
            }
        }

        ZipUtility.Zip(list, url);

        DispatchEvent(new FileEvent(FileEvent.SAVE_COMBINATION_COMPLETE));
    }
}
