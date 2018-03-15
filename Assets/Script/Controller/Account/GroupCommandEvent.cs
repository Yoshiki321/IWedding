using System.Xml;

public class GroupCommandEvent : EventObject
{
    public XmlDocument xml;
    public string name;

    public GroupCommandEvent(string types, XmlDocument xml, string name)
        : base(types)
    {
        this.xml = xml;
        this.name = name;
    }
}
