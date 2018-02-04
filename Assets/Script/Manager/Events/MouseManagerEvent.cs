public class MouseManagerEvent : EventObject
{
    public static string SELECT_OBJECT = "MouseManagerEvent_SELECT_OBJECT";
    public static string RELEASE_OBJECT = "MouseManagerEvent_RELEASE_OBJECT";

    public static string DOWN_OBJECT = "MouseManagerEvent_DOWN_OBJECT";

    public ObjectSprite objectSprite;

    public MouseManagerEvent(string types, ObjectSprite objectSprite = null)
        : base(types)
    {
        this.objectSprite = objectSprite;
    }
}
