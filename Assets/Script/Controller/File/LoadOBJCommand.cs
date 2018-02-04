using Common;
using UnityEngine;

public class LoadOBJCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FileControllor f = new FileControllor();
        OpenFileDlg pth = f.OpenProject(FileType.OBJ);
        if (pth == null) return;

        string url = pth.file;

        GameObject gameObject = new GameObject();
        OBJ obj = gameObject.AddComponent<OBJ>();
        obj.LoadObj("file:///" + url);

        DispatchEvent(new FileEvent(FileEvent.LOAD_OBJ_COMPLETE, url, "", gameObject));
    }
}
