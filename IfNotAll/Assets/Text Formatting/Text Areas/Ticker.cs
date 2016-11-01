using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ticker : Singleton<Ticker> {

    public int _maxLines = 27;

    public List<string> _tickerHistory;

    [SerializeField]
    private Text _textElement;

    private int _index = 0;

    [SerializeField]
    private Scrollbar _scroll;

	// Use this for initialization
	void Start () {
        _tickerHistory = new List<string>();
        /*
        for (int i = 0; i < 50; i++)
        {
            PrintToTicker(i.ToString());
        }
        */
        
    }
	
	// Update is called once per frame
	void Update () {
        _scroll.size = (((float)_maxLines) / (_tickerHistory.Count+1));
	}

    public void PrintToTicker(string msg)
    {
        msg = msg.HighlightPresets();
        //msg = msg.HighlightResources();

        /*
        foreach (Viewer v in ViewerRegistry.Registry.Values)
        {
            msg = msg.Replace(v.Username.ToLower(), TextEffects.Instance.DisplayName(v.Username));
        }
        */
        string[] list = msg.Split('\n');
        foreach(string s in list)
        {
            _tickerHistory.Add("* " + s + "\n");
        }
        
        _textElement.text = "";
        //textElement.text = "JOIN the caravan at any time by typing\nJOIN: <classname>\nIf no class is provided, one will be randomly assigned.\n------------------\n";
        for (int i = _index; i < _tickerHistory.Count; i++)
        {
            _textElement.text += _tickerHistory[i];
        }
        _scroll.value = 1;
        _scroll.onValueChanged.Invoke(1);
    }

    public void UpdateTickerPosition(float position)
    {
        _index = Mathf.FloorToInt(Mathf.Clamp(_tickerHistory.Count-_maxLines, 0, _tickerHistory.Count)*position);
        _textElement.text = "";
        //textElement.text = "JOIN the caravan at any time by typing\nJOIN: <classname>\nIf no class is provided, one will be randomly assigned.\n------------------\n";
        for (int i = _index; i < _tickerHistory.Count; i++)
        {
            _textElement.text += _tickerHistory[i];
        }
        //Debug.Log("Start index: " + index);
    }

}
