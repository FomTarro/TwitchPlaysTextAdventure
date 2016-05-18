using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayCommands.ToggleCommand("join", false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Begin(string input)
    {
        

        Debug.Log(input);

        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;
        PlayCommands.ToggleCommand("join", true);
        PlayCommands.ToggleCommand("start", false);
        ResourceTracker.Instance.IncrementDay();
        TickerHeader.Instance.PrintToHeader("Type 'JOIN: <classname>' to join the caravan.\nIf no class is provided, one will be randomly assigned.");
        TwitchIRC.Instance.SendMsg("join");

    }
}
