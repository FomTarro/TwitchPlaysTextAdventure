using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Help : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HelpCommand(string input)
    {

        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        List<PlayCommands> pcList = FindObjectsOfType<PlayCommands>().ToList();
        pcList = pcList.OrderBy(PlayCommands => PlayCommands.name).ToList();

        foreach (PlayCommands pc in pcList)
        {
            List<TwitchCommand> commands = pc.Commands;
            List<TwitchCommand> validCommands = new List<TwitchCommand>();

            foreach(TwitchCommand tc in commands)
            {
                if((tc.commandKey.Contains(parameter) || (parameter.Equals("help") && !tc.description.Equals(""))) && tc.currentlyActive)
                {
                    validCommands.Add(tc);
                }
            }
            List<TwitchCommand> sortedCommands = validCommands.OrderBy(TwitchCommand => TwitchCommand.commandKey).ToList();

            if(sortedCommands.Count > 0)
            {
                TextBody.Instance.PrintToBody("# " + pc.name.ToUpper() + ":");
            }

            foreach (TwitchCommand tc in sortedCommands)
            {
                    string display = "* " + "'" + tc.commandKey.ToUpper() + "'";
                    if (tc.streamerOnly)
                    {
                        display += " (Streamer Only)";
                    }
                    TextBody.Instance.PrintToBody(display);
                    TextBody.Instance.PrintToBody(tc.description);
            }
            if (sortedCommands.Count > 0)
            {
                TextBody.Instance.PrintToBody("\n");
            }
        }
    }
}
