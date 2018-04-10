using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.IO;

public class TransformationZwkjtiobj : MonoBehaviour
{
    InputField inputField;
    InputField inputField1;
    InputField inputField2;
    InputField inputField3;
    Button button;
    Button button1;

    void Start()
    {
        inputField = transform.Find("InputField").gameObject.GetComponent<InputField>();
        inputField1 = transform.Find("InputField (1)").gameObject.GetComponent<InputField>();
        inputField2 = transform.Find("InputField (2)").gameObject.GetComponent<InputField>();
        inputField3 = transform.Find("InputField (3)").gameObject.GetComponent<InputField>();
        button = transform.Find("Button").gameObject.GetComponent<Button>();
        button1 = transform.Find("Button (1)").gameObject.GetComponent<Button>();

        button.onClick.AddListener(OnClick);
        button1.onClick.AddListener(OnClick1);
    }

    void OnClick()
    {
        inputField1.text = DES_zaowu.Decder(inputField.text);

        WWW www = new WWW("file:///" + Application.dataPath + "\\Data\\" + "decder");
        if (www.error == null)
        {
            string str = System.Text.Encoding.Default.GetString(www.bytes);
            www.Dispose();

            if (str == "") return;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(DES_zaowu.Decder(str));
            xml.Save(Application.dataPath + "\\Data\\" + "encoder");
        }
    }

    void OnClick1()
    {
        inputField3.text = DES_zaowu.Encoder(inputField2.text);

        WWW www = new WWW("file:///" + Application.dataPath + "\\Data\\" + "encoder");
        if (www.error == null)
        {
            string str = System.Text.Encoding.Default.GetString(www.bytes);
            www.Dispose();

            if (str == "") return;

            string code = DES_zaowu.Encoder(str);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(code);

            File.WriteAllBytes(Application.dataPath + "\\Data\\" + "decder", bytes);
        }
    }

    void Update()
    {

    }
}
