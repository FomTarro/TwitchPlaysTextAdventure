using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(TwitchIRC))]
public class StreamerOnlyCommands : MonoBehaviour
{
    [Tooltip("List of available twitch commands")]
    public List<TwitchCommand> Commands;
    [Tooltip("Optional delimiter for command options\n   ie. \'vote: 1\', delimiter would be \':\'")]
    public string delimiter;

    public static char seperator = ':';

    TwitchIRC twitch;

    void Start()
    {
        twitch = GetComponent<TwitchIRC>();

        twitch.messageRecievedEvent.AddListener(getCommand);
    }

    void getCommand(string str)
    {
        //Debug.Log(str);
        string playerName = str.Substring(1, str.IndexOf("!") - 1);
        //Debug.Log(playerName);
        if (playerName.ToLower().Equals(twitch.nickName.ToLower()))
        {
            //Remove non-command parts of message (like username)
            int msgIndex = str.IndexOf("PRIVMSG #");
            str = str.Substring(msgIndex + twitch.nickName.Length + 11);

            //Allow non delimited commands using the entire string (ie 'A' for A-button instead of 'button: A')
            string cmd = str;
            if (delimiter.Length > 0 && str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).Length > 1)
            {
                string[] blocks = str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                cmd = blocks[0];
                str = "";
                for (int i = 1; i < blocks.Length; i++)
                {
                    str += blocks[i];
                }
            }
            str = playerName + seperator + str;
            //Debug.Log("Got Command: " + cmd);
            foreach (var v in Commands)
            {

                if (cmd.Trim().ToLower().Equals(v.commandKey.Trim().ToLower()))
                {
                    //Debug.Log(str);
                    v.onCommand.Invoke(str);
                }
            }
        }
    }
}


