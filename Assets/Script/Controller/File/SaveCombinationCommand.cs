using UnityEngine;
using System.Collections;
using System.IO;
using Common;
using System.Security.AccessControl;
using Build3D;
using BuildManager;
using System.Xml;
using System.Threading;

public class SaveCombinationCommand : Command
{
    public override void Execute(EventObject e)
    {
        string code = CodeManager.GetCombinationCode(Mouse3Manager.selectionItem);
        //XmlDocument xml = new XmlDocument();
        //xml.LoadXml(code);
        //xml.Save(SceneManager.ProjectCombinationURL + "\\" + "Combination");

        string name = "Combination";
        string urlName = SceneManager.ProjectCombinationURL + "\\" + name;
        byte[] bytes = System.Text.Encoding.Default.GetBytes(code);
        File.WriteAllBytes(urlName, bytes);

        //FileStream aFile = new FileStream(urlName, FileMode.OpenOrCreate);
        //StreamWriter sw = new StreamWriter(aFile);
        ////FileInfo t = new FileInfo(urlName);
        ////sw = t.AppendText();
        //sw.WriteLine(code);
        //sw.Close();
        //sw.Dispose();

        //FileControllor f = new FileControllor();
        //SaveFileDlg pth = f.SaveProject(FileType.COMBINATION);
        //if (pth == null) return;
        //string url = pth.file;

        //DispatchEvent(new FileEvent(FileEvent.SAVE_COMBINATION_COMPLETE));

        Thread.Sleep(3000);

        ZipUtility.Zip(new string[1] { SceneManager.ProjectCombinationURL + "\\" + "Combination" }, SceneManager.ProjectURL + "/" + "Resources" + "/" + "aaa.xxx");
    }
}
