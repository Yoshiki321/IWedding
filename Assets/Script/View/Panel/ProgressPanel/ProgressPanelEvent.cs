public class ProgressPanelEvent : EventObject
{
    public static string LOAD_COMPLETE = "ProgressPanelEvent_LOAD_COMPLETE";

    public ProgressPanelEvent(string types)
        : base(types)
    {
    }
}
