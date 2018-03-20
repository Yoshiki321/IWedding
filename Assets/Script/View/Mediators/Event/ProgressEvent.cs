public class ProgressEvent : EventObject
{
    public static string LOAD_COMPLETE = "ProgressEvent_LOAD_COMPLETE";

    public ProgressEvent(string types)
        : base(types)
    {
    }
}
