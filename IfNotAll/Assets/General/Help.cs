using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Help : MonoBehaviour {

    [SerializeField]
    private Module tooltipModule; 

	// Use this for initialization
	void Start () {
        //FillTooltips();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Would be used if we wanted to use xml for command tooltips.
    public void FillTooltips()
    {
        List<PlayCommands> pcList = FindObjectsOfType<PlayCommands>().ToList();
        List<TwitchCommand> missingList = new List<TwitchCommand>();
        foreach (PlayCommands pc in pcList)
        {
            List<TwitchCommand> commands = pc.Commands;
            foreach (TwitchCommand tc in commands)
            {
                if (tooltipModule.StringList.ContainsKey(tc.commandKey.ToLower()))
                {
                    tc.description = tooltipModule.StringList[tc.commandKey.ToLower()];
                }
                else
                {
                    tc.description = "";
                    missingList.Add(tc);
                }
            }
        }
        string missingDesc = "";
        foreach (TwitchCommand tc in missingList)
        {
            missingDesc += tc.commandKey + "\n";
        }
        Debug.Log("The following commands are missing tooltips: " + missingDesc);
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
            string pcType = pc.name.Substring(0, pc.name.ToLower().IndexOf(" commands"));

            foreach (TwitchCommand tc in commands)
            {

                bool categoryContains = pcType.Contains(parameter) && !tc.description.Equals("");
                bool commandContains = tc.commandKey.Contains(parameter) || tc.name.Contains(parameter);
                bool commandEnabled = (!tc.description.Equals("") && tc.currentlyActive);
                if ((parameter.Equals("help") && commandEnabled) || (categoryContains || commandContains) && commandEnabled)
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
