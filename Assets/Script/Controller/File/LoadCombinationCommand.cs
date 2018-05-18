using BuildManager;
using Common;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LoadCombinationCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FileControllor f = new FileControllor();

        OpenFileDlg pth = f.OpenProject(FileType.COMBINATION);
        if (pth == null) return;

        string url = pth.file;
        string fileName = pth.fileTitle.Replace(".zwkjCombination", "");
        string urlFile = pth.file.Replace(pth.fileTitle, "");

        _listString = new List<string>();
        ZipUtility.UnzipFile(url, SceneManager.ProjectResourcesURL, null, OnPostUnzip, OnFinished);
    }

    private List<string> _listString;

    private void OnPostUnzip(ZipEntry value)
    {
        _listString.Add(value.Name);
    }

    private void OnFinished(bool value)
    {
        string url = "";

        if (value)
        {
            foreach (string s in _listString)
            {
                if (s.Contains(".jpg") || s.Contains(".png"))
                {
                    File.Move(SceneManager.ProjectResourcesURL + "/" + s, SceneManager.ProjectPictureURL + "/" + s);
                }
                else if (s.Contains(".zwkjtiobj"))
                {
                    File.Move(SceneManager.ProjectResourcesURL + "/" + s, SceneManager.ProjectModelURL + "/" + s);
                }
                else
                {
                    File.Move(SceneManager.ProjectResourcesURL + "/" + s, SceneManager.ProjectCombinationURL + "/" + s);
                    url = SceneManager.ProjectCombinationURL + "/" + s;
                }
            }

            WWW www = new WWW("file:///" + url);
            if (www.error == null)
            {
                string str = System.Text.Encoding.Default.GetString(www.bytes);
                www.Dispose();

                if (str == "") return;

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(str);
                DispatchEvent(new FileEvent(FileEvent.LOAD_COMBINATION_COMPLETE, url, null, xml));
            }
        }
    }
}
