using BuildManager;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadCodeCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FileControllor f = new FileControllor();

        OpenFileDlg pth = f.OpenProject(FileType.CODE);
        if (pth == null) return;

        string url = pth.file;
        string fileName = pth.fileTitle.Replace(".xml", "");
        string urlFile = pth.file.Replace(pth.fileTitle, "");

        SceneManager.ProjectName = fileName;
        SceneManager.ProjectURL = urlFile;

        CodeManager.LoadCode(url);
    }
}
