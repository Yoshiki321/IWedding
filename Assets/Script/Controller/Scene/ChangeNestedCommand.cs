using Build2D;
using BuildManager;
using System.Collections.Generic;

public class ChangeNestedCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;
        if (se.isRedoAction || se.isUndoAction)
        {
            //List<string> ids = new List<string>();

            NestedVO assets = new NestedVO();
            NestedVO newAssets = new NestedVO();

            for (int i = 0; i < se.oldAssets.Count; i++)
            {
                if (se.oldAssets[i] is NestedVO)
                {
                    assets = se.oldAssets[i] as NestedVO;
                    newAssets = se.newAssets[i] as NestedVO;
                }
            }

            newAssets.lines = assets.lines;
            return;
        }

        SaveOldValue(se, CloneAssets(se.oldAssets));

        CommitHistoryEvent((SceneEvent)e);
    }
}
