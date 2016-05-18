using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

public class Module : MonoBehaviour {

    [SerializeField]
    private TextAsset document;
    [SerializeField]
    private Dictionary<string, string> stringList = new Dictionary<string, string>();
    public Dictionary<string,string> StringList
    {
        get { return stringList; }
    }

	// Use this for initialization
	void Awake () {
        Parse();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Parse()
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(document.text);
        XmlNodeList moduleList = xmlDoc.GetElementsByTagName("module");
        foreach (XmlNode info in moduleList)
        {
            XmlNodeList strings = info.ChildNodes;
            foreach (XmlNode line in strings) // levels itens nodes.
            {
                try
                {
                    stringList.Add(line.Attributes["id"].Value, line.InnerText.EnforceNewlines());
                    //Debug.Log(line.Attributes["id"].Value + ": " + line.InnerText.EnforceNewlines());
                }
                catch(Exception e)
                {
                    
                }
                
            }

        }
    }
}
