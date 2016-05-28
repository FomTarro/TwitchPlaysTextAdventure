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
    public TextAsset Document
    {
        get { return document; }
        set { document = value; }
    }

    [HideInInspector]
    [SerializeField]
    public List<string> handles = new List<string>();

    [SerializeField]
    private Dictionary<string, string> stringList = new Dictionary<string, string>();

    public Dictionary<string,string> StringList
    {
        get { return stringList; }
    }
    public Location location;

    [HideInInspector]
    [SerializeField]
    public int index = 0;

    [SerializeField]
    public string startTextHandle = "";


   // Use this for initialization
   void Awake () {
        transform.SetParent(ModuleMaster.Instance.transform);
        name = location.locationName;
        Parse();
        EnterArea();
    }

    void OnValidate()
    {
        Parse();
    }

    void Parse()
    {

        stringList = new Dictionary<string, string>();
        handles = new List<string>();

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
                    handles.Add(line.Attributes["id"].Value);
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
            location.locationName = stringList["loc"];
        }
        else
        {
            location.locationName = "Uncharted Territory";
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
                //Debug.Log(subValue);
                if (stringList.ContainsKey(subKey))
                {
                    str = str.Replace(subValue, stringList[subKey]);
                    //Debug.Log(str);
                }
                ReplaceSubs(str);
            }
            mod++;
        }
        return str;
    }

    public void EnterArea()
    {
        if (location.displayEntryInTicker)
        {
            Ticker.Instance.PrintToTicker("Arrived at " + location.LocationFormatted() + ".");
            ResourceTracker.Instance.SetLocation(location.locationName);
        }
        if (stringList.ContainsKey(startTextHandle))
        {
            TextBody.Instance.PrintToBody(stringList[startTextHandle]);
        }
    }
}
