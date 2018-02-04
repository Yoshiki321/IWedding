using UnityEngine;
using System.Collections;

public class ChangeCollageCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;
        if (se.isRedoAction || se.isUndoAction)
        {
            return;
        }

        SaveOldValue(se, CloneAssets(se.oldAssets));

        CommitHistoryEvent((SceneEvent)e);
    }
}
