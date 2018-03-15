public class LoginEvent : EventObject
{
    public const string LOGIN = "LoginEvent_LOGIN";
    public const string LOGIN_COMPLETE = "LoginEvent_LOGIN_COMPLETE";

    public string account;
    public string password;

    public LoginEvent(string types, string account = "", string password = "")
        : base(types)
    {
        this.account = account;
        this.password = password;
    }
}
