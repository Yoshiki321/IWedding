using UnityEngine;
using System.Collections;

public class ChangeBuildCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;
        BuildVO assets = se.oldAssets[0] as BuildVO;
        BuildVO newAssets = se.newAssets[0] as BuildVO;

        assets.FillFromBuild(newAssets);

        CommitHistoryEvent((SceneEvent)e);
    }
}
