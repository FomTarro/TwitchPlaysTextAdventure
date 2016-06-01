using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PlayCommands))]
public class Module : MonoBehaviour {

    [SerializeField]
    private TextAsset _document;
    public TextAsset Document
    {
        get { return _document; }
        set { _document = value; }
    }

    [HideInInspector]
    [SerializeField]
    public List<string> _handles = new List<string>();

    [SerializeField]
    private Dictionary<string, string> _stringList = new Dictionary<string, string>();

    public Dictionary<string,string> StringList
    {
        get { return _stringList; }
    }
    public Location location;

    [HideInInspector]
    [SerializeField]
    public int index = 0;

    [SerializeField]
    public string startTextHandle = "";

    private Ticker _ticker;
    private TextBody _body;
    private PlayCommands _commands;

   // Use this for initialization
   void Awake () {
        transform.SetParent(ModuleMaster.Instance.transform);

        _ticker = Ticker.Instance;
        _body = TextBody.Instance;
        _commands = GetComponent<PlayCommands>();

        Parse();
        ModuleMaster.Instance.SetActiveModule(this);
    }

    void OnValidate()
    {
        Parse();
    }

    void Parse()
    {
        if (_document != null)
        {

            _stringList = new Dictionary<string, string>();
            _handles = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(_document.text);
            XmlNodeList moduleList = xmlDoc.GetElementsByTagName("module");
            foreach (XmlNode info in moduleList)
            {
                XmlNodeList strings = info.ChildNodes;
                foreach (XmlNode line in strings)
                {
                    try
                    {
                        _stringList.Add(line.Attributes["id"].Value.ToLower(), line.InnerText.EnforceNewlines());
                        _handles.Add(line.Attributes["id"].Value);
                        //Debug.Log(line.Attributes["id"].Value + ": " + line.InnerText.EnforceNewlines());
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                }

            }

            Dictionary<string, string> stringListReplaced = new Dictionary<string, string>();

            foreach (string key in _stringList.Keys)
            {
                stringListReplaced.Add(key, ReplaceSubs(_stringList[key]));
            }

            _stringList = stringListReplaced;


            if (_stringList.ContainsKey("loc"))
            {
                location.locationName = _stringList["loc"];
            }
            else
            {
                location.locationName = "Uncharted Territory";
            }
        }
        name = location.locationName;
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
                if (_stringList.ContainsKey(subKey))
                {
                    str = str.Replace(subValue, _stringList[subKey]);
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
            _ticker.PrintToTicker("Arrived at " + location.LocationFormatted + ".");
            ResourceTracker.Instance.SetLocation(location.locationName);
        }
        if (_stringList.ContainsKey(startTextHandle))
        {
            _body.PrintToBody(_stringList[startTextHandle]);
        }
    }
}
