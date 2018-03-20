public class ProgressPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(ProgressPanelEvent.LOAD_COMPLETE, LoadCompleteHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(ProgressPanelEvent.LOAD_COMPLETE, LoadCompleteHandle);
    }

    private void LoadCompleteHandle(EventObject e)
    {
        DispatcherEvent(new ProgressEvent(ProgressEvent.LOAD_COMPLETE));
    }
}
