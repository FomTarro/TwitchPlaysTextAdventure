using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class TextBody : Singleton<TextBody> {

    public int _maxLines = 27;

    public List<string> _bodyHistory;

    [SerializeField]
    private Text _textElement;

    private int _index = 0;

    [SerializeField]
    private Scrollbar _scroll;

    public string _testString;

    void Awake()
    {
        //bodyHistory = new List<string>();
    }

    // Use this for initialization
    IEnumerator Start () {
       
        /*
        testString = TextEffects.Instance.DisplayTitle("WAY TO FALL") + "\nA Twitch-integrated multiplayer text pilgrimage.\nBuilt in Unity by @Tom Farro@" 
            + ".\nType 'HELP' for a list of commands.\nRevision 00 / Serial number 000000.\n57 41 59 54 4F 46 41 4C 4C\n";
        PrintToBody(testString);
        */
        yield return new WaitForSeconds(1.0f);
        if (!TwitchLogin.connected)
        {
            _testString = "To begin, please 'LOGIN' to the terminal.";
            PrintToBody(_testString);
        }
        /*
        for (int i = 0; i <50; i++)
        {
            PrintToBody(i.ToString());
        }
        */

    }

    // Update is called once per frame
    void Update () {
        _scroll.size = (((float)_maxLines) / (_bodyHistory.Count + 1));
    }

    public void PrintToBody(string msg)
    {
        
        msg = msg.EnforceNewlines();
        msg = msg.HighlightPresets();

        /*
        int[] indicies = msg.IndexOfAll('\'');
        string start = "<color=yellow>";
        string end = "</color>";
        int offset = 0;
        int mod = 0;
        for(int i = 0; i < indicies.Length; i++)
        {
            if(mod%2 == 0)
            {
                msg = msg.Insert(indicies[i] + offset, start);
                offset += start.Length;
            }
            else
            {
                msg = msg.Insert(indicies[i] + offset + 1, end);
                offset += end.Length + 1;
            }
            mod++;
        }
        */

        string[] lines = msg.Split('\n');
        foreach(string s in lines)
        {
            _bodyHistory.Add(s + "\n");
        }

       
        _textElement.text = "";
        for (int i = _index; i < _bodyHistory.Count; i++)
        {
            _textElement.text += _bodyHistory[i];
        }
        _scroll.value = 1;
        _scroll.onValueChanged.Invoke(1);
    }

    public void UpdateBodyPosition(float position)
    {
        _index = Mathf.FloorToInt(Mathf.Clamp(_bodyHistory.Count - _maxLines, 0, _bodyHistory.Count) * position);
        _textElement.text = "";
        for (int i = _index; i < _bodyHistory.Count; i++)
        {
            _textElement.text += _bodyHistory[i];
        }
        //Debug.Log("Start index: " + index);
    }
}
