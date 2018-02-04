using BuildManager;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadThickIrregularCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FileControllor f = new FileControllor();

        OpenFileDlg pth = f.OpenProject(FileType.THICKIRREGULAR);
        if (pth == null) return;

        string url = pth.file;
        string fileName = pth.fileTitle.Replace(".zwkjtiobj", "");
        string urlFile = pth.file.Replace(pth.fileTitle, "");

        WWW www = new WWW("file:///" + url);
        if (www.error == null)
        {
            string str = System.Text.Encoding.Default.GetString(www.bytes);
            www.Dispose();

            if (str == "") return;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(DES_zaowu.Decder(str));
            DispatchEvent(new FileEvent(FileEvent.LOAD_THICKIRREGULAR_COMPLETE, url, fileName, xml));
        }
    }
}
