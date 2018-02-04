using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Xml;
using System.IO;
using Build3D;

public class LoadXml 
{

	// Use this for initialization
	public static void Load (XmlDocument xml) 
	{
		//BuilderModel3D.Clear ();
		Layer.Clear ();

		Layer.Init ();

//		XmlDocument xml = new XmlDocument();
//		XmlReaderSettings set = new XmlReaderSettings();
//		set.IgnoreComments = true;
//		xml.Load(XmlReader.Create((Application.dataPath+"/data.xml"),set));

		XmlNode code = xml.SelectSingleNode("code");
		XmlNode builderCode = code.SelectSingleNode("builder");
		XmlNode assetsCode = code.SelectSingleNode("assets");

		if (builderCode != null) 
		{
			foreach (XmlElement xl in builderCode.ChildNodes)
			{
				if (xl.Name == "line")
				{
					Line3D l = new Line3D();
					//l.SetCode (xl);
					//BuilderModel3D.AddLine(l);

					//CeilingLine3D cl = new CeilingLine3D();
					//l.ceilingLine3D = cl;
					//cl.SetCode(xl);
				}
				if (xl.Name == "surface")
				{
					Surface3D s = new Surface3D();
					//s.SetCode (xl);
					//BuilderModel3D.AddSurface(s);
				}
			}
		}

		if (assetsCode != null) 
		{
			foreach (XmlElement xl in assetsCode.ChildNodes)
			{
//				if (xl.Name == "item")
//				{
//					GameObject item = new GameObject ();
//					item.transform.parent = Layer.ItemLayer.transform;
//					item.AddComponent<Item3D> ().SetCode(xl);
//				}
//				if (xl.Name == "nested")
//				{
//					GameObject item = new GameObject ();
//					item.transform.parent = Layer.ItemLayer.transform;
//					item.AddComponent<Item3D> ().SetCode(xl);
//				}

				GameObject item = new GameObject ();
				item.transform.parent = Layer.ItemLayer.transform;

				item.AddComponent<ItemChangeController> ();
			}
		}	
	}
}
