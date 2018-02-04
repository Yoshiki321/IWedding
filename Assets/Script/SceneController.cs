using System;  
using System.Xml;
using System.Collections; 
using UnityEngine;

public class SceneController : MonoBehaviour 
{
	void Start () 
	{
		//new Layer();

	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.B)) 
		{
			Program.Init (loadXmlComplete);
		}
	}

	private void loadXmlComplete(string code)
	{
		JsonDatas m = JsonUtility.FromJson<JsonDatas> (code);
		XmlDocument xml = new XmlDocument ();
		xml.LoadXml (m.data.talken);
		LoadXml.Load (xml);
	}

	IEnumerator SendPost(string _url, WWWForm _wForm)  
	{  
		WWW postData = new WWW(_url, _wForm);  
		yield return postData;  
		if (postData.error != null)  
		{  
			Debug.Log(postData.error);  
		}  
		else  
		{  
			Debug.Log(postData.text);  
		}  
	}  

}

[Serializable]
public class JsonDatas {

	public string code;

	public string message;

	public Datas data;
}

[Serializable]
public class Datas {

	public string talken;
}
