using BuildManager;
using Common;
using UnityEngine;

public class LoadSoundCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FileControllor f = new FileControllor();

        OpenFileDlg pth = f.OpenProject(FileType.SOUND);
        if (pth == null) return;
        string url = pth.file;
        SceneManager.Instance.audioSourceManager.AddSound(url);
        DispatchEvent(new FileEvent(FileEvent.LOAD_SOUND_COMPLETE, url));
    }
}
