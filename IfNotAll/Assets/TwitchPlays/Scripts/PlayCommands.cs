﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

//[RequireComponent (typeof(TwitchIRC))]
public class PlayCommands : MonoBehaviour 
{
	[Tooltip("List of available twitch commands")]
	public List<TwitchCommand> Commands;
	[Tooltip("Optional delimiter for command options\n   ie. \'vote: 1\', delimiter would be \':\'")]
	public static string delimiter = ":";

    public static char seperator = ':';

	TwitchIRC twitch;

	void Start()
	{
        //Make twitchIRC a singleton, probably, and then you can have per-module events
        twitch = TwitchIRC.Instance;

		twitch.messageRecievedEvent.AddListener(getCommand);
	}

	void getCommand(string str)
	{
		//Debug.Log(str);
        string playerName = str.Substring(1, str.IndexOf("!")-1);
        //Debug.Log(playerName);
        bool isStreamer = playerName.ToLower().Equals(twitch.nickName.ToLower());
        //Remove non-command parts of message (like username)
        int msgIndex = str.IndexOf("PRIVMSG #");
        str = str.Substring(msgIndex + twitch.nickName.Length + 11);
		//Allow non delimited commands using the entire string (ie 'A' for A-button instead of 'button: A')
		string cmd = str;
		if(delimiter.Length > 0 && str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).Length > 1)
		{
			string[] blocks = str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			cmd = blocks[0];
			str = "";
			for(int i=1; i<blocks.Length; i++)
			{
				str += blocks[i];
			}
		}
        str = playerName + seperator + str;
		//Debug.Log("Got Command: " + cmd);
		foreach(var v in Commands)
		{
            if (((v.streamerOnly == isStreamer) || v.streamerOnly == false) && v.currentlyActive)
                if ((isStreamer || (ViewerRegistry.Registry.ContainsKey(playerName) && ViewerRegistry.Registry[playerName].IsAlive)) || cmd.Trim().ToLower().Equals("join"))
                {
                    if (cmd.Trim().ToLower().Equals(v.commandKey.Trim().ToLower()))
                    {
                        //Debug.Log(str);
                        v.onCommand.Invoke(str.ToLower());
                    }
                }
                else
                {
                    Debug.Log("Command not executed because " + playerName + " is dead!");
                }
		}
	}

    public static void ToggleCommand(string cmd, bool enabled)
    {
        PlayCommands[] playCommands = FindObjectsOfType<PlayCommands>();
        foreach (PlayCommands pc in playCommands)
        {
            foreach (var v in pc.Commands)
            {
                if (cmd.Trim().ToLower().Equals(v.commandKey.Trim().ToLower()))
                {
                    v.currentlyActive = enabled;
                    Debug.Log(v.name + " enabled: " + enabled);  
                }
            }
        }
    }
}


[System.Serializable]
public class TwitchCommand
{
	[Tooltip("Name of Command, not relevant to code")]
	public string name;
    [Tooltip("Category of Command, for HELP sorting purposes")]
    public CommandCategory category;
    [Tooltip("Is this Command one that only the Streamer can use?")]
    public bool streamerOnly = false;
    [Tooltip("Is this Command currently allowed to be used?")]
    public bool currentlyActive = true;
	[Tooltip("string key for command\n   ie \'vote\', \'A\', \'up\'")]
	public string commandKey;
	[Tooltip("Method to call, will pass command options as a string")]
    public string description;
    [Tooltip("String displayed when the HELP command is called")]
    [SerializeField]
 	public stringEvent onCommand;
}

[System.Serializable]
public class stringEvent : UnityEvent<string>{}
