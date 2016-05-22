using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ticker : Singleton<Ticker> {

    public int maxLines;

    public List<string> tickerHistory;

    [SerializeField]
    private Text textElement;

    private int index = 0;

    [SerializeField]
    private Scrollbar scroll;

	// Use this for initialization
	void Start () {
    
        /*
        for (int i = 0; i < 50; i++)
        {
            PrintToTicker(i.ToString());
        }
        */
        
    }
	
	// Update is called once per frame
	void Update () {
        scroll.size = (((float)maxLines) / (tickerHistory.Count+1));
	}

    public void PrintToTicker(string msg)
    {
        msg = msg.HighlightCommands();

        foreach (Viewer v in ViewerRegistry.Registry.Values)
        {
            msg = msg.Replace(v.Username.ToLower(), "<color=#"+HexConverter.ColorToHex(ColorRegistry.Instance.ColorList["white"])+">" + v.Username + "</color>");
        }
        string[] list = msg.Split('\n');
        foreach(string s in list)
        {
            tickerHistory.Add("# " + s + "\n");
        }
        
        textElement.text = "";
        //textElement.text = "JOIN the caravan at any time by typing\nJOIN: <classname>\nIf no class is provided, one will be randomly assigned.\n------------------\n";
        for (int i = index; i < tickerHistory.Count; i++)
        {
            textElement.text += tickerHistory[i];
        }
        scroll.value = 1;
        scroll.onValueChanged.Invoke(1);
    }

    public void UpdateTickerPosition(float position)
    {
        index = Mathf.FloorToInt(Mathf.Clamp(tickerHistory.Count-maxLines, 0, tickerHistory.Count)*position);
        textElement.text = "";
        //textElement.text = "JOIN the caravan at any time by typing\nJOIN: <classname>\nIf no class is provided, one will be randomly assigned.\n------------------\n";
        for (int i = index; i < tickerHistory.Count; i++)
        {
            textElement.text += tickerHistory[i];
        }
        //Debug.Log("Start index: " + index);
    }

}
