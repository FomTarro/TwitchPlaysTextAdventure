using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextBody :Singleton<TextBody> {

    public int maxLines;

    public List<string> bodyHistory;

    [SerializeField]
    private Text textElement;

    private int index = 0;

    [SerializeField]
    private Scrollbar scroll;

    public string testString;

    // Use this for initialization
    IEnumerator Start () {

        testString = "WAY TO FALL \nA Twitch-integrated multiplayer text pilgrimage.\nBuilt in Unity by Tom Farro.\nType 'HELP' for a list of commands.\nRevision 00 / Serial number 000000.\n57 41 59 54 4F 46 41 4C 4C\n";
        PrintToBody(testString);
        yield return new WaitForSeconds(1.0f);
        if (!TwitchLogin.connected)
        {
            testString = "To begin, please 'LOGIN' to the terminal.";
            PrintToBody(testString);
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
        scroll.size = (((float)maxLines) / (bodyHistory.Count + 1));
    }

    public void PrintToBody(string msg)
    {
        msg = msg.EnforceNewlines();
        string[] lines = msg.Split('\n');
        foreach(string s in lines)
        {
            bodyHistory.Add(s + "\n");
        }

       
        textElement.text = "";
        for (int i = index; i < bodyHistory.Count; i++)
        {
            textElement.text += bodyHistory[i];
        }
        scroll.value = 1;
        scroll.onValueChanged.Invoke(1);
    }

    public void UpdateBodyPosition(float position)
    {
        index = Mathf.FloorToInt(Mathf.Clamp(bodyHistory.Count - maxLines, 0, bodyHistory.Count) * position);
        textElement.text = "";
        for (int i = index; i < bodyHistory.Count; i++)
        {
            textElement.text += bodyHistory[i];
        }
        //Debug.Log("Start index: " + index);
    }
}
