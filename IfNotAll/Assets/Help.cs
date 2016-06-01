using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Help : MonoBehaviour {

    [SerializeField]
    [HideInInspector]
    private Module tooltipModule;

    private TextBody _body;

	// Use this for initialization
	void Start () {
        _body = TextBody.Instance;
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


    public void HelpCommandCategorized(string input)
    {

        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        List<PlayCommands> pcList = FindObjectsOfType<PlayCommands>().ToList();
        pcList = pcList.OrderBy(PlayCommands => PlayCommands.name).ToList();
        Array categories = Enum.GetValues(typeof(CommandCategory));
        List<CommandCategory> categoriesList = new List<CommandCategory>();
        foreach(CommandCategory cc in categories)
        {
            categoriesList.Add(cc);
        }

        categoriesList = categoriesList.OrderBy(x => x.ToString()).ToList();

        foreach (CommandCategory cc in categoriesList)
        {
            string category = cc.ToString();
            List<TwitchCommand> validCommands = new List<TwitchCommand>();
            foreach (PlayCommands pc in FindObjectsOfType<PlayCommands>())
            {
                foreach (TwitchCommand tc in pc.Commands)
                {
                    if (tc.category == cc)
                    {
                        bool categoryContains = category.ToLower().Contains(parameter.ToLower()) && !tc.description.Equals("");
                        bool commandContains = tc.commandKey.ToLower().Contains(parameter.ToLower()) || tc.name.ToLower().Contains(parameter.ToLower());
                        bool commandEnabled = (!tc.description.Equals("") && tc.currentlyActive);
                        if ((parameter.ToLower().Equals("help") && commandEnabled) || (categoryContains || commandContains) && commandEnabled)
                        {
                            validCommands.Add(tc);
                        }
                    }
                }
            }
            List<TwitchCommand> sortedCommands = validCommands.OrderBy(TwitchCommand => TwitchCommand.commandKey).ToList();

            if (sortedCommands.Count > 0)
            {
                _body.PrintToBody("[magenta]" + category.ToUpper() + "[magenta]:");
            }

            foreach (TwitchCommand tc in sortedCommands)
            {
                string display = "* " + "[yellow]" + tc.commandKey.ToUpper() + "[yellow]";
                if (tc.streamerOnly)
                {
                    display += " ([cyan]Streamer Only[cyan])";
                }
                _body.PrintToBody(display);
                _body.PrintToBody(tc.description);
            }
            if (sortedCommands.Count > 0)
            {
                _body.PrintToBody("");

            }
        }
    }

    public void Credits(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        _body.PrintToBody(ModuleMaster.Instance.LookupHandleInModule("Title Screen", "credits"));
    }
}
