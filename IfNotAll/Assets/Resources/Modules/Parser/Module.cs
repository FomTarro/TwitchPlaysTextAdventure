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
    public string locationName;

	// Use this for initialization
	void Awake () {
        Parse();
       
	}

    void Start()
    {
        
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
                    Debug.Log(e.Message);
                }
                
            }

        }

        Dictionary<string, string> stringListReplaced = new Dictionary<string, string>();

        foreach (string key in stringList.Keys)
        {
            stringListReplaced.Add(key, ReplaceSubs(stringList[key]));
        }

        stringList = stringListReplaced;
        

        if (stringList.ContainsKey("loc"))
        {
            locationName = stringList["loc"];
        }
        else
        {
            locationName = "Uncharted Territory";
        }
        

    }

    string ReplaceSubs(string str)
    {
        int[] indicies = str.IndexOfAll('#');
        int mod = 0;
        int start = 0;
        int end = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                start = indicies[i];
            }
            else
            {
                end = indicies[i];
                string subKey = str.Substring(start + 1, (end - start) - 1);
                string subValue = str.Substring(start, (end - start)+1);
                Debug.Log(subValue);
                if (stringList.ContainsKey(subKey))
                {
                    str = str.Replace(subValue, stringList[subKey]);
                    Debug.Log(str);
                }
                ReplaceSubs(str);
            }
            mod++;
        }
        return str;
    }

    public void EnterArea()
    {
        Ticker.Instance.PrintToTicker("Arrived at " + TextEffects.Instance.DisplayTitle(locationName)+".");
        ResourceTracker.Instance.SetLocation(locationName);
    }
}
