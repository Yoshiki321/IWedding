using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class AccountModel : Actor<AccountModel>
{
    private string _account;
    private string _password;
    private string _url;
    private string _groupURL;

    public void Login(string account, string password)
    {
        _account = account;
        _password = password;

        CreateFile();
    }

    private void CreateFile()
    {
        _url = Application.dataPath + "/" + _account;
        _groupURL = url + "/" + "group";
        if (Directory.Exists(_url) == false)
        {
            Directory.CreateDirectory(_url);
            Directory.CreateDirectory(_groupURL);
            CodeManager.GetGroupCatalogXml().Save(url + "/" + "group" + "/" + "GroupCatalog.xml");
        }
    }

    public string account
    {
        get { return _account; }
    }

    public string password
    {
        get { return _password; }
    }

    public string url
    {
        get { return _url; }
    }

    public string groupURL
    {
        get { return _groupURL; }
    }
}
