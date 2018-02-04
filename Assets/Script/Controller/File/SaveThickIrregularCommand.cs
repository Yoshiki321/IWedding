using UnityEngine;
using System.Collections;
using System.IO;
using Common;
using System.Security.AccessControl;
using Build3D;
using BuildManager;
using System.Xml;

public class SaveThickIrregularCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        XmlNode xml = fileEvent.obj as XmlNode;
        string code = DES_zaowu.Encoder(xml.OuterXml);
        byte[] bytes = System.Text.Encoding.Default.GetBytes(code);
        string name = fileEvent.name + ".zwkjtiobj";

        if (fileEvent.url != "")
        {
            File.WriteAllBytes(fileEvent.url, bytes);
        }
        else
        {
            FileControllor f = new FileControllor();
            SaveFileDlg pth = f.SaveProject(FileType.THICKIRREGULAR);
            if (pth == null) return;
            string url = pth.file;
            File.WriteAllBytes(url, bytes);
        }
    }
}
