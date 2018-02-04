public class ComponentVO : AssetVO
{
    public virtual void FillFromObject(ComponentVO asset)
    {

    }

    private object _code;

    public new virtual object Code
    {
        get { return null; }
        set { _code = value; }
    }
}
