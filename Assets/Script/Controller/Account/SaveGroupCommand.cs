public class SaveGroupCommand : Command
{
    public override void Execute(EventObject e)
    {
        GroupCommandEvent groupCommandEvent = e as GroupCommandEvent;
        groupCommandEvent.xml.Save(AccountModel.Instance.groupURL + "" + groupCommandEvent.name);
    }
}
