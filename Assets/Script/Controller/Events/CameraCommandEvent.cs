public class CameraCommandEvent : EventObject
{
    public const string CHANGE = "CameraEvent_CHANGE";
    public const string UPDATE = "CameraEvent_UPDATE";

    public CameraFlags data;

    public CameraCommandEvent(string types, CameraFlags data)
        : base(types)
    {
        this.data = data;
    }
}
