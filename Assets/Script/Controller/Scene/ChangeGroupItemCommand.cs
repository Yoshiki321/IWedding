using Build2D;
using Build3D;

public class ChangeGroupItemCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;
        if (se.isRedoAction || se.isUndoAction) return;

        SaveOldValue(se, CloneAssets(se.oldAssets));

        for (int i = 0;i < se.newAssets.Count;i++)
        {
            ObjectData o = AssetsModel.Instance.GetObjectData(se.newAssets[i].id);
            ((o.object2 as Item2D).VO as ItemVO).groupId = (se.newAssets[i] as ItemVO).groupId;
            ((o.object3 as Item3D).VO as ItemVO).groupId = (se.newAssets[i] as ItemVO).groupId;
            (o.vo as ItemVO).groupId = (se.newAssets[i] as ItemVO).groupId;
        }

        CommitHistoryEvent((SceneEvent)e);
    }
}

