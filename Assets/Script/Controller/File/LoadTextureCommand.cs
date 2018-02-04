using Common;
using UnityEngine;

public class LoadTextureCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FileControllor f = new FileControllor();

        OpenFileDlg pth = f.OpenProject(FileType.TEXTURE);
        if (pth == null) return;

        string url = pth.file;
        WWW www = new WWW("file:///" + url);
        Texture2D tex = www.texture;
        DispatchEvent(new FileEvent(FileEvent.LOAD_TEXTURE_COMPLETE, url, "", tex));
    }
}
