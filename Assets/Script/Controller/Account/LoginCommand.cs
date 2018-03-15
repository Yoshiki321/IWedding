using BuildManager;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LoginCommand : Command
{
    public override void Execute(EventObject e)
    {
        LoginEvent loginEvent = e as LoginEvent;
        Login(loginEvent.account, loginEvent.password);
    }

    private string _account;
    private string _password;

    private void Login(string account, string password)
    {
        _account = account;
        _password = password;
        CreateFile();
    }

    private void CreateFile()
    {
        AccountModel.Instance.Login(_account, _password);
        DispatchEvent(new LoginEvent(LoginEvent.LOGIN_COMPLETE));
    }
}
