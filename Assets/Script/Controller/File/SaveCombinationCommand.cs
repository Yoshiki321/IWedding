using UnityEngine;
using System.Collections;
using System.IO;
using Common;
using System.Security.AccessControl;
using Build3D;
using BuildManager;
using System.Xml;

public class SaveCombinationCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        string code = DES_zaowu.Encoder(fileEvent.obj as string);
        byte[] bytes = System.Text.Encoding.Default.GetBytes(code);
        string name = fileEvent.name + ".zwkjCombination";

        if (fileEvent.url != "")
        {
            File.WriteAllBytes(fileEvent.url, bytes);
        }
        else
        {
            FileControllor f = new FileControllor();
            SaveFileDlg pth = f.SaveProject(FileType.COMBINATION);
            if (pth == null) return;
            string url = pth.file;
            File.WriteAllBytes(url, bytes);
        }
    }
}
